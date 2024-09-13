import useAuth from "@/auth/UseAuth";
import { Link, useNavigate } from "react-router-dom";

export default function Header() {
	const { user, isAuthenticated, logout } = useAuth();
	const navigate = useNavigate();


	const handleClick = () => {
		logout();
		navigate("/");
	}

	return (
		<div className={"static py-5"}>
			<h1 className={"text-center font-bold text-lg sm:text-xl lg:text-3xl text-blue-500 underline"}>
				<Link to={"/"}>Who Knows? - Ali & Brian</Link>
			</h1>
			<div className="text-blue-500 underline absolute top-0 right-0 flex gap-5 mr-8 pt-1">
				{isAuthenticated ? (
					<>
						<a onClick={handleClick}>Log Out [{user?.username}]</a>
					</>
				) : (
					<>
						<Link to={"/register"}>Register</Link>
						<Link to={"/login"}>Log in</Link>
					</>
				)}
			</div>
		</div>
	);
}
