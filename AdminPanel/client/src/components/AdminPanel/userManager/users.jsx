import React, {useEffect, useMemo, useState} from 'react';
import {useSelector} from "react-redux";
import User from "./user/user";

const Users = () => {
    const [data, setData] = useState(null);
    const [roles, setRoles] = useState(null)
    const [buttonClick, setButtonClicked] = useState(false)

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
                    .then(res => setData(res.sort((a, b) => a.Id > b.Id ? -1 : 1)))

            }
            fetchUsers().then(r => r);

            async function fetchRoles(){
                let response = await fetch("http://localhost:5000/roles/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setRoles(res))

            }
            fetchRoles().then(r => r);

            setButtonClicked(false)
        }
        catch (e){

        }
    }, [buttonClick]);

    if(data != null && roles != null){
        return (
            <div className='userList'>
                <div className="layout">
                    <div className="block horizontal">
                        <div className="block bg-secondary-dark shadow">
                            <div className="text-secondary shadow">Total users</div>
                            <div className="text-primary color-dark shadow">{data.length}</div>
                        </div>
                        <div className="block bg-magenta shadow">
                            <div className="text-secondary color-secondary-dark"></div>
                            <div className="text-primary color-dark shadow"></div></div>
                        <div className="block bg-light">
                            <div className="text-secondary shadow">Manager</div>
                            <div className="text-primary color-dark shadow">
                                <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                            </div>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Avatar</th>
                                <th>Name/Email</th>
                                <th>Role</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                                { data
                                    .map(user => <User key={user.UserName} user={user} roles={roles} refresh={setButtonClicked}/>) }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
    else {
        return <span>Loading...</span>
    }

};

export default Users;