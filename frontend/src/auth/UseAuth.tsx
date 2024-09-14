import React from "react";
import AuthContext, { AuthContextInterface } from "./AuthContext";

const useAuth = (): AuthContextInterface => {
	return React.useContext(AuthContext);
};

export default useAuth;
