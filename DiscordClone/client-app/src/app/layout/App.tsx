import { useEffect } from 'react';
import { useStore } from '../stores/store';
import HomePage from './homePage';
import NavBar from './navBar';
import './styles.css';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';
import AdminPanel from '../../features/Admin/AdminPanel';
import { ThemeProvider } from '@emotion/react';
import theme from '../theme/theme';
import { Box } from '@mui/material';
import SideBar from './sideBar';

function App() {

    const location = useLocation();
    const { userStore } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        if (location.pathname === '/main' && !userStore.getLoggedIn()) {
            navigate('/login');
        }
    }, [location.pathname, userStore, navigate]);

    return (
        <ThemeProvider theme={theme}>
        <>
            {location.pathname === '/' ? <HomePage /> : (
                <>
                    {location.pathname !== '/login' ? (
                        <>
                            {userStore.user?.role === 'Admin' ? (
                                <AdminPanel />
                                ) : (
                                <>
                                    <Box>
                                        <NavBar />
                                    </Box>
                                    <Box>
                                        <SideBar />
                                            </Box>
                                     <Box pl="62px" mt="62px">
                                        <Outlet />
                                    </Box>
                                </>
                            )}
                        </>
                    ) : (
                        <Outlet />
                    )}
                </>
            )}
            </>
        </ThemeProvider>
    );
}

export default App;
