import {TeamDto} from "api/teamApi";


interface TeamDisplayPanelProps {
    dto : TeamDto
}

export default function TeamDisplayPanelComponent(props : TeamDisplayPanelProps){


    return ( <>
        {props.dto.name}
    </> )
}