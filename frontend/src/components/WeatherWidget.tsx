import IWeather from "@/models/IWeather";

interface Props {
	weather: IWeather | null;
}

export default function WeatherWidget({ weather }: Props) {
	console.log(weather);

	return (
		<div className="flex gap-2 justify-center items-center w-52 h-24 bg-stone-100 rounded-md m-2 p-2 shadow-md outline outline-1 outline-slate-400">
			<div>
				<img className="drop-shadow-sm size-20" src={weather?.current.condition.icon} alt="" />
			</div>
			<div className="text-center text-sm">
				<div>{weather?.location.country}</div>
				<div>{weather?.location.region}</div>
				<div className="font-bold">{weather?.current.temp_c}°C</div>
			</div>
		</div>
	);
}
