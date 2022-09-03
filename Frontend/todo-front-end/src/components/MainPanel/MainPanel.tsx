import './MainPanel.css';
import {useIsLoggedIn} from "services/authenticationService";
import {useNavigate} from "react-router-dom";
import MainPanelUserData from "./UserDataComponent/MainPanelUserData";

export default function MainPanel() {
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

            </div>
            <div className='center-panel'>

            </div>
        </div>)
}