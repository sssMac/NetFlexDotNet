import React, {useState} from 'react';
import {useDispatch} from "react-redux";
import Input from "../../../../../utils/input/Input";
import {addGenre} from "../../../../../actions/genre";

const AddGenre = () => {
    const [genreName, setGenreName] = useState()

    return (
        <div>
            <label className="modal__head">Add new genre</label>
            <div className="textbox">
                <Input type="text" value={genreName} setValue={setGenreName} placeholder="Genre..." />
            </div>

            <button className="button light color-dark" onClick={() => addGenre(genreName)}> Add
            </button>

        </div>
    )
};

export default AddGenre;