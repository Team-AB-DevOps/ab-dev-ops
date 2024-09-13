import Button from "@/components/core/Button";
import { TextInput } from "@/components/core/Input";
import UsersEndpoint from "@/services/UsersEndpoint";
import React from "react";
import { useLocation, useNavigate } from "react-router-dom";

const INIT_FORM = {
	username: "",
	password: "",
};

function LoginPage() {
	const { registered } = useLocation().state || false;
    const [loginForm, setLoginForm] = React.useState(INIT_FORM);
    const [errorMessage, setErrorMessage] = React.useState("");
	const navigate = useNavigate();

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const { name, value } = e.target;
		setLoginForm((prev) => ({ ...prev, [name]: value }));
	};

	const handleSubmit = () => {
		UsersEndpoint.Login({...loginForm})
			.then(() => navigate("/"))
			.catch((e: Error) => setErrorMessage("Wrong username and/or password"));
	};

	return (
		<>
			<div>
				{registered && <div className="bg-teal-300 p-1 border text-sm mb-5">You Were successfully registered and can login now</div>}
				<h3 className="text-xl font-bold mb-3">Log In</h3>
				<div className="flex flex-col items-center gap-4">
					<div className="flex flex-col">
						<TextInput required name="username" value={loginForm.username} onChange={handleChange} label="Username:" />
					</div>

					<div className="flex flex-col">
						<TextInput required name="password" value={loginForm.password} onChange={handleChange} label="Password:" type="password" />
					</div>

					<div className="flex flex-col gap-3 justify-center items-center">
						<Button onClick={handleSubmit}>Log In</Button>
						{errorMessage ?? (
							<div>
								<p className="text-red-800">{errorMessage}</p>
							</div>
						)}
					</div>
				</div>
			</div>
		</>
	);
}

export default LoginPage;
