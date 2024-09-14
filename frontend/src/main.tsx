import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { BrowserRouter } from "react-router-dom";
import AuthProvidor from "./auth/AuthProvidor.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
		<BrowserRouter>
			<AuthProvidor>
				<App />
			</AuthProvidor>
		</BrowserRouter>
	</React.StrictMode>,
);
