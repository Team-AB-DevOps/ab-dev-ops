import IUser from "@/models/User";
import React from "react";

export interface AuthContextInterface {
	user?: IUser;
	isAuthenticated: boolean;
	logout(): void;
	getAccessToken(): Promise<string>;
	login(credentials: { username: string; password: string }): void;
}

const AuthContext = React.createContext<AuthContextInterface>(null!);

export default AuthContext;
