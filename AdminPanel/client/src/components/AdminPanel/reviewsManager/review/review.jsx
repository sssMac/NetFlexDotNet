import React, {useEffect, useState} from 'react';
import {acceptReview, cancelReview, pendingReview} from "../../../../actions/review";

const Review = ({review,handleAction}) => {
    const [firstBtn, setFirstBtn] = useState( <button></button>)

    const [secondBtn, setSecondBtn] = useState( <button></button>)


    useEffect(() => {
        if(review.Status === "accepted"){
            setFirstBtn(<button className="button dark color-yellow"
                                onClick={() => {
                                    pendingReview(review.Id).then(r => r)
                                    handleAction(review.Id)
                                }}> Сonsider </button>)
            setSecondBtn(<button className="button dark color-red"
                                 onClick={() => {
                                     cancelReview(review.Id).then(r => r)
                                     handleAction(review.Id)
                                 }}> Cancel </button>)
        }
        else if(review.Status === "canceled"){
            setFirstBtn(<button className="button dark color-yellow"
                                onClick={() => {
                                    pendingReview(review.Id).then(r => r)
                                    handleAction(review.Id)
                                }}> Сonsider </button>)
            setSecondBtn(<button className="button dark color-green"
                                 onClick={() => {
                                     acceptReview(review.Id).then(r => r)
                                     handleAction(review.Id)
                                 }}> Accept </button>)
        }
        else{
            setFirstBtn(<button className="button dark color-green"
                                onClick={() => {
                                    acceptReview(review.Id).then(r => r)
                                    handleAction(review.Id)
                                }}> Accept </button>)
            setSecondBtn(<button className="button dark color-red"
                                 onClick={() => {
                                     cancelReview(review.Id).then(r => r)
                                     handleAction(review.Id)
                                 }} > Cancel </button>)
        }
    })


    return (
        <tr>
            <td>
                <div className="text-primary color-light">{review.ContentId}</div>
                <div className="text-secondary">{review.ContentId}</div>
            </td>
            <td>
                <div className="text-primary color-light">{review.UserName}</div>
            </td>
            <td>
                <div className="text-primary color-light">
                    <div className="review">
                        <input className="show_more" type="checkbox"/>
                        <div className="show_more_button">
                            <div className="label"></div>
                        </div>
                        <div className="text">{review.Text}
                        </div>
                    </div>
                </div>
            </td>
            <td>
                <div className="text-primary color-light">{review.Rating}</div>
            </td>
            <td>
                <div className="text-primary color-light">{review.Status}</div>
            </td>
            <td>
                {firstBtn}
                {secondBtn}
            </td>

        </tr>


    );
};

export default Review;