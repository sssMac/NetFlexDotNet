import React, {useEffect, useState} from 'react';
import {addSubscription} from "../../../../../actions/subscription";
import {useFormik} from "formik";
import * as yup from "yup";

const AddSubscription = ({setActive}) => {
    const formik = useFormik({
        initialValues:{
            name: "",
            price: ""

        },validationSchema: yup.object({
            name: yup.string().required("*").max(20),
            price: yup.string().required("*").matches(/^(0|[1-9]\d*)([.]\d+)?/, "Invalid cost"),

        }),
        onSubmit: async ({name,price}) => {
            console.log({name, price});
            setActive(false)
            await addSubscription(name,price)
            formik.resetForm();
        }
    })
    return (
        <div>
            <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
                <div className="heading color-secondary-dark">ADD SUBSCRIPTION</div>

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
                <button className="button dark color-red" onClick={() => {
                    setActive(false)
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                }}> Create
                </button>
            </form>
        </div>
    );
};

export default AddSubscription;