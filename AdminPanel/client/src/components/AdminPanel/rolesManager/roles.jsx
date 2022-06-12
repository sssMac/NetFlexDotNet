import React, {useEffect, useState} from 'react';
import Role from "./role/role";
import Modal from "../../../utils/modal/modal";
import {createRole, removeRole} from "../../../actions/role";
import CreateRole from "./role/createRole/createRole";

const Roles = () => {
    const [data, setData] = useState([]);
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [buttonClick, setButtonClicked] = useState(false)


    useEffect( () => {
        try{
            async function fetchRoles(){
                let response = await fetch("http://localhost:5000/roles/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchRoles().then(r => r);
            setButtonClicked(false)
        }
        catch (e){

        }
    }, [buttonClick]);

    const handleDelete = (id) => {
        setData(data
            .filter((item) => item.Id !== id)
        );
    };

    if(data != null){
        return (
            <div className='roleList'>
                <div className="layout">
                    <div className="block horizontal">
                        <div className="block bg-secondary-dark shadow">
                            <div className="text-secondary shadow"></div>
                            <div className="text-primary color-dark shadow"></div>
                        </div>
                        <div className="block bg-magenta shadow">
                            <div className="text-secondary color-secondary-dark"></div>
                            <div className="text-primary color-dark shadow"></div></div>
                        <div className="block bg-light">
                            <div className="text-secondary shadow">Manager</div>
                            <button className="button dark color-light" onClick={() => {
                                setModalActive(true)
                                setModalChild(<CreateRole setActive={setModalActive}/>)
                            }}> Create </button>
                            <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Role</th>
                                <th>Actions</th>
                            </tr>
                                {data
                                    .sort((a, b) => a.Id > b.Id ? 1 : -1)
                                    .map(role =>{
                                    return <tr key={role.Id}>
                                        <Role role={role} />
                                        <td>
                                            <button className="button dark color-light"
                                                    disabled = { role.Name === "Admin" || role.Name === "User"}
                                                    onClick={() => {
                                                        removeRole(role.Id).then(r => r)
                                                        handleDelete(role.Id)
                                            }}>Remove
                                            </button>
                                        </td>
                                    </tr>
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
                <Modal active={modalActive} setActive={setModalActive} setButtonClicked={setButtonClicked}>
                    {modalChild}
                </Modal>
            </div>
        );
    }
    else{
        return <span>Loading...</span>

    }

};

export default Roles;