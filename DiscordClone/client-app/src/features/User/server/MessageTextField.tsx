import { Box, Button, IconButton, TextField } from "@mui/material";
import { ChangeEvent, useEffect, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { v4 as uuidv4 } from 'uuid';
import Message from "../../../app/Models/message";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import SendIcon from '@mui/icons-material/Send';
export default observer(function MessageTextField() {
    const { channelStore, userStore, signalRStore, serverStore } = useStore(); 
    const [content, setContent] = useState("");
    const { channelIdParam } = useParams();
    useEffect(() => {
        console.log(channelIdParam);
        channelStore.getChannelByIdApi(channelIdParam!);
    }, [channelIdParam])
    const handleMessageChange = (e: ChangeEvent<HTMLInputElement>) => {
        setContent(e.target.value);
    };

    const handleSend = async () => {
        if (!content.trim()) return;

        const channelId = channelStore.selectedChannel!.channelId || channelIdParam!;

        const message: Message = {
            messageId: uuidv4(),
            content: content.trim(),
            createdAt: new Date().toISOString(),
            senderName: userStore.user?.username || "Anonymous",
            senderId: userStore.user!.id,
            channelId,
        };

        await signalRStore.sendMessage(message, channelStore.selectedChannel!.name, serverStore.selectedServer!.name);  

        setContent(""); 
    };

    return (
        <Box
            display="flex"
            gap={2}
            alignItems="center"
            width="100%"
        >
            <TextField
                fullWidth
                value={content}
                onChange={handleMessageChange}
                placeholder="Type your message..."
                onKeyDown={(e) => {
                    if (e.key === "Enter") {
                        e.preventDefault();
                        handleSend();
                    }
                }}
                sx={{
                    flexGrow: 1,
                }}
            />
            <IconButton onClick={handleSend}>
                <SendIcon />
            </IconButton>
        </Box>
    );
});
