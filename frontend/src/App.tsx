import PageLayout from "./components/layouts/PageLayout";
import { Toaster } from "./components/ui/toaster";
import { Routes, Route } from "react-router-dom";
import SearchPage from "@/pages/SearchPage";
import AboutPage from "./pages/AboutPage";

function App() {
	return (
		<>
			<PageLayout>
				<Routes>
					<Route path="/" element={<SearchPage />} />
					<Route path="/about" element={<AboutPage />} />
					<Route path="*" element={<h2>404 Page not found</h2>} />

					{/*<Route path="/products" >
					 <Route index element={<ProductListPage/>}/>
					 <Route path=":id" element={<DetailedProductPage/>}/>
					 </Route>*/}
				</Routes>
			</PageLayout>
			<Toaster />
		</>
	);
}

export default App;
