import React, {useEffect, useState} from 'react';
import {useSelector} from "react-redux";
import User from "./user/user";
import * as ReactBootStrap from "react-bootstrap";

const Users = () => {
    const [data, setData] = useState();
    const [usersCount, setUsersCount] = useState(0);
    const [lastUser, setLastUser] = useState();
    useEffect( () => {
        try{

            async function fetchUsers(){
                let response = await fetch("http://localhost:5000/user/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res.sort(a => a[res.Id])))
                setUsersCount(data.length)
                setLastUser(data[data.length-1].UserName)

            }
            fetchUsers();

        }
        catch (e){

        }
    }, [data]);

    return (
        <div className='userList'>
            <div className="layout">
                <div className="block horizontal">
                    <div className="block bg-secondary-dark shadow">
                        <div className="text-secondary shadow">Total users</div>
                        <div className="text-primary color-dark shadow">{usersCount}</div>
                    </div>
                    <div className="block bg-magenta shadow">
                        <div className="text-secondary color-secondary-dark">Last registered</div>
                        <div className="text-primary color-dark shadow">{lastUser}</div></div>
                    <div className="block bg-light">
                        <div className="text-secondary shadow">December</div>
                        <div className="text-primary color-dark shadow">December</div>
                    </div>
                </div>
                <div className="block bg-secondary-dark shadow">
                    <table className="table">
                        <tbody>

                            <tr className="text-secondary">
                                <th>Avatar</th>
                                <th>Name/Email</th>
                                <th>Email Confirmed</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                            {
                                data ? (
                                    data.map(user => <User key={user.UserName} user={user}/> )
                                )
                                    :
                                    <tr>
                                        <th>
                                            Loading...
                                        </th>
                                    </tr>
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default Users;