import React, {useEffect, useState} from 'react';
import './users.scss'
import {useSelector} from "react-redux";
import User from "./user/user";

const Users = () => {
    const [data, setData] = useState();
    useEffect(async () => {
        await fetch("http://localhost:5000/user/all", {
            method: "GET",
            headers: {
                "Authorization": localStorage.getItem("token")
            }
        })
            .then(res => res.json())
            .then(res => setData(res))
    }, []);

    //const users = useSelector(state => state.users.users).map(user => <User key={user.id} file={user}/>)
    console.log(data)
    return (
        <div className='userList'>
            <div className="userListHeader">
                <div className="userListName">UserName</div>
                <div className="userListEmail">Email</div>
                <div className="userListEmailConf">Email Confirmed</div>
                <div className="userListStatus">Status</div>
            </div>
            {
                data ? (
                    data.map(user => <User key={user.id} user={user}/>)
                ) : <span>Загрузка...</span>
            }
        </div>
    );
};

export default Users;