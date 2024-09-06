class ApiResponse<T> {
	public value?: T;
	private statusCode: number;
	private errorMessage?: string;

	constructor(status: number) {
		this.statusCode = status;
	}

	public get ok(): boolean {
		return this.statusCode < 400;
	}

	public get error() {
		return this.errorMessage;
	}

	public setError(errorObj: { [key: string]: string }): void {
		try {
			// const errorObj = (await response.json()) as { [key: string]: string };
			this.errorMessage = Object.entries(errorObj)
				.map(([key, value]) => `${key}: ${value}`)
				.join(", ");
		} catch (error) {
			console.error("Failed to parse error response:", error);
			this.errorMessage = "Unknown error occurred";
		}
	}


}

export default ApiResponse;
