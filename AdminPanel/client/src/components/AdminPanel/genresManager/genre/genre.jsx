import React, {useState} from 'react';
import {removeGenre} from "../../../../actions/genre";
import RenameGenre from "./renameGenre/renameGenre";

const Genre = ({genre, setModalChild, setModalActive}) => {
    return (
        <tr>
            <td>
                <div className="text-primary color-light">{genre.GenreName}</div>
                <div className="text-secondary">{genre.GenreName}</div>
            </td>
            <td>
                <button className="button dark color-light" onClick={() => {
                    removeGenre(genre.Id).then(r => r)
                }}>Remove
                </button>

                <button className="button dark color-light" onClick={() => {
                    setModalActive(true)
                    setModalChild(<RenameGenre genre={genre} setModalActive={setModalActive} />)
                }}>Rename
                </button>
            </td>
        </tr>

    );
};

export default Genre;