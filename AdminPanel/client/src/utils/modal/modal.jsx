import React, {useEffect, useMemo} from 'react';
import './modal.scss'
import '../../components/AdminPanel/PanelsUI-v2.1.min.css'

const Modal = ({active, setActive,children, setButtonClicked} ) => {

    useEffect( () => {
        function refresh(){
            setButtonClicked(true)
        }
        refresh()
    }, [active]);

    return (
        <div className={active ? "modal active" : "modal"} onClick={() => setActive(false)}>
            <div className={active ? "modal__content active" : "modal__content"} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>

    );
};

export default Modal;