import { createContext, useContext } from "react";

import UserStore from "./userStore";
import SignalRStore from "./SignalRStore";
import { ServerStore } from './serverStore';
import { ChannelStore } from "./channelStore";
import FriendStore from "./friendStore";
interface Store {
    userStore: UserStore,
    signalRStore: SignalRStore,
    serverStore: ServerStore,
    channelStore: ChannelStore,
    friendStore: FriendStore,
}

export const store: Store = {
    userStore: new UserStore(),
    signalRStore: new SignalRStore(),
    serverStore: new ServerStore(),
    channelStore: new ChannelStore(),
    friendStore: new FriendStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}