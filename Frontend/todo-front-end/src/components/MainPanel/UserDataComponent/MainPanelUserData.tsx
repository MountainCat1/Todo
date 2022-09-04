import {useClearAuthToken, useGetUserData} from "services/authenticationService";


export default function MainPanelUserData() {
    const getUserData = useGetUserData();
    const clearAuthToken = useClearAuthToken();

    if (getUserData() == null) {
        console.error('Failed to get user data. Logging of user')
        clearAuthToken();
    }

    return <div>
        Welcome {getUserData()?.username}!
    </div>
}