import React from 'react';
import "./AuthorizePanel.scss"
import {Outlet} from "react-router-dom";

const AuthorizePanel = () => {
    return (
        <div className="AuthorizePanel">
            <Outlet/>
        </div>
    );
};

export default AuthorizePanel;