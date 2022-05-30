import React, {useState} from 'react';
import {useSelector} from "react-redux";
import Input from "../../../../../utils/input/Input";
import {publicReview} from "../../../../../actions/review";

const AddMovie = ({setModalActive}) => {
    const [textReview, setTextReview] = useState("")
    const [contentId, setContentId] = useState("")
    const [rating, setRating] = useState(0)

    const handleChange = (e) => {
        setTextReview('')
        setContentId('')
        setRating(0)
    }

    return (
        <div>
            <div>
                <div className="textbox">
                    <Input type="text" value={contentId} setValue={setContentId} placeholder="ContentId..." />
                </div>
                <div className="textbox">
                    <Input type="text" value={textReview} setValue={setTextReview} placeholder="Typing..." maxlength={2000}/>
                </div>
                <div className="textbox">
                    <Input type="number" value={rating} setValue={setRating} placeholder="Rating..." min={0} max={10}/>
                </div>

                <button className="button dark color-red" onClick={() => {
                    setModalActive(false)
                    handleChange()
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                    setModalActive(false)
                }}> Create
                </button>



            </div>
        </div>
    );
};

export default AddMovie;