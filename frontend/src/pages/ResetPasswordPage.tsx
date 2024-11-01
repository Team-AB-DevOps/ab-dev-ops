import useAuth from "@/auth/UseAuth";
import Button from "@/components/core/Button";
import { TextInput } from "@/components/core/Input";
import UsersEndpoint from "@/services/UsersEndpoint";
import React from "react";
import { useNavigate } from "react-router-dom";

const INIT_FORM = {
	currentPassword: "",
	newPassword: "",
	newPassword2: "",
};

function ResetPasswordPage() {
	const [passwordForm, setPasswordForm] = React.useState(INIT_FORM);
    const [errorMessage, setErrorMessage] = React.useState("");
	const navigate = useNavigate();
    const auth = useAuth();


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
		setPasswordForm((prev) => ({ ...prev, [name]: value }));
    };
    
    const handleSubmit = async () => {
        try {
            await UsersEndpoint.UpdatePassword(auth.user!.id, passwordForm);
            navigate("/");
        } catch (error) {
            setErrorMessage((error as Error).message);
        }
	};

	return (
		<div>
			<h3 className="text-xl font-bold mb-3 text-center">Change Password</h3>
			<div className="flex flex-col items-center gap-4">
				<div className="flex flex-col mb-5">
					<TextInput
						required
						name="currentPassword"
						value={passwordForm.currentPassword}
						onChange={handleChange}
						label="Current Password:"
						type="password"
					/>
				</div>

				<div className="flex flex-col">
					<TextInput required name="newPassword" value={passwordForm.newPassword} onChange={handleChange} label="New Password:" type="password" />
				</div>

				<div className="flex flex-col">
					<TextInput
						required
						name="newPassword2"
						value={passwordForm.newPassword2}
						onChange={handleChange}
						label="New Password repeated:"
						type="password"
					/>
				</div>

				<div className="flex flex-col gap-3 justify-center items-center">
					<Button onClick={handleSubmit}>Confirm change</Button>
					{errorMessage && (
						<div>
							<p className="text-red-800">{errorMessage}</p>
						</div>
					)}
				</div>
			</div>
		</div>
	);
}

export default ResetPasswordPage;
