import { Log, UserManager } from "oidc-client";

const userManagerSettings = {
    authority: "https://localhost:7280/",
    client_id: "notes-app",
    response_type: "code",
    scope: "openid profile NotesApp",
    redirect_uri: "http://localhost:3000/signin",
    post_logout_redirect_uri: "http://localhost:3000/signout",
    automaticSilentRenew: false,
    monitorSession: false
};

Log.logger = console;
Log.level = Log.DEBUG;

const userManager = new UserManager(userManagerSettings);

export async function loadUser() {
    const user = await userManager.getUser();
    console.log(user);
    const token = user?.access_token;
    localStorage.setItem("token", token ? token : "");
}

export const signInRedirect = () => {
    userManager.signinRedirect();
}

export const signInRedirectCallback = async () => {
    await userManager.signinRedirectCallback();
}

export const signOutRedirect = async () => {
    const userIdToken = (await userManager.getUser()).id_token;
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirect({id_token_hint: userIdToken});
}

export const signOutRedirectCallback = () => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
}

export default userManager;