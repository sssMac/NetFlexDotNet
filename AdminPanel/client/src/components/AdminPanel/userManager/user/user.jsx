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
            <div className="userName">{user.Name}</div>
            <div className="userEmail">{user.Email}</div>
            <div className="userEmailConf">{user.EmailConfirmed}</div>
            <div className="userStatus">{user.Status}</div>

            <button className="btnBlock" onClick={() => dispatch(block(user.Email))}>Block</button>
            <button className="btnBlock" onClick={() => dispatch(unblock(user.Email))}>UnblocK</button>
        </div>
    );
};

export default User;