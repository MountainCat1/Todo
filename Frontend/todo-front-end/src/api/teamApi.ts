import useApiPost from "./useApiPost";


export type CreateTeamDto = {
    name:        string;
    description: string;
}

export function useApiCreateTeam() {
    const apiPost = useApiPost();

    return( async (createDto : CreateTeamDto) : Promise<CreateTeamDto | null> => {
        let team: CreateTeamDto | null = null;

        apiPost('/team/create', createDto)
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
                return response.json();
            })
            .then(responseJson => {
                team = responseJson as CreateTeamDto;
            })
            .catch((reason) => {
                console.error(reason);
            });

        return team;
    })
}