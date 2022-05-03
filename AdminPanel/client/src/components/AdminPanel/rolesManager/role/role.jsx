import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import {removeRole} from "../../../../actions/role";

const Role = ({role}) => {
    const dispatch = useDispatch()

    return (
        <tr>
            <td>
                <div className="text-primary color-light">{role.Name}</div>
                <div className="text-secondary">{role.Name}</div>
            </td>
            <td>
                <button className="button dark color-light" onClick={() => {
                    removeRole(role.Id).then(r => r)
                }}>Remove
                </button>
            </td>
        </tr>
    );
};

export default Role;