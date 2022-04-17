import React from 'react';
import '../SignUp/RegLog.scss'
import Input from "../../utils/input/Input";
import {NavLink} from "react-router-dom";

const Login = () => {
    return (
        <div className="RegLog">
            <div className="main">
                <label className="reg__head">Sign In</label>

                <div className="textbox">
                    <Input type="text" placeholder="Email..." />
                </div>

                <div className="textbox">
                    <Input type="password" placeholder="Password..." />

                </div>

                <button className="registration__btn">Sign In</button>

                <div className="otherInfo">Do you want to register   <NavLink to="/SignUp">Sign Up</NavLink></div>
            </div>
        </div>
    );
};

export default Login;