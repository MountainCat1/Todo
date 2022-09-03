import {useCookies} from "react-cookie";


export function useSaveAuthToken(){
    const [, setCookie] = useCookies(['auth_token'])

    return (token : string) : void => {
        setCookie('auth_token', token);
    }
}
export function useIsLoggedIn() {
    const [cookies, setCookie] = useCookies(['auth_token'])

    return () : boolean => {
        return cookies.auth_token != null;
    };
}