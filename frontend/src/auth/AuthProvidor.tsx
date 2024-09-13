import IUser from "@/models/User";
import React from "react";
import AuthContext, { AuthContextInterface } from "./AuthContext";
import UsersEndpoint from "@/services/UsersEndpoint";

interface AuthProps extends React.PropsWithChildren {}

function AuthProvidor(props: AuthProps) {
	const [user, setUser] = React.useState<IUser>();
	const [isAuthenticated, setIsAuthenticated] = React.useState(false);

	const logout = React.useCallback((): void => {
		_clearCache();
		setUser(undefined);
		setIsAuthenticated(false);
		console.log("logout completed");
	}, []);

	const login = React.useCallback((credentials: { username: string; password: string }) => {
		try {
            UsersEndpoint.Login(credentials).then((r) => {
                //TODO: Gem access token.
                _setCache("ACCESS_TOKEN", "Min acces token");
                setUser({ username: r.username, email: r.email })
                setIsAuthenticated(true);
			});
		} catch (error) {
			throw new Error("Wrong username and/or password");
		}
    }, []);
    
    const getAccessToken = React.useCallback(async (): Promise<string> => {
        const cachedToken = _getCache("ACCESS_TOKEN");

        if (!cachedToken) {
            throw new Error("ACCESS_TOKEN is not set");
        }

        return cachedToken;
    }, []);

	const contextValue = React.useMemo<AuthContextInterface>(() => {
		return {
			user,
			isAuthenticated,
			logout,
			getAccessToken,
			login,
		};
	}, [user, isAuthenticated, logout, getAccessToken, login]);

	return (
		<>
			<AuthContext.Provider value={contextValue}>{props.children}</AuthContext.Provider>
		</>
	);
}

function _clearCache() {
	localStorage.removeItem("ACCESS_TOKEN");
}

function _setCache(key: string, value: string, expiryDate?: Date) {
	const json = JSON.stringify({ value, expiryTime: expiryDate?.getTime() });
	const cacheValue = btoa(json); // Encode base64
	localStorage.setItem(key, cacheValue);
}

function _getCache(key: string): string | null {
	const value = localStorage.getItem(key);
	if (value == null) return null;

	const json = atob(value); // Decode base64
	const cacheItem: { value: string; expiryTime?: number } = JSON.parse(json);

	if (cacheItem.expiryTime != null) {
		if (new Date(cacheItem.expiryTime) < new Date()) {
			localStorage.removeItem(key);
			return null;
		}
	}

	return cacheItem.value;
}

export default AuthProvidor;
