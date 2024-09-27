import useAuth from "@/auth/UseAuth";
import { Link, useLocation, useNavigate } from "react-router-dom";
import WeatherWidget from "../WeatherWidget";
import React from "react";
import WeatherEndpoint from "@/services/WeatherEndpoint";
import IWeather from "@/models/IWeather";

export default function Header() {
	const { loggedOut } = useLocation().state || false;
	const { user, isAuthenticated, logout } = useAuth();
	const [weather, setWeather] = React.useState<IWeather | null>(null);

	const navigate = useNavigate();

	const handleClick = () => {
		logout();
		navigate("/", { state: { loggedOut: true } });
	};

	React.useEffect(() => {
		WeatherEndpoint.getPages()
			.then((weather) => {
				setWeather(weather);
				console.log(weather);
			})
			.catch((e: Error) => {
				console.log(e.message);
			});
	}, []);

	return (
		<>
			{weather ? (
				<div>
					<WeatherWidget weather={weather} />
				</div>
			) : (
				<div>No weather data available</div>
			)}

			<div className={"static py-5"}>
				<h1 className={"text-center font-bold text-lg sm:text-xl lg:text-3xl text-blue-500 underline"}>
					<Link to={"/"}>Who Knows? - Ali & Brian</Link>
				</h1>
				<div className="text-blue-500 underline absolute top-0 right-0 flex gap-5 mr-8 pt-1">
					{isAuthenticated ? (
						<a className="cursor-pointer" onClick={handleClick}>
							Log Out [{user?.username}]
						</a>
					) : (
						<>
							<Link to={"/register"}>Register</Link>
							<Link to={"/login"}>Log in</Link>
						</>
					)}
				</div>
			</div>
			{loggedOut && <div className="bg-teal-300 p-1 border text-sm mt-8 mx-8">You were logged out</div>}
		</>
	);
}
