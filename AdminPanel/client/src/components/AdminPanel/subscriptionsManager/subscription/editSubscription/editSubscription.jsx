import React, {useEffect, useState} from 'react';
import {editSubscription} from "../../../../../actions/subscription";
import { ErrorMessage } from "@hookform/error-message";
import _ from "lodash/fp";
import "../input.css"
import {useForm} from "react-hook-form";


const EditSubscription = ({subscription,setModalActive}) => {
    const [newName, setNewName] = useState('')
    const [newCost, setNewCost] = useState('')
    console.log(subscription)


    const { watch, register, handleSubmit, formState: { errors }, setValue } = useForm({
        defaultValues: {
            name: "",
            price: "",
        }
    });
    const [name, price] = watch(["name", "price"]);

    useEffect(() => {
        setValue("name", `${subscription.Name}`)
        setValue("price", `${subscription.Price}`)

    }, [subscription, name, price])


    const onSubmit = data => console.log(data);

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="textbox">
                    <input type="text"
                           placeholder="Name"
                           {...register("name", {required: true, maxLength: 25})} />
                </div>
                <div className="textbox">
                    <input type="number" placeholder="Price" {...register("price", {required: true, min: 0, pattern: /^[0-9]+(\.[0-9][0-9])?/i})} />
                </div>
                <ErrorMessage
                    errors={errors}
                    name="multipleErrorInput"
                    render={({ messages }) => {
                        console.log("messages", messages);
                        return messages
                            ? _.entries(messages).map(([type, message]: [string, string]) => (
                                <p key={type}>{message}</p>
                            ))
                            : null;
                    }}
                />

                <button className="button dark color-red" onClick={() => {
                    setModalActive(false)
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                    setModalActive(false)
                    editSubscription(subscription.Id, newName, newCost).then(r => r)
                }}> Submit
                </button>
            </form>

        </div>
    )
};

export default EditSubscription;