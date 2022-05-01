import React, {useState} from 'react';
import './RegLog.scss'
import Input from "../../../utils/input/Input";
import {NavLink} from "react-router-dom";
import {login} from "../../../actions/user";
import {useDispatch} from "react-redux";
import {logout} from "../../../reducers/userReducer";

const Login = () => {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const dispatch = useDispatch()

    const handleLogin = (e) => {
        e.preventDefault();

        dispatch(login(email,password))
    }

    return (
        <div className="RegLog">
            <div className="main">
                <label className="reg__head">Sign In</label>

                <div className="textbox">
                    <Input type="text" value={email} setValue={setEmail} placeholder="Email..." />
                </div>

                <div className="textbox">
                    <Input type="password" value={password} setValue={setPassword} placeholder="Password..." />

                </div>

                <button className="registration__btn" onClick={ (e)=> handleLogin(e)} >Sign In</button>

                <div className="otherInfo">Do you want to register   <NavLink to="/SignUp">Sign Up</NavLink></div>
            </div>
        </div>
    );
};

export default Login;