import React, {createContext, useReducer} from "react";
import {StorageService} from "../services";
import {StorageTypes} from "../constants";

interface AuthContextProps {
    state: {
        accessToken: string;
        refreshToken: string;
    }
    dispatch: React.Dispatch<any>;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

function authReducer(state: any, action: any) {
    switch (action.type) {
        case "authentication": {
            return {
                ...state,
                accessToken: action.accessToken,
                refreshToken: action.refreshToken,
            };
        }
        case "logout": {
            return {};
        }
        default: {
            throw new Error(`Unhandled action type: ${action.type}`);
        }
    }
}

function AuthProvider({children}: any) {
    const auth = StorageService.getLocalStorage(StorageTypes.AUTH) ?? {};
    const [state, dispatch] = useReducer(authReducer, auth);
    const value = {state, dispatch};

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

function useLogin() {
    const context = React.useContext(AuthContext);
    if (context === undefined) {
        throw new Error("useLogin must be used within a AuthProvider");
    }
    return context;
}

export default {AuthProvider, useLogin};