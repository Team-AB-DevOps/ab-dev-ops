import React from "react";

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
	label?: string;
}

export const SearchInput = (props: InputProps) => {
	return <input {...props} className="border border-slate-300 rounded-md p-2 w-[85%] " placeholder="Search..." value={props.value} />;
};

export const TextInput = (props: InputProps) => {
	return (
		<>
			<label>{props.label}</label>
			<input {...props} className="border border-slate-300 rounded-md p-2" value={props.value} />
		</>
	);
};
