import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
	children: React.ReactNode;
}

const Button = (props: ButtonProps) => {
	return (
		<button {...props} className="px-4 py-2 bg-gray-200 border border-gray-300 rounded-md ">
			{props.children}
		</button>
	);
};

export default Button;
