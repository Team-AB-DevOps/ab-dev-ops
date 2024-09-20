import IWeather from "@/models/IWeather";
import ApiClient from "./ApiClient";

class WeatherEndpoint {
	static async getPages(): Promise<IWeather> {
		const resp = await new ApiClient().Get<IWeather>("api/weather");

		if (!resp.ok) {
			throw new Error(resp.error);
		}

		return resp.value!;
	}
}

export default WeatherEndpoint;
