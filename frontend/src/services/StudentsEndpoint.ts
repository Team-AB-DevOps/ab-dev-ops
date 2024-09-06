import ApiClient, { TParams } from "@/services/ApiClient.ts";
import { IPagination } from "@/models/IPagination.ts";

class StudentsEndpoint {
	static async getStudents(): Promise<Array<any>> {
		const resp = await new ApiClient().Get<Array<any>>("students");

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value ?? [];
	}

	static async getStudentById(id: number): Promise<any> {
		const resp = await new ApiClient().Get<any>(`students/${id}`);

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value;
	}

	static async deleteStudentById(id: number): Promise<any> {
		const resp = await new ApiClient().Delete(`students/${id}`);

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value;
	}

	static async testPagination(): Promise<IPagination<any>> {
		const resp = await new ApiClient().Get<IPagination<any>>("students");

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value as IPagination<any>;
	}
}

export default StudentsEndpoint;
