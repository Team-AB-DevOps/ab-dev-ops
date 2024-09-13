import React from "react";
import { SearchInput } from "@/components/core/Input";
import Button from "@/components/core/Button";
import PagesEndpoint from "@/services/PagesEndpoint";
import IPage from "@/models/Page";
import { useSearchParams } from "react-router-dom";
import { useToast } from "@/components/ui/use-toast";

export default function SearchPage() {
	const [searchParams] = useSearchParams();
	const [searchValue, setSearchValue] = React.useState(searchParams.get("q") ?? "");
	const [pages, setPages] = React.useState<IPage[]>([]);
	const toast = useToast();

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
			<div className="flex gap-2 justify-center items-center">
				<SearchInput onKeyDown={handleKeyDown} className="w-52" value={searchValue} onChange={(e) => setSearchValue(e.target.value)} />
				<Button onClick={handleFetch}>Search</Button>
			</div>
			<div className="flex flex-col gap-5 mt-10">
				<div className="flex flex-col gap-3 mt-5">
					{pages.map((p) => (
						<div className="inline-block">
							<a key={p.title} href={p.url} className="text-blue-500 font-semibold underline">
								{p.title}
							</a>
						</div>
					))}
				</div>
			</div>
		</>
	);
}
