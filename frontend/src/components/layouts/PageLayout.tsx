import { ReactNode } from "react";
import Header from "@/components/layouts/Header.tsx";
import Footer from "./Footer";

type Props = {
	children: ReactNode;
};

export default function PageLayout({ children }: Props) {
	return (
		<div className="min-h-screen">
			<Header />
			<main className={"m-8"}>{children}</main>
			<Footer />
		</div>
	);
}
