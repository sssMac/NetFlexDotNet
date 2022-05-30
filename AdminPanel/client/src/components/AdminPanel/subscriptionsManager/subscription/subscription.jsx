import React from 'react';
import {removeSubscription} from "../../../../actions/subscription";
import EditSubscription from "./editSubscription/editSubscription";

const Subscription = ({subscription,handleDelete,setModalActive}) => {
    return (
        <tr>
            <td>
                <div className="text-primary color-light">{subscription.Name}</div>
            </td>
            <td>
                <div className="text-primary color-light">{subscription.Cost}</div>
            </td>

            <td>
                <button className="button dark color-light"
                        onClick={() => {
                            EditSubscription(subscription,setModalActive).then(r => r)
                        }}>Edit
                </button>
                <button className="button dark color-light"
                        onClick={() => {
                            removeSubscription(subscription.Id).then(r => r)
                            handleDelete(subscription.Id)
                        }}>Remove
                </button>
            </td>
        </tr>
    );
};

export default Subscription;