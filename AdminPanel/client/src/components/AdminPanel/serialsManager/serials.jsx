import React, {useEffect, useState} from 'react';

import Modal from "../../../utils/modal/modal";


import {deleteFilm} from "../../../actions/film";
import AddSerial from "./addSerial/addSerial";
import EditSerial from "./editSerial/editSerial";
import {deleteSeries} from "../../../actions/series";
import AddEpisdoes from "./addEpisodes/addEpisdoes";
import ReactPlayer from "react-player";

const Serials = () => {
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [data, setData] = useState([]);
    const [genres, setGenres] = useState([])
    const [numEp, setNumEp] = useState(0)
    const [buttonClick, setButtonClicked] = useState(false)



    useEffect( () => {
        try{
            async function fetchSerials(){
                let response = await fetch("http://localhost:5000/series/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchSerials().then(r => r);

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


    const handleDelete = (id) => {
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
                                setModalChild(<AddSerial setActive={setModalActive} genres={genres}/>)
                            }}> Add Series </button>
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
                                .map(serial =>{
                                    return <tr key={serial.Id}>
                                        <td className="avatar">
                                            <div className="avatar">
                                                <img
                                                    src={serial.poster}/>
                                            </div>
                                        </td>

                                        <td>
                                            <div className="text-primary color-light">{serial.title}</div>
                                        </td>

                                        <td>
                                            <div className="text-primary color-light">{serial.description}</div>
                                        </td>

                                        <td>
                                            <div className="text-primary color-light">{serial.ageRation}+</div>
                                        </td>


                                        <td>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        setModalActive(true)
                                                        setModalChild(<EditSerial serial={serial} setActive={setModalActive} allGenres={genres} />)
                                                    }}>Edit
                                            </button>
                                            <button className="button dark color-light"
                                                    onClick={() => {
                                                        setModalActive(true)
                                                        setModalChild(<AddEpisdoes serial={serial} setModalChild={setModalChild} setModalActive={setModalActive} setNumEp={setNumEp} />)
                                                    }}>Episodes
                                            </button>
                                            <button className="button dark color-light"
                                                    onClick={async () => {
                                                        handleDelete(serial.Id)
                                                        await deleteSeries(serial.Id)
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

export default Serials;