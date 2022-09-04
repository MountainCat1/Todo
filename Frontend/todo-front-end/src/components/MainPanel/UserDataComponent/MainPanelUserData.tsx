import {useClearAuthToken, useGetUserData} from "services/authenticationService";
import {useEffect, useState} from "react";
import {UserDto} from "api/userApi";


export default function MainPanelUserData() {
    const getUserData = useGetUserData();
    const clearAuthToken = useClearAuthToken();

    const [userData, setUserData] = useState<UserDto | null>()

    useEffect( () => {
        getUserData().then((userData) => {
            setUserData(userData);
            if (userData == null) {
                console.error('Failed to get user data. Logging off user')
                clearAuthToken();
            }
        });
    }, [clearAuthToken, getUserData]);
    return <div>
        Welcome {userData?.username}!
    </div>
}