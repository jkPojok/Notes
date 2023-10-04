import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signOutRedirectCallback } from "./AuthService";

export default function SignOutOIDC() {

    const navigate = useNavigate();

    useEffect(() => {
        async function signOutAsync() {
            await signOutRedirectCallback();
            navigate("/");
        }

        signOutAsync();
    }, []);

    return ( <div>Redirecting...</div> );
}