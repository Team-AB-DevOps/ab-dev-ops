import ApiResponse from "./ApiResponse.ts";

export type TParams = {
	[key: string]: string;
};

class ApiClient {
	private baseUrl: URL;

	public constructor() {
		const baseUrl = import.meta.env.VITE_API_BASE_URL;
		if (!baseUrl) throw new Error("Environment variable VITE_API_BASE_URL missing");

		this.baseUrl = new URL(baseUrl);
	}


	public async Get<T>(path: string, params?: TParams): Promise<ApiResponse<T>> {
		const requestUrl = this.getURL(path, params);
		const response = await fetch(requestUrl);

		return this.handleResponse<T>(response);
	}

	public async Post<T, R>(path: string, data: R): Promise<ApiResponse<T>> {
		const req: RequestInit = {
			body: JSON.stringify(data),
			method: "POST",
			headers: { "Content-Type": "application/json" },
		};
		const requestUrl = this.getURL(path);
		const response = await fetch(requestUrl, req);

		return this.handleResponse<T>(response);
	}

	public async Put<T, R>(path: string, data: R): Promise<ApiResponse<T>> {
		const req: RequestInit = {
			body: JSON.stringify(data),
			method: "PUT",
			headers: { "Content-Type": "application/json" },
		};
		const requestUrl = this.getURL(path);
		const response = await fetch(requestUrl, req);

		return this.handleResponse<T>(response);
	}

	public async Delete<T>(path: string): Promise<ApiResponse<T>> {
		const req: RequestInit = {
			method: "DELETE",
		};
		const requestUrl = this.getURL(path);
		const response = await fetch(requestUrl, req);

		return this.handleResponse<T>(response);
	}

	public async Patch<T, R>(path: string, data: R): Promise<ApiResponse<T>> {
		const req: RequestInit = {
			body: JSON.stringify(data),
			method: "PATCH",
			headers: { "Content-Type": "application/json" },
		};
		const requestUrl = this.getURL(path);
		const response = await fetch(requestUrl, req);

		return this.handleResponse<T>(response);
	}

	private getURL(path: string, params?: TParams): URL {
		const url = new URL(this.baseUrl);
		url.pathname = path;

		if (params) {
			Object.entries(params).forEach(([key, value]) => url.searchParams.append(key, value));
		}

		return url;
	}

	private async handleResponse<T>(rawResponse: Response): Promise<ApiResponse<T>> {
		const apiResponse = new ApiResponse<T>(rawResponse.status);
		const body = await rawResponse.json()

		if (rawResponse.ok) {
			apiResponse.value = body;
		} else {
			apiResponse.setError(body);
		}

		return apiResponse;
	}

}

export default ApiClient;
