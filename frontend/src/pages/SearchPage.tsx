import React from "react";
import StudentsEndpoint from "@/services/StudentsEndpoint.ts";
import { SearchInput } from "@/components/core/Input";
import Button from "@/components/core/Button";
import PagesEndpoint from "@/services/PagesEndpoint";
import IPage from "@/models/Page";
import { useSearchParams } from "react-router-dom";

export default function SearchPage() {
	const [searchParams] = useSearchParams();
	const [searchValue, setSearchValue] = React.useState(searchParams.get("q") ?? "");
	const [pages, setPages] = React.useState<IPage[]>([]);

	const handleClick = () => {
		PagesEndpoint.getPages(searchValue)
			.then((pages) => {
				console.log(pages);
			})
			.catch((e: Error) => {
				console.log(e.message);
			});
	};

	const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
		if (event.key !== "Enter")
			return;

		handleClick();
	};

	React.useEffect(() => {
		if (!searchValue)
			return;

		handleClick();
	}, [])

	
	return (
		<>
			<div className="flex gap-2 justify-center items-center">
				<SearchInput onKeyDown={handleKeyDown} className="w-52" value={searchValue} onChange={(e) => setSearchValue(e.target.value)} />
				<Button onClick={handleClick}>Search</Button>
			</div>
		</>
	);
}
