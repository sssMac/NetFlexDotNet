import React, {useEffect, useState} from 'react';
import Modal from "../../../utils/modal/modal";
import Genre from "./genre/genre";
import AddGenre from "./genre/addGenre/addGenre";
import {removeGenre} from "../../../actions/genre";
import RenameGenre from "./genre/renameGenre/renameGenre";

const Genres = () => {
    const [data, setData] = useState([]);
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [buttonClick, setButtonClicked] = useState(false)

    useEffect( () => {
        try{
            async function fetchGenres(){
                let response = await fetch("http://localhost:5000/genre/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchGenres().then(r => r);
            setButtonClicked(false)
        }
        catch (e){

        }
    }, [buttonClick]);

    const handleDelete = (id) => {
        setData(data
            .filter((item) => item.Id !== id)
        );
    };


    if(data != null){
        return (
            <div className='genreList'>
                <div className="layout">
                    <div className="block horizontal">
                        <div className="block bg-secondary-dark shadow">
                            <div className="text-secondary shadow"></div>
                            <div className="text-primary color-dark shadow"></div>
                        </div>
                        <div className="block bg-magenta shadow">
                            <div className="text-secondary color-secondary-dark"></div>
                            <div className="text-primary color-dark shadow"></div></div>
                        <div className="block bg-light">
                            <div className="text-secondary shadow">Manager</div>
                            <button className="button dark color-light" onClick={() => {
                                setModalActive(true)
                                setModalChild(<AddGenre setActive={setModalActive}/>)
                            }}> Add </button>
                            <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Genre</th>
                                <th>Actions</th>
                            </tr>
                            {data
                                .sort((a, b) => a.GenreName > b.GenreName ? 1 : -1)
                                .map(genre =>{
                                return <tr key={genre.Id}>
                                    <Genre genre={genre}/>

                                    <td>
                                        <button className="button dark color-light" onClick={() => {
                                            removeGenre(genre.Id).then(r => r)
                                            handleDelete(genre.Id)
                                        }}>Remove
                                        </button>

                                        <button className="button dark color-light" onClick={() => {
                                            setModalActive(true)
                                            setModalChild(<RenameGenre genre={genre} setActive={setModalActive}/>)
                                        }}>Rename
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

export default Genres;