import React, {useState} from 'react';
import {removeGenre} from "../../../../actions/genre";
import RenameGenre from "./renameGenre/renameGenre";

const Genre = ({genre}) => {
    return (
            <td>
                <div className="text-primary color-light">{genre.GenreName}</div>
                <div className="text-secondary">{genre.GenreName}</div>
            </td>

    );
};

export default Genre;