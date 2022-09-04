import {useCookies} from "react-cookie";


export default function useApiGet() {
    const [cookies,] = useCookies();

    return async (endpoint: string) => {
        const requestHeaders: HeadersInit = new Headers({
        });

        if(cookies.auth_token != null)
            requestHeaders.set('Authorization', `Bearer ${cookies.auth_token}`)

        endpoint = endpoint.trim();
        if(endpoint.startsWith('/'))
            endpoint = endpoint.substring(1);


        let url: string = `${process.env.REACT_APP_API_URL}/${endpoint}`;
        const requestOptions: RequestInit = {
            method: 'GET',
            headers: requestHeaders
        };

        return fetch(url, requestOptions);
    }
}