import { Dialog, Box, Typography, TextField, Button, Checkbox } from "@mui/material";
import { useStore } from "../../../app/stores/store";
import { useState } from "react";
import { ServerCreateDto } from "../../../app/Models/ServerCreate";
import { observer } from "mobx-react-lite";


interface Props {
    open: boolean;
    onClose: () => void;
}

export default observer(function JoinServerDialog({ open, onClose }: Props) {

    const { serverStore, userStore, signalRStore } = useStore();

    const [join, setJoin] = useState<boolean>(true);
    const [joinServerId, setJoinServerId] = useState<string>("");
    const [serverCreate, setServerCreate] = useState<ServerCreateDto>({
        ownerId: userStore.user!.id,
        name: "",
        description: "",
        iconUrl: "",
        isPublic: true,
    });

    const handleJoin = async () => {
        await serverStore.joinServerApi(userStore.user.id, joinServerId);
        await signalRStore.joinChannel(serverStore.servers.find(x => x.serverId === joinServerId)!.name, "Default");
        await serverStore.getServersApi(userStore.user!.id);
        onClose();
    };
    const handleCreate = async () => {
        await serverStore.createServerApi(serverCreate);
        onClose();
    };

    return(
        <Dialog open={open} onClose={onClose}>
            <Box display="flex" flexDirection="column" alignItems="center" justifyContent="center" width="300px" height={join ? "300px" : "400px"}>
                {join ? (
                    <Box display="flex" flexDirection="column" alignItems="center" justifyContent="center" p="10px 20px" gap="20px">
                        <Typography variant="h6" alignSelf="center">Join Server</Typography>
                        <TextField label="Server Id" value={joinServerId} onChange={(e) => setJoinServerId(e.target.value)} />
                        <Button variant="contained" color="success" onClick={handleJoin}>Join</Button>
                        <Box
                            sx={{ cursor: "pointer" }}
                            onClick={() => setJoin(false)}
                        >
                            <Typography variant="body2" >Want to create server?</Typography>
                        </Box>
                    </Box>
                ) : (
                    <Box display="flex" flexDirection="column" alignItems="center" justifyContent="center" p="10px 20px" gap="20px">
                        <Typography variant="h6" alignSelf="center">Create Server</Typography>
                        <TextField label="Server Name*" value={serverCreate.name} onChange={(e) => setServerCreate({ ...serverCreate, name: e.target.value })} />
                        <TextField label="Server Description" value={serverCreate.description} onChange={(e) => setServerCreate({ ...serverCreate, description: e.target.value })} />
                        <Box display="flex" alignItems="center" justifyItems="center">
                            <Typography variant="body2">Is public?</Typography>
                            <Checkbox checked={serverCreate.isPublic} color="success" onChange={(e) => setServerCreate({ ...serverCreate, isPublic: e.target.checked })} />
                        </Box>
                        <Button variant="contained" color="success" onClick={handleCreate}>Create Server</Button>
                        <Box
                            sx={{ cursor: "pointer" }}
                            onClick={() => setJoin(true)}
                        >
                            <Typography variant="body2" >Join server instead</Typography>
                        </Box>
                    </Box>
                )}
            </Box>
        </Dialog>
    )
});