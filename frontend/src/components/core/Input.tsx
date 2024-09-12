import React from "react";

interface SearchInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
	value: string;
}

export const SearchInput = (props: SearchInputProps) => {
	return (
		<>
			<input {...props} className="border border-slate-300 rounded-md p-2 w-[85%] " placeholder="Search..." type="text" value={props.value} />
		</>
	);
};
