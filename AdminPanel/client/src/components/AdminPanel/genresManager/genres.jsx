import React, {useEffect, useState} from 'react';
import Modal from "../../../utils/modal/modal";
import Genre from "./genre/genre";
import AddGenre from "./genre/addGenre/addGenre";

const Genres = () => {
    const [data, setData] = useState(null);
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
                                setModalChild(<AddGenre />)
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
                            {data.map(genre => <Genre key={genre.Id} genre={genre} modalChild={modalChild} setModalChild={setModalChild} setModalActive={setModalActive}/>) }
                            </tbody>
                        </table>
                    </div>
                </div>
                <Modal active={modalActive} setActive={setModalActive}>
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