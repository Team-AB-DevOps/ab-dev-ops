import PageLayout from "./components/layouts/PageLayout";
import { Toaster } from "./components/ui/toaster";
import { Routes, Route } from "react-router-dom";
import SearchPage from "@/pages/SearchPage";
import AboutPage from "./pages/AboutPage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ResetPasswordPage from "./pages/ResetPasswordPage";

function App() {
	return (
		<>
			<PageLayout>
				<Routes>
					<Route path="/" element={<SearchPage />} />
					<Route path="/weather" element={<SearchPage />} />
					<Route path="/about" element={<AboutPage />} />
					<Route path="/login" element={<LoginPage />} />
					<Route path="/register" element={<RegisterPage />} />
					<Route path="/reset-password" element={<ResetPasswordPage />} />
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
