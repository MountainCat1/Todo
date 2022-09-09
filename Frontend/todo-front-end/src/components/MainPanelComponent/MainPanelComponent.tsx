import './MainPanelComponent.css';
import  'styles/text.css'

import {useIsLoggedIn} from "services/authenticationService";
import {useNavigate} from "react-router-dom";
import MainPanelUserData from "./UserDataComponent/MainPanelUserData";
import TeamComponent from "./TeamComponent/TeamComponent";

export default function MainPanelComponent() {
    const isLoggedIn = useIsLoggedIn();
    const navigate = useNavigate();

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
                <TeamComponent/>
            </div>
            <div className='center-panel'>

            </div>
        </div>)
}