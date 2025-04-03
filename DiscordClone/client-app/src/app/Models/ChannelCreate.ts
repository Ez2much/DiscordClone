export default interface ChannelCreateDto {
    serverId: string;
    name: string;
    channelType: string | null;
    topic: string | null;
}