import IPage from "@/models/Page";
import ApiClient from "./ApiClient";


class PagesEndpoint {

	static async getPages(searchValue: string, language: string = "en"): Promise<IPage[]> {
		const resp = await new ApiClient().Get<IPage[]>("api/search");

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value ?? [];
	}
}

export default PagesEndpoint;
