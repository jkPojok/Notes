import MainMenu from "./Screens/MainMenuScreen/MainMenu";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SignInOIDC from "./auth/SignInOIDC";
import SignOutOIDC from "./auth/SignOutOIDC";
import { loadUser } from "./auth/AuthService";

function App() {

  loadUser();

  return (
    <Router>
      <div className="app">
        <Routes>
          <Route path="/" element={<MainMenu />} />
          <Route path="/signin" element={<SignInOIDC />} />
          <Route path="/signout" element={<SignOutOIDC />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
