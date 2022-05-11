import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import {renameGenre} from "../../../../../actions/genre";

const RenameGenre = (genre, setModalActive) => {
    console.log(genre)
    const [newName, setNewName] = useState(genre.genre.GenreName)

    return (
        <div>
            <label className="modal__head">Rename</label>
            <div className="textbox">
                <Input type="text" value={newName} setValue={setNewName} placeholder={genre.genre.GenreName} />
            </div>

            <button className="button light color-dark" onClick={() => {
                renameGenre(genre.genre.Id, newName)
                setModalActive(false)
            }}> Save
            </button>

        </div>
    )
};

export default RenameGenre;