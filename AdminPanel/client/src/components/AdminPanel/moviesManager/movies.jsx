import React, {useEffect, useState} from 'react';
import Movie from "./movie/movie";
import AddMovie from "./movie/addMovie/addMovie";
import Modal from "../../../utils/modal/modal";
import EditSubscription from "../subscriptionsManager/subscription/editSubscription/editSubscription";
import {removeSubscription} from "../../../actions/subscription";
import EditMovie from "./movie/editMovie/editMovie";
import {deleteFilm} from "../../../actions/film";
import ReactPlayer from "react-player";

const Movies = () => {
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [data, setData] = useState([]);
    const [genres, setGenres] = useState([])
    const [buttonClick, setButtonClicked] = useState(false)
    const [acceptedReviews, setAcceptedReviews] = useState([])



    useEffect( () => {
        try{
            async function fetchMovie(){
                let response = await fetch("http://localhost:5000/films/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchMovie().then(r => r);

            async function fetchGenres(){
                let response = await fetch("http://localhost:5000/genre/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setGenres(res))

            }
            fetchGenres().then(r => r);

            try{
                async function fetchReviews(){
                    let response = await fetch("http://localhost:5000/review/allByStatus?status=accepted", {
                        method: "GET",
                        headers: {
                            "Authorization": localStorage.getItem("token")
                        }
                    })
                        .then(res => res.json())
                        .then(res => setAcceptedReviews(res))

                }
                fetchReviews().then(r => r);
            }
            catch (e){

            }
            setButtonClicked(false)

        }
        catch (e){

        }
    }, [buttonClick]);


    const handleAction = (id) => {
        setData(data
            .filter((item) => item.Id !== id)
        );
    };


    if(data != null){
        return (
            <div className='roleList'>
                <div className="layout">
                    <div className="block horizontal">
                        <div className="block bg-secondary-dark shadow">
                            <div className="text-secondary shadow"></div>
                            <div className="text-secondary shadow">Status</div>
                            <div className="text-primary color-dark shadow"></div>
                        </div>
                        <div className="block bg-magenta shadow">
                            <div className="text-secondary color-secondary-dark">Count</div>
                            <div className="text-primary color-dark shadow"> {data.length}</div>
                        </div>
                        <div className="block bg-light">
                            <div className="text-secondary shadow">Manager</div>
                            <button className="button dark color-light" onClick={() => {
                                setModalActive(true)
                                setModalChild(<AddMovie setModalActive={setModalActive} genres={genres}/>)
                            }}> Add Movie </button>
                            <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Poster</th>
                                <th>title</th>
                                <th>description</th>
                                <th>Age Rating</th>
                                <th>Actions</th>
                            </tr>
                            {data
                                .sort((a, b) => a.title > b.title ? 1 : -1)
                                .map(movie =>{
                                    return <tr key={movie.Id}>
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
                                            <div className="text-primary color-light">{movie.description}</div>
                                        </td>

                                        <td>
                                            <div className="text-primary color-light">{movie.ageRation}+</div>
                                        </td>

                                        <td>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        setModalActive(true)
                                                        setModalChild(<EditMovie movie={movie} setModalActive={setModalActive} allGenres={genres} />)
                                                    }}>Edit
                                            </button>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        setModalActive(true)
                                                        setModalChild(<div className="text-primary color-dark">
                                                            <div>
                                                                {movie.title}
                                                            </div>
                                                            <ReactPlayer controls url={movie.videoLink} />
                                                        </div>)
                                                    }}>Watch
                                            </button>
                                            <button className="button dark color-light"
                                                    onClick={async () => {
                                                        handleAction(movie.Id)
                                                        await deleteFilm(movie.Id)
                                                    }}>Remove
                                            </button>
                                        </td>
                                    </tr>
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
                <Modal active={modalActive} setActive={setModalActive} setButtonClicked={setButtonClicked}>
                    {modalChild}
                </Modal>
            </div>
        );
    }
    else{
        return <span>Loading...</span>

    }

};

export default Movies;