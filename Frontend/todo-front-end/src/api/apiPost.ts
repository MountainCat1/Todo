


export default function apiPost(endpoint: string, dto : any) {
    const requestHeaders: HeadersInit = new Headers({
        'Content-Type': 'application/json'
    });

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