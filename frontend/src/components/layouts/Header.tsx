import { Link } from "react-router-dom";

export default function Header() {
	return (
		<div className={"py-5"}>
			<h1 className={"text-center font-bold text-lg sm:text-xl lg:text-3xl text-blue-500 underline"}>
				<Link to={"/"}>Who Knows? - Ali & Brian</Link>
			</h1>
		</div>
	);
}
