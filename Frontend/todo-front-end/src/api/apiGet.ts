

export default function apiGet(endpoint: string) {
    endpoint = endpoint.trim();
    if(endpoint.startsWith('/'))
        endpoint = endpoint.substring(1);

    let url: string = `${process.env.REACT_APP_API_URL}/${endpoint}`;
    const requestOptions: RequestInit = {
        method: 'GET',
    };

    return fetch(url, requestOptions);
}