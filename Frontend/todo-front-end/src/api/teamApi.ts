import useApiPost from "./useApiPost";
import React from "react";
import {RequestStatus} from "./abstractions";
import useApiGet from "./useApiGet";


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

    return(async () : Promise<TeamDto[] | null>  => {
        let dto : TeamDto[] | null = null;

        await apiGet('team/list')
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
                return response.json();
            })
            .then(responseJson => {
                dto = responseJson as TeamDto[];
            })
            .catch((reason) => {
                console.error(reason);
            });

        return dto;
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