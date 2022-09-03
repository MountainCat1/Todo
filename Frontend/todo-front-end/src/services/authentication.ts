import {useCookies} from "react-cookie";


export function useSaveAuthToken(){
    const [, setCookie] = useCookies(['auth_token'])

    return (token : string) : void => {
        setCookie('auth_token', token, {
            sameSite: true,
            maxAge: 900 // 15 min
        });
    }
}
export function useIsLoggedIn() {
    const [cookies, ] = useCookies(['auth_token'])

    return () : boolean => {
        return cookies.auth_token != null;
    };
}