import './PopupComponent.css'
import React, {useEffect, useRef} from "react";

interface IPopupProps {
    children : any,
    handleClose? : () => any | null
}

// need to add a focus on popup show
export default function PopupComponent(props : IPopupProps) {
    const popupRef = useRef<any>(null);
    const effect = useEffect(() => {
        popupRef.current?.focus()
    },[]);

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
        <div onKeyDown={handleKeyPress} ref={popupRef} tabIndex={0}>
            <div className='popup-background' onClick={handleClose} >

            </div>
            <div className="center-screen popup" >
                {props.children}
            </div>
        </div>
    );
}