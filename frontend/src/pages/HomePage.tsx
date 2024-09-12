import { useEffect } from "react";
import StudentsEndpoint from "@/services/StudentsEndpoint.ts";
import { SearchInput } from "@/components/core/Input";

export default function HomePage() {


	useEffect(() => {
		StudentsEndpoint.getStudents()
			.then((students) => {
				console.log(students);
			})
			.catch((e: Error) => {
				console.log(e.message);
			});
	}, []);

	return (
		<>
			
			<div className="flex justify-center items-center">
				<SearchInput className="w-64" value="hello" />
			</div>
		</>
	);
}
