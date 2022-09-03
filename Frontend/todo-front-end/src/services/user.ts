import {useCookies} from "react-cookie";
import apiGet from "../api/apiGet";
import {RegisterRequestResponse} from "./authentication";

export type UserDto = {
    accountGuid: string,
    guid: string,
    username: string,
}

export function useSaveAuthToken() {
    const [, setCookie] = useCookies(['auth_token'])

    return (token: string): void => {
        setCookie('auth_token', token, {
            sameSite: true,
            maxAge: 900 // 15 min
        });
    }
}

export function useIsLoggedIn() {
    const [cookies,] = useCookies(['auth_token'])

    return (): boolean => {
        return cookies.auth_token != null;
    };
}

export function useGetUserData() {
    return (): UserDto => {
        apiGet('user/get')
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);

                return response.json();
            })
            .then(responseJson => {
                let dto = responseJson as UserDto;
                return dto;
            })
            .catch((reason) => {
                console.log(reason);
            });

        throw new Error('Failed to fetch user data');
    }
}