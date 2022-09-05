import './Popup.css'
import React, {useRef} from "react";

interface IPopupProps {
    children : any,
    handleClose? : () => any | null
}

// need to add a focus on popup show
export default function Popup(props : IPopupProps) {
    const mainRef = useRef(null);

    const handleClose = () => {
        if (props?.handleClose) {
            props?.handleClose();
        }
    }

    function handleKeyPress(event: React.KeyboardEvent<HTMLDivElement>) {
        if(event.key === 'Escape'){
            handleClose();
        }
    }

    return (
        <div onKeyDown={handleKeyPress} ref={mainRef} tabIndex={0}>
            <div className='popup-background' onClick={handleClose} >

            </div>
            <div className="center-screen popup" >
                {props.children}
            </div>
        </div>
    );
}