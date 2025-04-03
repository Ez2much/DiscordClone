import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../API/agent';
import { Server } from '../Models/Server';
import { ServerCreateDto } from '../Models/ServerCreate';
import { User } from '../Models/user';
import ServerBan from '../Models/ServerBan';
import UnbanDto from '../Models/UnbanDto';
import { ServerMember } from '../Models/ServerMember';

export class ServerStore {
    constructor() {
        makeAutoObservable(this);
    }
    servers: Server[] = [];
    selectedServer: Server | null = null;
    loading: boolean = false;
    serverMembers: ServerMember[] = [];


    getServers = () => this.servers;
    setServers = (servers: Server[]) => {
        runInAction(() =>
            this.servers = servers
        );
    };

    setSelectedServer = (server: Server) => {
        this.selectedServer = server;
    };

    setLoading = (loading: boolean) => {
        this.loading = loading;
    };

    getServersApi = async (userId: string) => {
        this.setLoading(true);
        try {
            const response = await agent.Servers.GetServersByUserId(userId);
            this.setServers(response);
        } catch (error) {
            console.log(`Cannot load servers`);
        } finally {
            this.setLoading(false);
        }
    };

    createServerApi = async (serverCreate: ServerCreateDto) => {
        this.setLoading(true);
        try {
            agent.Servers.CreateServer(serverCreate).then(response => {this.joinServerApi(serverCreate.ownerId, response.serverId); this.addServer(response); }); 
        } catch (error) {
            console.log(`Cannot create server with provided data ${JSON.stringify(serverCreate)} error: ${error}`,error);
        } finally {
            this.setLoading(false);
        }
    };
    joinServerApi = async (userId: string, serverId: string) => {
        this.setLoading(true);
        try {
            await agent.Servers.JoinServer(userId, serverId);
        } catch (error) {
            console.log(`Cannot join server with provided data ${userId} ${serverId} error: ${error}`,error);
        } finally {
            this.setLoading(false);
        }
    }
    leaveServerApi = async (userId: string, serverId: string) => {
        this.setLoading(true);
        try {
            await agent.Servers.LeaveServer(userId, serverId);
        } catch (error) {
            console.log("Cannot leave server with provided data", userId, serverId, error);
        }
        finally {
            this.setLoading(false);
        }
    }
    deleteServerApi = async (userId: string, serverId: string) => {
        this.setLoading(true);
        try {
            await agent.Servers.DeleteServer(userId, serverId);
        } catch (error) {
            console.log("Cannot delete server with provided data", userId, serverId, error);
        }
        finally {
            this.setLoading(false);
        }
    }
    banUserApi = async (serverBan: ServerBan) => {
        this.setLoading(true);
        try {
            await agent.Servers.BanUser(serverBan);
        } catch (error) {
            console.log("Cannot ban user with provided data", serverBan, error);
        }
        finally {
            this.setLoading(false);
        }
    }
    unbanUserApi = async (unbanDto: UnbanDto) => {
        this.setLoading(true);
        try {
            await agent.Servers.UnBanUser(unbanDto);
        } catch (error) {
            console.log("Cannot unban user with provided data", unbanDto, error);
        }
        finally {
            this.setLoading(false);
        }
    }
    getServerApi = async (serverId: string) => {
        this.setLoading(true);
        try {
            const response = await agent.Servers.GetServerById(serverId);
            this.setSelectedServer(response);
        } catch (error) {
            console.log(`Cannot get server with provided data ${serverId} error: ${error}`,error);
        } finally {
            this.setLoading(false);
        }
    }
    addServer = (server: Server) => {
        this.servers.push(server);
    };
    // W ServerStore
    fetchServerMembers = async (serverId: string) => {
        this.setLoading(true);
        try {
            const response = await agent.Servers.GetServerMembers(serverId);
            console.log('API Response for Server Members:', response);  // Sprawdź odpowiedź API

            // Jeśli odpowiedź to tablica, po prostu przypisz ją do stanu MobX
            if (Array.isArray(response)) {
                runInAction(() => {
                    this.serverMembers = response.map((member: any) => ({
                        id: member.id,
                        username: member.username,
                        email: member.email,
                        image: member.image,
                    }));
                });
            } else {
                console.log('Odpowiedź API nie zawiera tablicy:', response);
            }
        } catch (error) {
            console.log(`Cannot fetch server members for server ${serverId}`, error);
            runInAction(() => {
                this.serverMembers = [];
            });
        } finally {
            this.setLoading(false);
        }
    };



}
