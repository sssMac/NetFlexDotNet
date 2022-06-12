import React, {useEffect, useState} from 'react';
import Modal from "../../../utils/modal/modal";
import AddSubscription from "./subscription/addSubscription/addSubscription";
import Subscription from "./subscription/subscription";
import {removeSubscription} from "../../../actions/subscription";
import Role from "../rolesManager/role/role";
import {removeRole} from "../../../actions/role";
import EditSubscription from "./subscription/editSubscription/editSubscription";
import RenameGenre from "../genresManager/genre/renameGenre/renameGenre";

const Subscriptions = () => {
    const [data, setData] = useState([]);
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [buttonClick, setButtonClicked] = useState(false)


    useEffect( () => {
        try{
            async function fetchSubscriptions(){
                let response = await fetch("http://localhost:5000/subscription/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchSubscriptions().then(r => r);
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
                                setModalChild(<AddSubscription setActive={setModalActive}/>)
                            }}> Create </button>
                            <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Name</th>
                                <th>Cost</th>
                                <th>Active</th>
                            </tr>
                            {data
                                .sort((a, b) => a.Id > b.Id ? 1 : -1)
                                .map(subscription =>{
                                    return <tr key={subscription.Id}>
                                        <td>
                                            <div className="text-primary color-light">{subscription.Name}</div>
                                        </td>
                                        <td>
                                            <div className="text-primary color-light">{subscription.Cost} $</div>
                                        </td>

                                        <td>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        setModalActive(true)
                                                        setModalChild(<EditSubscription subscription={subscription} setModalActive={setModalActive}/>)
                                                    }}>Edit
                                            </button>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        removeSubscription(subscription.Id).then(r => r)
                                                        handleDelete(subscription.Id)
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

export default Subscriptions;