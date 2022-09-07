import useApiPost from "./useApiPost";


export type CreateTeamDto = {
    name:        string;
    description: string;
}

export function useApiCreateTeam() {
    const apiPost = useApiPost();

    return( (createDto : CreateTeamDto) => {
        apiPost('/team/create', createDto)
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
            })
            .catch((reason) => {
                console.error(reason);
            });
    })
}