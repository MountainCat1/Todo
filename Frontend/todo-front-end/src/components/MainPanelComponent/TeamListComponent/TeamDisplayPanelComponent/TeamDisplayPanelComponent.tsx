import './TeamDisplayPanelComponent.css'
import {TeamDto} from "api/teamApi";


interface TeamDisplayPanelProps {
    dto : TeamDto,
    handleClick: (teamDto : TeamDto) => void
}

export default function TeamDisplayPanelComponent(props : TeamDisplayPanelProps){

    return (
    <button className='button button-size-small team-display-panel'
            onClick={() => props.handleClick(props.dto)}>
        <div className='team-name'>
            {props.dto.name}
        </div>
    </button>)
}