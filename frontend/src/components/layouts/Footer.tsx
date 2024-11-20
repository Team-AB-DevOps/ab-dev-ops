import React from "react";
import { Link } from "react-router-dom";

export default function Footer() {
	return (
		<div className="bg-gray-200 px-2 sticky top-[100vh]">
			Who Knows? (C) 2024{" "}
			<Link to={"/about"} className="text-blue-500 underline">
				About
			</Link>
		</div>
	);
}
