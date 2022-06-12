import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import Input from "../../../../../utils/input/Input";
import {addGenre} from "../../../../../actions/genre";

const AddGenre = ({setActive}) => {
    const [genreName, setGenreName] = useState('')

    const handleClear = (e) =>  {
        e.preventDefault()
        setGenreName('')
    }
    const handleChange = (e) => setGenreName('')

    return (
        <div>
            <div className="textbox">
                <Input type="text" value={genreName} setValue={setGenreName} placeholder="Add genre..." />
            </div>

            <button className="button dark color-red" onClick={() => {
                setActive(false)
                handleChange()
            }}> Cancel
            </button>

            <button className="button dark color-green" onClick={e => {
                e.stopPropagation()
                addGenre(genreName).then(r => r)
                setActive(false)
            }}> Submit
            </button>



        </div>
    )
};

export default AddGenre;