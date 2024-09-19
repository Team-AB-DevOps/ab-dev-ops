import IPage from "@/models/Page";
import ApiClient from "./ApiClient";

type TPagesRequest = {
	q: string,
	language?: string
}

class PagesEndpoint {

	static async getPages(searchValue: string, language: string = "en"): Promise<IPage[]> {
		const body: TPagesRequest = {
			q: searchValue,
			language: language
		}
		const resp = await new ApiClient().Post<IPage[], TPagesRequest>("api/search", body);

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value ?? [];
	}
}

export default PagesEndpoint;
