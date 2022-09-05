import './Popup.css'

interface IPopupProps {
    children : any
}

export default function Popup(props : IPopupProps) {
    return (
        <div className="center-screen popup">
            {props.children}
        </div>
    );
}