import React, {useEffect, useState} from 'react';
import Movie from "./movie/movie";
import AddMovie from "./movie/addMovie/addMovie";
import Modal from "../../../utils/modal/modal";

const Movies = () => {
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [data, setData] = useState([]);
    const [genres, setGenres] = useState([])
    const [buttonClick, setButtonClicked] = useState(false)



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
            setButtonClicked(false)
        }
        catch (e){

        }

        try{
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
                                setModalChild(<AddMovie setActive={setModalActive} genres={genres}/>)
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
                                <th>Rating</th>
                                <th>Actions</th>
                            </tr>
                            {data
                                .map(movie => <Movie key={movie.Id} movie={movie} handleAction={handleAction} /> )}
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