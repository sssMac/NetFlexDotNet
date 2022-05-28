import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import {createRole} from "../../../../../actions/role";
import {publicReview} from "../../../../../actions/review";
import {useSelector} from "react-redux";

const PublicReview = ({setActive}) => {
    const [textReview, setTextReview] = useState("")
    const [contentId, setContentId] = useState("")
    const [rating, setRating] = useState(0)

    const user = useSelector(state => state.user)

    const handleClear = (e) =>  {
        e.preventDefault()
        setTextReview('')
        setContentId('')
        setRating(0)
    }
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
                    setActive(false)
                    handleChange()
                }}> Cancel
                </button>

                <button className="button dark color-green" onClick={() => {
                    publicReview(user.currentUser.UserName, contentId, textReview, rating).then(r => r)
                    setActive(false)
                }}> Create
                </button>



            </div>
        </div>
    );
};

export default PublicReview;