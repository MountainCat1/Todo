import useApiPost from "./useApiPost";
import React from "react";
import {RequestStatus} from "./abstractions";
import useApiGet from "./useApiGet";
import {UserDto} from "./userApi";


export type CreateTeamDto = {
    name:        string;
    description: string;
}

export type TeamDto = {
    guid:        string;
    name:        string;
    description: string;
}

export function useApiGetTeamList(){
    const apiGet = useApiGet()

    return(async () => {
        let teamDto : TeamDto | null = null;

        await apiGet('teams/list')
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
                return response.json();
            })
            .then(responseJson => {
                teamDto = responseJson as TeamDto;
            })
            .catch((reason) => {
                console.error(reason);
            });
    })
}

export function useApiCreateTeam(setStatus : React.Dispatch<RequestStatus>,
                                 onSuccess : () => void) {
    const apiPost = useApiPost();

    return( async (createDto : CreateTeamDto) => {
        setStatus({
            loading: true,
            error: false
        })

        await apiPost('/team/create', createDto)
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
            })
            .then(() => {
                setStatus({
                    loading: false,
                    error: true
                })
                onSuccess();
            })
            .catch((reason) => {
                console.error(reason);
                setStatus({
                    loading: false,
                    error: true
                })
            });
    })
}