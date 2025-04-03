import axios, { AxiosResponse, InternalAxiosRequestConfig } from 'axios';
import { store } from '../stores/store';
import { LoginModel } from '../Models/LoginModel';
import { User } from '../Models/user';
import ApiResponseModel from '../Models/ApiResponseModel';
import RegisterModel from '../Models/RegisterModel';
import { ServerCreateDto } from '../Models/ServerCreate';
import ServerBan from '../Models/ServerBan';
import UnbanDto from '../Models/UnbanDto';
import ChannelCreateDto from '../Models/ChannelCreate';
import { Channel } from '../Models/Channel';
import  Message  from '../Models/message';
import FriendRequest from '../Models/FriendRequest';
import FriendsUsernameRequest from '../Models/FriendsUsernameRequest';
import PrivateMessage from '../Models/PrivateMessage';


axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use((config: InternalAxiosRequestConfig) => {

    if (config.headers && !config.headers['NoAuth']) {
        const token = store.userStore.getToken();
        token ? token : localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
    }
    return config;
});

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: <T>(url: string, noAuth = false) =>
        axios.get<T>(url, { headers: { NoAuth: noAuth } }).then(responseBody),
    post: <T>(url: string, body: {}, noAuth = false) =>
        axios.post<T>(url, body, { headers: { NoAuth: noAuth } }).then(responseBody),
    put: <T>(url: string, body: {}, noAuth = false) =>
        axios.put<T>(url, body, { headers: { NoAuth: noAuth } }).then(responseBody),
    delete: <T>(url: string, noAuth = false) =>
        axios.delete<T>(url, { headers: { NoAuth: noAuth } }).then(responseBody),
};


const Auth = {
    login: (loginModel: LoginModel, noAuth = false) => requests.post<ApiResponseModel>('/auth/login', loginModel, noAuth),
    logout: ( noAuth = false) => requests.post<void>('/auth/logout', noAuth),
};

const Servers = {
    CreateServer: (serverCreate: ServerCreateDto, noAuth = false) => requests.post<ApiResponseModel>('server/create', serverCreate,noAuth),
    JoinServer: (userId: string, serverId: string, noAuth = false) => requests.post<ApiResponseModel>(`server/join/${userId}/${serverId}`, noAuth),
    LeaveServer: (userId: string, serverId: string, noAuth = false) => requests.post<ApiResponseModel>(`server/leave/${userId}/${serverId}`, noAuth),
    DeleteServer: (userId: string, serverId: string, noAuth = false) => requests.delete<ApiResponseModel>(`server/delete/${userId}/${serverId}`, noAuth),
    BanUser: (serverBan: ServerBan, noAuth = false) => requests.post<ApiResponseModel>('server/ban', serverBan, noAuth),
    UnBanUser: (unbanDto: UnbanDto, noAuth = false) => requests.post<ApiResponseModel>('server/unban', unbanDto, noAuth),
    GetServersByUserId: (userId: string, noAuth = false) => requests.get<ApiResponseModel>(`server/user/${userId}`, noAuth),
    GetServerById: (serverId: string, noAuth = false) => requests.get<ApiResponseModel>(`server/${serverId}`, noAuth),

    GetServerMembers: (serverId: string, noAuth = false) =>
        requests.get<ApiResponseModel>(`server/members/${serverId}`, noAuth)
}

const Messages = {
    SendMessage: (messageDto: Message, noAuth = false) => requests.post<ApiResponseModel>('message/send', messageDto, noAuth),
    SendPrivateMessage:(messageDto: PrivateMessage, noAuth = false) => requests.post<ApiResponseModel>('message/send/private',messageDto, noAuth),
    GetAllMessages: (channelId: string, noAuth = false) => requests.get<ApiResponseModel>(`message/${channelId}`, noAuth),
    GetMessagesFromLastDays: (channelId: string, days: number, noAuth = false) => requests.get<ApiResponseModel>(`message/${channelId}/last/${days}`, noAuth),
    GetPrivateMessagesFromNDays: (user1: string, user2: string, days: number, noAuth = false) => requests.get<ApiResponseModel>(`message/private/${user1}/${user2}/${days}`, noAuth),
}
const agent = {
    Auth,
    Users,
    Servers,
    Channels,
    Messages,
    Friends,
};

export default agent;
