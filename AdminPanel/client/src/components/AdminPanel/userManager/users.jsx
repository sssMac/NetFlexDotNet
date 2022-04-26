import React, {useEffect, useState} from 'react';
import './users.scss'
import {useSelector} from "react-redux";
import User from "./user/user";

const Users = () => {
    const [data, setData] = useState();
    useEffect(() => {
        /*await fetch("http://localhost:5000/api/GetUsers", {
            method: "POST",
            headers: {
                "Auhtorization": localStorage.getItem("token")
            }
        })
            .then(res => res.json())
            .then(res => setData(res))*/
        setInterval(() => {
            setData([
                {id: 1, name: 'kekis', email: 'kekis@mail.ru', emailConfirmed: 'true', status: 'true' },
                {id: 2, name: 'kekis', email: 'kekis@mail.ru', emailConfirmed: 'true', status: 'true' }]
                .map(user => <User key={user.id} user={user}/>))
        }, 1000)
    }, []);

    //const users = useSelector(state => state.users.users).map(user => <User key={user.id} file={user}/>)

    return (
        <div className='userList'>
            <div className="userListHeader">
                <div className="userListName">UserName</div>
                <div className="userListEmail">Email</div>
                <div className="userListEmailConf">Email Confirmed</div>
                <div className="userListStatus">Status</div>
            </div>
            {data}
        </div>
    );
};

export default Users;