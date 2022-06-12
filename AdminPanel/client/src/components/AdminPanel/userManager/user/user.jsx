import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import {block, unblock} from "../../../../actions/user";
import axios from "axios";
import './CustomSelect.scss'
import Select from "react-select";
import {assignRole} from "../../../../actions/role";
import {setSubscription} from "../../../../actions/subscription";

const User = ({user, roles, refresh}) => {
    const [currentRole, setCurrentRole] = useState(user.Roles[0].Name)
    const dispatch = useDispatch()

    const optionsRoles = []

    roles.map(r => optionsRoles.push({
        value: r.Name,
        label: r.Name
    }))



    const onChangeRole = async (newRole) => {
        setCurrentRole(newRole)
        await assignRole(user.Id, roles.find(r => r.Name === newRole.value).Id)
    }
    const getRole = () => {
        return currentRole ? optionsRoles.find(r => r.value === currentRole) : ''
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
                <Select classNamePrefix='custom-select' onChange={onChangeRole} value={getRole()} options={optionsRoles}/>
                <div className="text-secondary">{user.Roles[0].Name}</div>
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