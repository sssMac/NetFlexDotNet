import React, {useEffect, useState} from 'react';
import {editSubscription} from "../../../../../actions/subscription";
import "../input.css"
import {useForm} from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';

const schema = yup.object().shape({
        name: yup.string().required(),
        price: yup.string().required().matches(/^(0|[1-9]\d*)([.]\d+)?/, "Invalid value "),
    });

const EditSubscription = ({subscription,setModalActive}) => {
    const [newName, setNewName] = useState('')
    const [newCost, setNewCost] = useState('')
    console.log(subscription)


    const { register, handleSubmit, errors } = useForm({
        resolver: yupResolver(schema),
    });


    const onSubmit = data => console.log(data);
    console.log(errors)
    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="textbox">
                    <input type="text"
                           name="name"
                           placeholder="Name"
                           value={newName}
                           onChange={(e) => setNewName(e.target.value)}
                           ref={register} />
                </div>

                <div className="textbox">
                    <input type="number"
                           name="price"
                           placeholder="Price"
                           value={newCost}
                           onChange={(e) => setNewCost(e.target.value)}
                           ref={register}/>
                </div>

                <button className="button dark color-red" onClick={() => {
                    setModalActive(false)
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                    //setModalActive(false)
                    //editSubscription(subscription.Id, newName, newCost).then(r => r)
                }}> Submit
                </button>
            </form>

        </div>
    )
};

export default EditSubscription;