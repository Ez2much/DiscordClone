import { Box, Button, Typography } from '@mui/material';
import './styles.css'
import { Link } from 'react-router-dom';
import { useStore } from '../stores/store';

export default  function HomePage() {

    const { userStore } = useStore();


	return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
                height: "100vh",
                width: "100vw",
                position: "absolute",
                top: 0,
            }}>
            <Typography variant="h1" fontStyle="italic">Welcome</Typography>

            {userStore.isLoggedIn ? (
                <Link to='/main'>
                    <Button>Go to Main</Button>
                </Link>
            ) : (
                <Link to='/login'>
                    <Button>Go to Login</Button>
                </Link>
            )}
        </Box>
	)
}