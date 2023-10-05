import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signInRedirectCallback } from "./AuthService";

export default function SignInOIDC() {

    const navigate = useNavigate();

    useEffect(() => {
        async function signInAsync() {
            await signInRedirectCallback();
            navigate("/");
            window.location.reload();
        }

        signInAsync();
    }, []);

    return ( <div>Redirecting...</div> );
}