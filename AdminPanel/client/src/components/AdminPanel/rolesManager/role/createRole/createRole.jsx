import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import './createRole.scss'
import {createRole, removeRole} from "../../../../../actions/role";
import {useDispatch} from "react-redux";

const CreateRole = (setButtonClicked) => {
    const [roleName, setRoleName] = useState()
    const dispatch = useDispatch();

    return (
        <div>
            <label className="modal__head">Create Role</label>
            <div className="textbox">
                <Input type="text" value={roleName} setValue={setRoleName} placeholder="Role..." />
            </div>

            <button className="button light color-dark" onClick={() => createRole(roleName)}> Create
            </button>

        </div>

    );
};

export default CreateRole;