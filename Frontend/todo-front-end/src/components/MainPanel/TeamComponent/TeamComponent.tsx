import Popup from "components/Popup/Popup";
import {useState} from "react";

type TeamComponentState = {
    showFormPopup: boolean
}

export default function TeamComponent() {
    const [state, setState] = useState<TeamComponentState>({
        showFormPopup: false
    });

    const handleCreateTeamButton = () => {
        setState({
            ...state,
            showFormPopup: !state.showFormPopup
        })
    }

    return (<>
        <button className='button button-size-small' onClick={handleCreateTeamButton}>
            Create Team
        </button>

        {state.showFormPopup
            ? <Popup>
                XDD
            </Popup>
            : <></>
        }
    </>)
}