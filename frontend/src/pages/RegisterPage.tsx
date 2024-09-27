import Button from "@/components/core/Button";
import { TextInput } from "@/components/core/Input";
import UsersEndpoint from "@/services/UsersEndpoint";
import React from "react";
import { useNavigate } from "react-router-dom";

const INIT_FORM = {
	username: "",
	email: "",
	password: "",
	passwordRepeat: "",
};

function RegisterPage() {
	const [registerForm, setRegisterForm] = React.useState(INIT_FORM);
	const [errorMessage, setErrorMessage] = React.useState("");
	const navigate = useNavigate();

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const { name, value } = e.target;
		setRegisterForm((prev) => ({ ...prev, [name]: value }));
	};

	const handleSubmit = () => {
		const { username, email, password, passwordRepeat } = registerForm;

		if (password !== passwordRepeat) {
			setErrorMessage("Password is not matching.");
			return;
		}

		if (!email.includes("@")) {
			setErrorMessage("Please insert a valid email.");
			return;
		}

		const body = { username, email, password, password2: password };

		UsersEndpoint.Register(body)
			.then(() => navigate("/login", { state: { redirected: true } }))
			.catch(() => console.log("Could not register"));
	};

	return (
		<div>
			<h3 className="text-xl font-bold mb-3 text-center">Sign Up</h3>
			<div className="flex flex-col items-center gap-4">
				<div className="flex flex-col">
					<TextInput required name="username" value={registerForm.username} onChange={handleChange} label="Username:" />
				</div>
				<div className="flex flex-col">
					<TextInput required name="email" value={registerForm.email} onChange={handleChange} label="E-mail:" type="email" />
				</div>
				<div className="flex flex-col">
					<TextInput required name="password" value={registerForm.password} onChange={handleChange} label="Password:" type="password" />
				</div>
				<div className="flex flex-col">
					<TextInput
						required
						name="passwordRepeat"
						value={registerForm.passwordRepeat}
						onChange={handleChange}
						label="Password (repeat):"
						type="password"
					/>
				</div>
				<div className="flex flex-col gap-3 justify-center items-center">
					<Button onClick={handleSubmit}>Sign up</Button>
					{errorMessage ?? (
						<div>
							<p className="text-red-800">{errorMessage}</p>
						</div>
					)}
				</div>
			</div>
		</div>
	);
}

export default RegisterPage;
