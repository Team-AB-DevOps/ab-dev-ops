import { ReactNode } from "react";
import NavBar from "@/components/layouts/NavBar.tsx";
import Header from "@/components/layouts/Header.tsx";
import Footer from "./Footer";

type Props = {
	children: ReactNode;
};

export default function PageLayout({ children }: Props) {
	return (
		<>
			<Header />
			<main className={"m-8"}>{children}</main>
			<Footer />
		</>
	);
}
