import useApiPost from "./useApiPost";


export type TeamDto = {
    name:        string;
    description: string;
}

export function useApiCreateTeam(createDto : TeamDto) {
    const apiPost = useApiPost();


    return( async () : Promise<TeamDto | null> => {
        let team: TeamDto | null = null;

        apiPost('/team/create', createDto)
            .then(response => {
                if (!response.ok)
                    throw new Error(response.statusText);
                return response.json();
            })
            .then(responseJson => {
                team = responseJson as TeamDto;
            })
            .catch((reason) => {
                console.error(reason);
            });

        return team;
    })
}