import React from "react";

const registerEndpoint: string = '/authentication/register';

export interface RegisterDto {
    username: string,
    password: string
}

export interface RegisterRequestResponse {
    guid: String,
    username: String,
    userGuid: String
}

export interface RegistrationStatus {
    loading: boolean,
    error: boolean
}

export const useApiRegister
    = (setStatus: React.Dispatch<RegistrationStatus>,
       onSuccess : (response: RegisterRequestResponse) => void) =>
    (dto: RegisterDto) => {
        const requestHeaders: HeadersInit = new Headers({
            'Content-Type': 'application/json'
        });

        const requestOptions: RequestInit = {
            method: 'POST',
            headers: requestHeaders,
            body: JSON.stringify(dto)
        };

        let url: string = `${process.env.REACT_APP_API_URL}${registerEndpoint}`;
        fetch(url, requestOptions)
            .then(response => {
                if(!response.ok)
                    throw new Error(response.statusText);

                return response.json()
            })
            .then(responseJson => {
                let response = responseJson as RegisterRequestResponse;
                onSuccess(response)
                setStatus({loading: false, error: false});
            })
            .catch(() => {
                setStatus({loading: false, error: true});
            });
    }
