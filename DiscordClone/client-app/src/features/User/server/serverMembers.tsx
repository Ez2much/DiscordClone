import { Typography, Box, Avatar, IconButton } from "@mui/material"
import { observer } from "mobx-react-lite"
import { useStore } from "../../../app/stores/store";

import GroupsIcon from '@mui/icons-material/Groups';

interface Props {
    isOpen: boolean
    setIsOpen: Function
}

export default observer(function ServerMembers({ isOpen, setIsOpen }: Props) {

    const { serverStore } = useStore();

    return (
        <>
            <Box display="flex" flexDirection="column" padding="5px" flexWrap="wrap" gap="10px">
                {isOpen
                    ?
                    <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center">
                        <Box>
                            <IconButton onClick={() => setIsOpen(!isOpen)}>
                                <GroupsIcon />
                            </IconButton>
                        </Box>
                        <Typography variant="h6" sx={{ mb: 2, color: "white" }}>
                           Members
                        </Typography>
                    </Box>
                    :
                    <Box>
                        <IconButton onClick={() => setIsOpen(!isOpen)}>
                            <GroupsIcon />
                        </IconButton>
                    </Box>
                    
                }
                {serverStore.serverMembers.length === 0 ? (
                    <Typography>No members available</Typography>
                ) : (
                    serverStore.serverMembers.map((member) => (
                        <Box
                            key={member.id}
                            display="flex"

                            sx={{
                                mb: "10px",
                                p: "5px",
                                borderRadius: 2,
                                '&:hover': {
                                    backgroundColor: "#4A4A4A"
                                }
                            }}
                        >
                            <Avatar
                                sx={{
                                    width: 32,
                                    height: 32,
                                    mr: 2
                                }}
                                src={member.image || "/public/user.png"}
                            >

                            </Avatar>
                            {isOpen ? <Typography>{member.username || 'Unnamed'}</Typography> : (<></>)}
                        </Box>
                    ))
                    )}
            </Box>
        </>

    )
})