import React from "react";
import apiPost from "./apiPost";

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
        apiPost(registerEndpoint, dto)
            .then(response => {
                if(!response.ok)
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
