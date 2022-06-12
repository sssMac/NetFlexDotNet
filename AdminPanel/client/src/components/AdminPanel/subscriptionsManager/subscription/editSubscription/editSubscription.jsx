import React, {useEffect, useState} from 'react';
import {addSubscription, editSubscription} from "../../../../../actions/subscription";
import "../input.css"
import {useForm} from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import {useFormik} from "formik";

const schema = yup.object().shape({
        name: yup.string().required(),
        price: yup.string().required().matches(/^(0|[1-9]\d*)([.]\d+)?/, "Invalid value "),
    });

const EditSubscription = ({subscription,setModalActive}) => {
    console.log(subscription)
    const formik = useFormik({
        initialValues:{
            name: subscription.Name,
            price: subscription.Cost,
        },validationSchema: yup.object({
            name: yup.string().required("*").max(20),
            price: yup.string().required("*").matches(/[0-9]+[.][0-9]{2}/, "Invalid cost"),
        }),
        enableReinitialize: true,
        onSubmit: async ({name,price}) => {
            console.log({name, price});
            setModalActive(false)
            await editSubscription(subscription.Id,name,price)
            formik.resetForm();
        }
    })
    return (
        <div>
            <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
                <div className="heading color-secondary-dark">EDIT SUBSCRIPTION</div>

                <div className="block" data-label="Name">
                    <input
                        className="modal_input"
                        placeholder="Name"
                        name="name"
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        value={formik.values.name}
                        autoComplete="off"
                    />
                </div>
                <div className="validation">
                    <div className="color-danger error">
                        {formik.touched.name && formik.errors.name ? (
                            formik.errors.name
                        ) : null}
                    </div>
                </div>
                <div className="block" aria-label="Hello world">
                    <input
                        className="modal_input"
                        type="text"
                        placeholder="Price"
                        name="price"
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        value={formik.values.price}
                        autoComplete="off"
                    />
                </div>
                <div className="validation">
                    <div className="color-danger error">
                        {formik.touched.price && formik.errors.price ? (
                            formik.errors.price
                        ) : null}
                    </div>
                </div>
                <button type="reset" className="button dark color-red" onClick={() => {
                    formik.resetForm()
                }}> Reset
                </button>

                <button className="button dark color-green" onClick={() => {
                }}> Update
                </button>
            </form>
        </div>
    );
};

export default EditSubscription;