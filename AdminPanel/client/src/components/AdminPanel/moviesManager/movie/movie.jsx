import React from 'react';
import EditSubscription from "../../subscriptionsManager/subscription/editSubscription/editSubscription";
import {removeSubscription} from "../../../../actions/subscription";
import EditMovie from "./editMovie/editMovie";
import {deleteFilm} from "../../../../actions/film";

const Movie = ({movie, setModelActive}) => {
    return (
        <tr>
            <td className="avatar">
                <div className="avatar">
                    <img
                        src={movie.poster}/>
                </div>
            </td>

            <td>
                <div className="text-primary color-light">{movie.title}</div>
            </td>

            <td>
                <div className="text-primary color-light">{movie.userRating}</div>
            </td>

            <td>
                <button className="button dark color-light"
                        onClick={() => {
                            EditMovie(movie,setModelActive)
                        }}>Edit
                </button>
                <button className="button dark color-light"
                        onClick={async () => {
                            await deleteFilm(movie.Id)
                        }}>Remove
                </button>
            </td>
        </tr>
    );
};

export default Movie;