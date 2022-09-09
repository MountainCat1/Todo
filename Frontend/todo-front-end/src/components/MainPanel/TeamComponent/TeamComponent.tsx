import Popup from "components/Popup/Popup";
import {useEffect, useState} from "react";
import CreateTeamForm from "./CreateTeamForm/CreateTeamForm";
import {TeamDto, useApiGetTeamList} from "api/teamApi";

type TeamComponentState = {
    showFormPopup: boolean,

    teamDto: TeamDto[]
}

export default function TeamComponent() {
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



    return (<>
        <button className='button button-size-small' onClick={handleCreateTeamButton}>
            Create Team
        </button>
        {state.showFormPopup
            ? <Popup handleClose={closeTeamCreationPopup}>
                <CreateTeamForm closePopup={closeTeamCreationPopup}/>
            </Popup>
            : <></>
        }

        {}
    </>)
}