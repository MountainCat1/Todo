import React from "react";
import useApiPost from "./useApiPost";

const registerEndpoint: string = '/authentication/register';
const loginEndpoint: string = '/authentication/authenticate';

export type RegisterDto = {
    username: string,
    password: string
}

export type RegisterRequestResponse = {
    guid: String,
    username: String,
    userGuid: String
}

export type RegistrationStatus = {
    loading: boolean,
    error: boolean
}

export const useApiRegister
    = (setStatus: React.Dispatch<RegistrationStatus>,
       onSuccess: (response: RegisterRequestResponse) => void) => {

    const apiPost = useApiPost();

    return (dto: RegisterDto) => {
        setStatus({loading: true, error: false})
        apiPost(registerEndpoint, dto)
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);

                return response.json();
            })
            .then(responseJson => {
                let response = responseJson as RegisterRequestResponse;
                onSuccess(response);
                setStatus({loading: false, error: false});
            })
            .catch(() => {
                setStatus({loading: false, error: true});
            });
    }
}


export type LoginStatus = {
    loading: boolean,
    error: boolean
}

export type LoginDto = {
    username: string,
    password: string
}

export const useApiAuthenticate
    = (setStatus: React.Dispatch<LoginStatus>,
       onSuccess: (response: string) => void) => {

    const apiPost = useApiPost();

    return (dto: LoginDto) => {
        setStatus({loading: true, error: false})

        apiPost(loginEndpoint, dto)
            .then(response => {
                if (!response.ok) {
                    throw new Error(response.statusText)
                }
                return response.text();
            })
            .then((responseText) => {
                setStatus({loading: false, error: false});

                onSuccess(responseText);
            })
            .catch((ex) => {
                console.log(ex)
                setStatus({loading: false, error: true});
            })
    }

}