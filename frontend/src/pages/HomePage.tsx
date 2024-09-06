import { SkeletonCard } from "@/components/core/SkeletonCard.tsx";
import SkeletonLine from "@/components/core/SkeletonLine.tsx";
import { useEffect } from "react";
import StudentsEndpoint from "@/services/StudentsEndpoint.ts";

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
			<h2>Home</h2>
		</>
	);
}
