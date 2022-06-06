import React, {useState} from 'react';
import {useSelector} from "react-redux";
import Input from "../../../../../utils/input/Input";
import {publicReview} from "../../../../../actions/review";
import {useForm} from "react-hook-form";

const AddMovie = ({setModalActive}) => {
    const [textReview, setTextReview] = useState("")
    const [contentId, setContentId] = useState("")
    const [rating, setRating] = useState(0)

    const handleChange = (e) => {
        setTextReview('')
        setContentId('')
        setRating(0)
    }

    const { register, handleSubmit, formState: { errors } } = useForm();
    const onSubmit = data => console.log(data);
    console.log(errors);

    return (
        <div>
            <div>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <input type="text" placeholder="Title" {...register("Title", {required: true, maxLength: 50})} />
                    <textarea {...register("Description", {required: true, maxLength: 500})} />
                    <input type="url" placeholder="Poster" {...register("Poster", {required: true})} />
                    <select {...register("AgeRating", { required: true })}>
                        <option value="0">0+</option>
                        <option value="6">6+</option>
                        <option value="12">12+</option>
                        <option value="18">18+</option>
                    </select>
                    <input type="url" placeholder="VideoLink" {...register("VideoLink", {required: true})} />

                    <button className="button dark color-red" onClick={() => {
                        setModalActive(false)
                        handleChange()
                    }}> Cancel
                    </button>

                    <button className="button dark color-green" onClick={() => {
                        setModalActive(false)
                    }}> Create
                    </button>
                </form>





            </div>
        </div>
    );
};

export default AddMovie;