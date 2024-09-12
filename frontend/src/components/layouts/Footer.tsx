import React from "react";
import { Link } from "react-router-dom";

export default function Footer() {
	return (
		<div className="bg-gray-200 mx-2 px-2">
			Who Knows? (C) 2024 <Link to={"/about"} className="text-blue-500">About</Link>
		</div>
	);
}
