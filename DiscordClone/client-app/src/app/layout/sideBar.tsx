import { useStore } from "../stores/store";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import JoinServerDialog from "../../features/User/server/joinServerDialog";
import FriendRequestsDialog from "../../features/User/friends/friendRequestsDialog";
import { Drawer, Box, Typography } from "@mui/material";

export default observer(function SideBar() {
    const { userStore, serverStore, friendStore } = useStore();
    
    const [openServer, setOpenServer] = useState<boolean>(false);
    const [openFriends, setOpenFriends] = useState<boolean>(false);
    useEffect(() => {
        if (serverStore.servers.length === 0)
            serverStore.getServersApi(userStore.user!.id).then();

    }, [userStore,serverStore]);


    return (
        <>
            <Drawer variant="permanent">
                <Box sx={{ display: 'flex', flexDirection: 'column', height: "95%" }} justifyContent="space-between">
                    <Box display="flex" flexDirection="column" gap="10px">
                        <Link to={`/main`}>
                            <Box sx={{
                                borderRadius: "20px", height: "50px", width: "50px", fontSize: "10px", display: "flex",
                                alignItems: "center", justifyContent: "center", color: "white",
                                backgroundImage: `url(${userStore.getUser()!.image})`, backgroundSize: "cover", backgroundPosition: "center",
                            }}>
                                {userStore.user?.username}
                            </Box>
                        </Link>

                        {serverStore.loading ? (null) :
                            (serverStore.servers?.map((server) => (
                                <Link to={`/server/${server.serverId}`} style={{ textDecoration: "none", textAlign: "center" }} key={server.serverId}>
                                    <Box sx={{
                                        display: "flex", alignItems: "center", justifyContent: "center",
                                        height: "50px", width: "50px", fontSize: "10px", color: "black",
                                        cursor: "pointer", backgroundColor: "lightgray", borderRadius: "20px",
                                    }}>
                                        {server.name}
                                    </Box>
                                </Link>
                            )))}
                    </Box>
                    <Box position="relative" display="flex" flexDirection="column" gap="10px">
                        <Box sx={{
                            borderRadius: "20px", height: "50px", width: "50px", fontSize: "10px", display: "flex",
                            alignItems: "center", justifyContent: "center", color: "black", textAlign:"center",
                            cursor: "pointer", backgroundColor: "lightgray",
                        }}
                            onClick={() => setOpenFriends(true)}
                        >
                            <Typography component="p" variant="caption">Add Friends</Typography>
                            {friendStore.friendRequests.length === 0 ? (null) :
                                <Box position="absolute" width="10px" height="10px" borderRadius="50%"
                                    sx={{
                                        backgroundColor: friendStore.friendRequests.length === 0 ? "transparent" : "red",
                                        color: "white", fontSize: "8px", display: "flex",
                                        alignItems: "center", justifyContent: "center",
                                        top: "0px", right: "0px"
                                    }}
                                >
                                    {friendStore.friendRequests.length}
                                </Box>
                            }
                        </Box>

                        <Box sx={{
                            borderRadius: "20px", height: "50px", width: "50px", fontSize: "10px", display: "flex",
                            alignItems: "center", justifyContent: "center", color: "black",
                            cursor: "pointer", backgroundColor: "lightgray",
                        }}
                            onClick={() => setOpenServer(true)}
                        >
                            Servers
                        </Box>
                    </Box>
                </Box>
            </Drawer>
            <JoinServerDialog open={openServer} onClose={() => setOpenServer(false)} />
            <FriendRequestsDialog open={openFriends} onClose={() => setOpenFriends(false)} />
            
        </>
    );
});
