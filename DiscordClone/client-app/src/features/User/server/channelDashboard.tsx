import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { Box, Typography, Divider } from "@mui/material";
import {  Outlet, useNavigate, useParams } from "react-router-dom";
import AddChannelButton from "./AddChannelButton";
import ServerMembers from "./serverMembers";

export default observer(function ChannelDashboard() {
    const { serverStore, channelStore } = useStore();
    const { serverId } = useParams();
    const navigate = useNavigate();
    const [sideBarOpen, setSideBarOpen] = useState<boolean>(false);
    useEffect(() => {
        if (serverId) {
            serverStore.getServerApi(serverId);
            channelStore.getChannelsApi(serverId);
            serverStore.fetchServerMembers(serverId);
        }
    }, [serverId, serverStore, channelStore]);

    //console.log(serverStore.serverMembers); 

    const handleClick = (channelId: string) => {
        navigate(`/server/${serverId}/${channelId}`);
    }

    return (
        <Box
            display="flex"
            flexDirection="row"
            height="91vh"
            width="96vw" // Ensure the container takes full screen width
            sx={{ backgroundColor: "#4E4E4E", margin: 0 }}
        >
            {/* Left sidebar for channels */}
            <Box
                display="flex"
                flexDirection="column"
                alignItems="center"
                mt="20px"
                ml="16px"
                sx={{
                    backgroundColor: "#4E4E4E",
                    width: "17vw",  // Fixed width for left sidebar
                    flexShrink: 0,
                }}
            >
                <Typography variant="h6">
                    {serverStore.selectedServer?.name}
                </Typography>

                <Divider sx={{ width: '80%', borderColor: 'gray', my: 1 }} />

                {channelStore.channels.map((channel) => (
                    <Box
                        key={channel.channelId}
                        display="flex"
                        justifyContent="center"
                        alignItems="center"
                        width="90%"
                        height="30px"
                        sx={{
                            backgroundColor: "#222222",
                            borderRadius: "15px",
                            mb: "10px",
                            '&:hover': {
                                backgroundColor: "#2A2A2A",
                                cursor: "pointer",
                            },
                        }}
                        onClick={() => handleClick(channel.channelId)}
                    >
                        <Typography variant="body1">
                            {channel.name}
                        </Typography>
                    </Box>
                ))}

                <Divider sx={{ width: '80%', borderColor: 'gray', my: 1 }} />
                <Box display="flex" width="100%" justifyContent="center">
                    <AddChannelButton serverId={serverId} />
                </Box>
            </Box>

            {/* Main content area */}
            <Box
                display="flex"
                sx={{
                    flexGrow: 1,  // This ensures the content takes up the remaining width
                    backgroundColor: "#1B1B1B",
                    height: "100%",
                    width: "60vw", // Adjusted width for main content area
                }}
            >
                <Outlet />
            </Box>

            {/* Server members sidebar (on the right) */}
            <Box
                display="flex"
                flexDirection="column"
                sx={{
                    width: sideBarOpen
                        ?  "15vw"
                        : "5vw"
                    ,  // Fixed width for right sidebar
                    backgroundColor: "#3A3A3A",
                    overflowY: "auto",
                    marginLeft: "auto", // To ensure it is on the right
                }}
            >
                <ServerMembers isOpen={sideBarOpen} setIsOpen={(value:boolean)=>setSideBarOpen(value) } />
            </Box>
        </Box>
    );
});