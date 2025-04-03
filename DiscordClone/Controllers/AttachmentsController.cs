using System.Runtime.InteropServices;
using DiscordClone.Db;
using DiscordClone.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB limit
    private const string UploadsFolder = "wwwroot/uploads";
    private readonly ApplicationContext _context;

    public AttachmentsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAttachment(IFormFile file, Guid messageId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Validate file size
        if (file.Length > MaxFileSize)
        {
            return BadRequest("File size exceeds the maximum limit of 5 MB.");
        }

        // Ensure the uploads directory exists
        if (!Directory.Exists(UploadsFolder))
        {
            Directory.CreateDirectory(UploadsFolder);
        }

        // Generate a unique file name
        string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}"; // Generate unique name
        string filePath = Path.Combine(UploadsFolder, uniqueFileName);

        try
        {
            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        // Create an attachment record
        var attachment = new Attachment
        {
            AttachmentId = Guid.NewGuid(),
            AttachmentUrl = $"/uploads/{uniqueFileName}", // Store the relative URL
            AttachmentType = GetAttachmentType(file.ContentType),
            MessageId = messageId
        };

        // Save the attachment in the database
        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync(); // Ensure changes are saved

        return Ok(new { attachment.AttachmentId, attachment.AttachmentUrl, attachment.AttachmentType });
    }

    private AttachmentTypeEnum GetAttachmentType(string contentType)
    {
        if (contentType.StartsWith("image"))
            return AttachmentTypeEnum.Image;
        if (contentType.StartsWith("application/pdf"))
            return AttachmentTypeEnum.Document;

        // Add other content
        return AttachmentTypeEnum.Unknown;
    }
}
