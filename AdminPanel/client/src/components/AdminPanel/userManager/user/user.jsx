import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import {block, unblock} from "../../../../actions/user";
import axios from "axios";
import './CustomSelect.scss'
import Select from "react-select";
import {assignRole} from "../../../../actions/role";

const User = ({user, roles, refresh}) => {
    const [currentRole, setCurrentRole] = useState(user.Roles[0].Name)
    const dispatch = useDispatch()
    const emailCong = String(user.EmailConfirmed)

    const options = []

    roles.map(r => options.push({
        value: r.Name,
        label: r.Name
    }))



    const onChange = (newRole) => {
        setCurrentRole(newRole)
        assignRole(user.Id, roles.find(r => r.Name === newRole.value).Id).then(r => r)
    }
    const getRole = () => {
        return currentRole ? options.find(r => r.value === currentRole) : ''
    }

    return (
        <tr>
            <td className="avatar">
                <div className="avatar">
                    <img
                        src={user.Avatar}/>
                </div>
            </td>
            <td>
                <div className="text-primary color-light">{user.UserName}</div>
                <div className="text-secondary">{user.Email}</div>
            </td>
            <td>
                <Select classNamePrefix='custom-select' onChange={onChange} value={getRole()} options={options}/>
                <div className="text-secondary">{user.Roles[0].Name}</div>
            </td>
            <td>
                <div className="text-primary color-light">{emailCong}</div>
            </td>
            <td>
                {
                    user.Status === "access" ?
                        <div className="text-primary color-green">{user.Status}</div>

                        :
                        <div className="text-primary color-danger">{user.Status}</div>

                }
            </td>
            <td>
                {
                    user.Status === "access" ?
                    <button className="button dark color-light" onClick={() => {
                        dispatch(block(user.Email))
                        refresh(true)
                    }}>Block
                    </button>
                        :
                    <button className="button dark color-light" onClick={() => {
                        dispatch(unblock(user.Email))
                        refresh(true)
                    }}>Unblock
                    </button>
                }

            </td>
        </tr>
    );
};

export default User;