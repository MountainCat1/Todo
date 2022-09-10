import './TeamDisplayPanelComponent.css'
import {TeamDto} from "api/teamApi";


interface TeamDisplayPanelProps {
    dto : TeamDto
}

export default function TeamDisplayPanelComponent(props : TeamDisplayPanelProps){
    return (
    <button className='button button-size-small team-display-panel'>
        {props.dto.name}
    </button>)
}