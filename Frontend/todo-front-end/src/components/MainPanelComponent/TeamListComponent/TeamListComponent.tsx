import './TeamListComponent.css'
import PopupComponent from "components/PopupComponent/PopupComponent";
import {useEffect, useState} from "react";
import CreateTeamForm from "./CreateTeamFormComponent/CreateTeamForm";
import {TeamDto, useApiGetTeamList} from "api/teamApi";
import TeamDisplayPanelComponent from "./TeamDisplayPanelComponent/TeamDisplayPanelComponent";

type TeamComponentState = {
    showFormPopup: boolean,
    teamDto: TeamDto[]
}

interface ITeamComponentListProps{
    handleSelectTeam : (teamDto : TeamDto) => void;
}

export default function TeamListComponent(props : ITeamComponentListProps) {
    const [state, setState] = useState<TeamComponentState>({
        showFormPopup: false,
        teamDto: new Array<TeamDto>()
    });

    const getTeamList = useApiGetTeamList();

    useEffect(()=> {
        getTeamList().then((dto) => {
            setState({
                ...state,
                teamDto: dto as TeamDto[]
            })
        });
    }, [])

    const handleCreateTeamButton = () => {
        setState({
            ...state,
            showFormPopup: !state.showFormPopup
        })
    }

    const closeTeamCreationPopup = () => {
        setState({
            ...state,
            showFormPopup: false
        })
    }

    const handleClickOnDisplayPanel = (teamDto : TeamDto) => {
        props.handleSelectTeam(teamDto)
    }

    return (<>
        <button className='button button-size-small create-team-button'
                onClick={handleCreateTeamButton}>
            Create New Team
        </button>
        {state.showFormPopup
            ? <PopupComponent handleClose={closeTeamCreationPopup}>
                <CreateTeamForm closePopup={closeTeamCreationPopup}/>
            </PopupComponent>
            : <></>
        }

        <div className='team-list'>
            {   state.teamDto != null
                ? state.teamDto.map(dto =>
                    (<TeamDisplayPanelComponent
                        key={dto.guid}
                        dto={dto as TeamDto}
                        handleClick={handleClickOnDisplayPanel}
                        />))
                : <></>
            }
        </div>

    </>)
}