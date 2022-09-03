import {useCookies} from "react-cookie";


export default function useApiPost() {
    const [cookies,] = useCookies();
    
    return (endpoint: string, dto : any) => {
        const requestHeaders: HeadersInit = new Headers({
            'Content-Type': 'application/json'
        });

        if(cookies.auth_token != null)
            requestHeaders.set('Authorization', `Bearer ${cookies.auth_token}`)

        endpoint = endpoint.trim();
        if(endpoint.startsWith('/'))
            endpoint = endpoint.substring(1);

        let url: string = `${process.env.REACT_APP_API_URL}/${endpoint}`;
        const requestOptions: RequestInit = {
            method: 'POST',
            headers: requestHeaders,
            body: JSON.stringify(dto)
        };

        return fetch(url, requestOptions);
    }
}