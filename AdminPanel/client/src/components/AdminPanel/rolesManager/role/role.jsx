import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import {removeRole} from "../../../../actions/role";

const Role = ({role}) => {

    return (
            <td>
                <div className="text-primary color-light">{role.Name}</div>
                <div className="text-secondary">{role.Name}</div>
            </td>
    );
};

export default Role;