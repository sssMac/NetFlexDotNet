import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import {renameGenre} from "../../../../../actions/genre";

const RenameGenre = ({genre,setActive}) => {
    const [newName, setNewName] = useState('')

    const handleClear = (e: any) =>  {
        e.preventDefault()
        setNewName('')
    }
    const handleChange = (e: any) => setNewName('')


    return (
        <div>
            <div className="textbox">
                <Input type="text" value={newName} setValue={setNewName} placeholder={genre.GenreName} />
            </div>

            <button className="button dark color-red" onClick={() => {
                setActive(false)
            }}> Cancel
            </button>

            <button className="button dark color-green" onClick={() => {
                setActive(false)
                renameGenre(genre.Id, newName).then(r => r)
                handleChange()
            }}> Submit
            </button>

        </div>
    )
};

export default RenameGenre;