import './MainPanelComponent.css';
import  'styles/text.css'

import {useIsLoggedIn} from "services/authenticationService";
import {useNavigate} from "react-router-dom";
import MainPanelUserData from "./UserDataComponent/MainPanelUserData";
import TeamListComponent from "./TeamListComponent/TeamListComponent";
import {TeamDto} from "api/teamApi";
import {useState} from "react";


type DashboardState = {
    selectedTeam : TeamDto
}

export default function MainPanelComponent() {
    const isLoggedIn = useIsLoggedIn();
    const navigate = useNavigate();

    const [state, setState] = useState<DashboardState>();


    const handleSelectTeam = (teamDto : TeamDto) => {
        setState({
            ...state,
            selectedTeam: teamDto
        })
    }

    return (
        <div className='Main-panel Panel'>
            <div className='top-panel'>
                {isLoggedIn()
                    ? <MainPanelUserData/>
                    : <button className='button'
                              onClick={() => {
                                  navigate('/login')
                              }}>Log in</button>}
            </div>
            <div className='left-panel'>
                <TeamListComponent handleSelectTeam={handleSelectTeam}/>
            </div>
            <div className='center-panel'>

            </div>
        </div>)
}