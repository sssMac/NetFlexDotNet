import React, {useEffect, useState} from 'react';
import {addSubscription} from "../../../../../actions/subscription";

const AddSubscription = ({setActive}) => {
    const initialValues = { name: "", price: ""};
    const [formValues, setFormValues] = useState(initialValues);
    const [formErrors, setFormErrors] = useState({});
    const [isSubmit, setIsSubmit] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormValues({ ...formValues, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        setFormErrors(validate(formValues));
        if (formErrors === {}){
            setActive(false)
        }
        setIsSubmit(true);
    };

    useEffect(() => {
        console.log(formErrors);
        if (Object.keys(formErrors).length === 0 && isSubmit) {
            console.log(formValues);
        }
    }, [formErrors]);

    const validate = (values) => {
        const errors = {};
        const regex = /^(0|[1-9]\d*)([.]\d+)?/i;
        if (!values.name) {
            errors.name = "Name is required!";
        }
        if (!values.price) {
            errors.price = "Price is required!";
        } else if (regex.test(values.price) || values.price < 0) {
            errors.price = "This is not a valid price format!";
        }
        return errors;
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    placeholder="Name"
                    value={formValues.name}
                    onChange={handleChange}
                />
                <p>{formErrors.name}</p>

                <input
                    type="number"
                    name="price"
                    placeholder="Price"
                    value={formValues.price}
                    onChange={handleChange}
                    min={0}
                />
                <p>{formErrors.price}</p>

                <button className="button dark color-red" onClick={() => {
                    setActive(false)
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                    addSubscription(formValues.name,formValues.price).then(r => r)
                }}> Create
                </button>
            </form>
        </div>
    );
};

export default AddSubscription;