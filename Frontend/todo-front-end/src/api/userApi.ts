import useApiGet from "./useApiGet";

export type UserDto = {
    accountGuid: string,
    guid: string,
    username: string,
}

export function useApiGetUserData() {
    const apiGet = useApiGet();

    return async () : Promise<UserDto | null> => {
        let user : UserDto | null = null;

         await apiGet('user/me')
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
                return response.json();
            })
            .then(responseJson => {
                user = responseJson as UserDto;
            })
            .catch((reason) => {
                console.error(reason);
            });
        return user;
    }
}