import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import './createRole.scss'
import {createRole, removeRole} from "../../../../../actions/role";
import {useDispatch} from "react-redux";

const CreateRole = ({setActive}) => {
    const [roleName, setRoleName] = useState()

    const handleClear = (e) =>  {
        e.preventDefault()
        setRoleName('')
    }
    const handleChange = (e) => setRoleName('')

    return (
        <div>
            <div className="textbox">
                <Input type="text" value={roleName} setValue={setRoleName} placeholder="Create role..." />
            </div>

            <button className="button dark color-red" onClick={() => {
                setActive(false)
                handleChange()
            }}> Cancel
            </button>

            <button className="button dark color-green" onClick={() => {
                createRole(roleName).then(r => r)
                setActive(false)
                handleChange()
            }}> Create
            </button>



        </div>

    );
};

export default CreateRole;