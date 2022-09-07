import useApiPost from "./useApiPost";
import React from "react";
import {RequestStatus} from "./abstractions";


export type CreateTeamDto = {
    name:        string;
    description: string;
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