import React, {useEffect, useLayoutEffect, useState} from 'react';
import Input from "../../../../../utils/input/Input";
import {editSubscription} from "../../../../../actions/subscription";
import Subscription from "../subscription";

const EditSubscription = ({subscription,setModalActive}) => {
    const [newName, setNewName] = useState(subscription.Name)
    const [newCost, setNewCost] = useState(subscription.Cost)

    useEffect (() => {
        setNewName(subscription.Name)
        setNewCost(subscription.Cost)
    },[subscription.Name,subscription.Cost])

    console.log(newName)


    return (
        <div>
            <div className="textbox">
                <Input type="text" value={newName} setValue={setNewName} placeholder={subscription.Name} />
            </div>

            <div className="textbox">
                <Input type="number" value={newCost} setValue={setNewCost} placeholder={subscription.Cost} />
            </div>

            <button className="button dark color-red" onClick={() => {
                setModalActive(false)
            }}> Cancel
            </button>

            <button className="button dark color-green" onClick={() => {
                setModalActive(false)
                editSubscription(subscription.Id, newName, newCost).then(r => r)
                console.log(subscription)
            }}> Submit
            </button>

        </div>
    )
};

export default EditSubscription;