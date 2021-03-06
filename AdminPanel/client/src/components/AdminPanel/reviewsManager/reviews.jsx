import React, {useEffect, useMemo, useState} from 'react';
import {removeRole} from "../../../actions/role";
import Modal from "../../../utils/modal/modal";
import Review from "./review/review";
import PublicReview from "./review/publicReview/publicReview";

const Reviews = () => {
    const [data, setData] = useState([]);
    const [modalActive, setModalActive] = useState(false)
    const [modalChild, setModalChild] = useState(<div/>)
    const [movies, setMovies] = useState([])
    const [series, setSeries] = useState([])
    const [buttonClick, setButtonClicked] = useState(false)



    useEffect( () => {
        try{
            async function fetchReviews(){
                let response = await fetch("http://localhost:5000/review/allByStatus?status=pending", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchReviews().then(r => r);

            async function fetchMovie(){
                let response = await fetch("http://localhost:5000/films/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setMovies(res))

            }
            fetchMovie().then(r => r);

            async function fetchSerials(){
                let response = await fetch("http://localhost:5000/series/all", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setSeries(res))

            }
            fetchSerials().then(r => r);
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

    const handleAccepted = () => {
        try{
            async function fetchReviews(){
                let response = await fetch("http://localhost:5000/review/allByStatus?status=accepted", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchReviews().then(r => r);
        }
        catch (e){

        }
    };

    const handleCanceled = () => {
        try{
            async function fetchReviews(){
                let response = await fetch("http://localhost:5000/review/allByStatus?status=canceled", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchReviews().then(r => r);
        }
        catch (e){

        }
    };

    const handlePending = () => {
        try{
            async function fetchReviews(){
                let response = await fetch("http://localhost:5000/review/allByStatus?status=pending", {
                    method: "GET",
                    headers: {
                        "Authorization": localStorage.getItem("token")
                    }
                })
                    .then(res => res.json())
                    .then(res => setData(res))

            }
            fetchReviews().then(r => r);
        }
        catch (e){

        }
    };

    if(data != null){
        return (
            <div className='roleList'>
                <div className="layout">
                    <div className="block horizontal">
                        <div className="block bg-secondary-dark shadow">
                            <div className="text-secondary shadow"></div>
                            <div className="text-secondary shadow">Status</div>
                            <button className="button dark color-light" onClick={handlePending} > Considered <i className="fa-solid fa-hourglass"></i></button>
                            <button className="button dark color-light" onClick={handleAccepted}> Accepted <i className="fa-solid fa-circle-check"></i></button>
                            <button className="button dark color-light" onClick={handleCanceled}> Canceled <i className="fa-solid fa-circle-xmark"></i> </button>
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
                                setModalChild(<PublicReview setActive={setModalActive} movies={movies} series={series}/>)
                            }}> Test review </button>
                            <button className="button dark color-light" onClick={() => setButtonClicked(true)}> Refresh </button>
                        </div>
                    </div>
                    <div className="block bg-secondary-dark shadow">
                        <table className="table">
                            <tbody>

                            <tr className="text-secondary">
                                <th>Content</th>
                                <th>User</th>
                                <th>Text</th>
                                <th>Rating</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                            {data
                                .sort((a, b) => a.PublishTime > b.PublishTime ? 1 : -1)
                                .map(review => <Review key={review.Id} review={review} handleAction={handleAction} /> )}
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

export default Reviews;