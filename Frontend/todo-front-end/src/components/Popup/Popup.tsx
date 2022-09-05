import './Popup.css'
import React from "react";

interface IPopupProps {
    children : any
}

export default function Popup(props : IPopupProps) {



    return (
        <>
            <div className='popup-background'>

            </div>
            <div className="center-screen popup">
                {props.children}
            </div>
        </>
    );
}