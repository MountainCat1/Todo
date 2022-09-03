import {useCookies} from "react-cookie";
import {useApiGetUserData} from "../api/userApi";


export function useSaveAuthToken() {
    const [, setCookie] = useCookies(['auth_token'])

    return (token: string): void => {
        setCookie('auth_token', token, {
            sameSite: true,
            maxAge: 900 // 15 min
        });
    }
}

export function useClearAuthToken() {
    const [, setCookie] = useCookies(['auth_token'])

    return (): void => {
        setCookie('auth_token', '', {
            sameSite: true,
            maxAge: 0 // 15 min
        });
    }
}

export function useIsLoggedIn() {
    const [cookies,] = useCookies(['auth_token'])

    return (): boolean => {
        return cookies.auth_token != null;
    };
}

export function useGetUserData(){
    const apiGetUserData = useApiGetUserData();

    return () => {
        return apiGetUserData();
    }
}