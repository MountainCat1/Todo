import './MainPanel.css';
import {useIsLoggedIn} from "../../services/authentication";
import {useNavigate} from "react-router-dom";

export default function MainPanel() {
    const isLoggedIn = useIsLoggedIn();
    const navigate = useNavigate();

    return (
        <div className='Main-panel Panel'>
            <div className='top-panel'>
                {isLoggedIn()
                    ? <div></div>
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