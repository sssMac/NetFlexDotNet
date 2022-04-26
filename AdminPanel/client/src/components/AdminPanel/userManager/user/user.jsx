import React from 'react';
import './user.scss'
import userIcon from '../../../../assets/img/userIcon.png'
import {useDispatch} from "react-redux";
import {block, unblock} from "../../../../actions/user";

const User = ({user}) => {
    const dispatch = useDispatch()

    return (
        <div className='user'>
            <img src={userIcon} alt="" className="userIcon"/>
            <div className="userName">{user.name}</div>
            <div className="userEmail">{user.email}</div>
            <div className="userEmailConf">{user.emailConfirmed}</div>
            <div className="userStatus">{user.status}</div>

            <button className="btnBlock" onClick={() => dispatch(block(user.email))}>Block</button>
            <button className="btnBlock" onClick={() => dispatch(unblock(user.email))}>Unblocl</button>
        </div>
    );
};

export default User;