import React from "react";
import { SearchInput } from "@/components/core/Input";
import Button from "@/components/core/Button";
import PagesEndpoint from "@/services/PagesEndpoint";
import IPage from "@/models/Page";
import { useLocation, useSearchParams } from "react-router-dom";

export default function SearchPage() {
	const { redirected } = useLocation().state || false;
	const [searchParams] = useSearchParams();
	const [searchValue, setSearchValue] = React.useState(searchParams.get("q") ?? "");
	const [pages, setPages] = React.useState<IPage[]>([]);

	const handleFetch = () => {
		PagesEndpoint.getPages(searchValue)
			.then((pages) => {
				setPages(pages);
				console.log(pages);
			})
			.catch((e: Error) => {
				console.log(e.message);
			});
	};


	const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
		if (event.key !== "Enter") return;

		handleFetch();
	};

	React.useEffect(() => {
		if (!searchValue) return;

		handleFetch();
	}, []);

	return (
		<>
			{redirected && <div className="bg-teal-300 p-1 border text-sm mb-5">You were logged in</div>}
			<div className="flex gap-2 justify-center items-center">
				<SearchInput onKeyDown={handleKeyDown} className="w-52" value={searchValue} onChange={(e) => setSearchValue(e.target.value)} />
				<Button onClick={handleFetch}>Search</Button>
			</div>
			<div className="flex flex-col gap-5 mt-10">
				<div className="flex flex-col gap-3 mt-5">
					{pages.map((p) => (
						<div key={p.title} className="inline-block">
							<a href={p.url} className="text-blue-500 font-semibold underline">
								{p.title}
							</a>
						</div>
					))}
				</div>
			</div>
		</>
	);
}
