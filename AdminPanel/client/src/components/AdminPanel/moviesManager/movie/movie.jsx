import React from 'react';

const Movie = ({movie}) => {
    return (
        <tr>
            <td>
                <div className="text-primary color-light">{movie.Poster}</div>
                <div className="text-secondary">{movie.Id}</div>
            </td>

            <td>
                <div className="text-primary color-light">{movie.Title}</div>
            </td>

            <td>
                <div className="text-primary color-light">{movie.UserRating}</div>
            </td>

            <td>
                <div className="text-primary color-light">ACTION</div>
            </td>
        </tr>
    );
};

export default Movie;