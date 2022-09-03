import useApiGet from "./useApiGet";

export type UserDto = {
    accountGuid: string,
    guid: string,
    username: string,
}

export function useApiGetUserData(){
    const apiGet = useApiGet();

    return (): UserDto | null => {
        apiGet('user/get')
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);

                return response.json();
            })
            .then(responseJson => {
                return responseJson as UserDto;
            })
            .catch((reason) => {
                console.log(reason);
            });

        return null;
    }
}