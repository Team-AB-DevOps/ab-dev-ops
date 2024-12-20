import IUser from "@/models/User";
import ApiClient from "./ApiClient";

type TRegisterRequest = { username: string; email: string; password: string; password2: string };
type TLoginRequest = { username: string; password: string };
type TChangePasswordRequest = {
	currentPassword: string;
	newPassword: string;
	newPassword2: string;
};

export type LoginResponse = { user: IUser; token: string };

class UsersEndpoint {
	static async Register(user: TRegisterRequest): Promise<IUser> {
		const resp = await new ApiClient().Post<IUser, TRegisterRequest>(`api/register`, user);

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value!;
	}

	static async Login(user: TLoginRequest): Promise<LoginResponse> {
		const resp = await new ApiClient().Post<LoginResponse, TLoginRequest>(`api/login`, user);

		if (!resp.ok) {
			console.log("ERROR");
			throw new Error(resp.error);
		}

		return resp.value!;
	}

	static async UpdatePassword(id: number, body: TChangePasswordRequest) {
		const resp = await new ApiClient().Patch<null, TChangePasswordRequest>(`api/users/${id}`, body);

		if (!resp.ok) {
			throw new Error(resp.error)
		}
	}
}

export default UsersEndpoint;
