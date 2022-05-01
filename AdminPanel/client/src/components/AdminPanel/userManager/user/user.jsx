import React from 'react';
import {useDispatch} from "react-redux";
import {block, unblock} from "../../../../actions/user";

const User = ({user}) => {
    const dispatch = useDispatch()
    console.log(user.EmailConfirmed)
    const emailCong = String(user.EmailConfirmed)
    return (
        <tr>
            <td className="avatar">
                <div className="avatar">
                    <img
                        src={user.Avatar}/>
                </div>
            </td>
            <td>
                <div className="text-primary color-light">{user.UserName}</div>
                <div className="text-secondary">{user.Email}</div>
            </td>
            <td>
                <div className="text-primary color-light">{emailCong}</div>
            </td>
            <td>
                {
                    user.Status === "access" ?
                        <div className="text-primary color-green">{user.Status}</div>

                        :
                        <div className="text-primary color-danger">{user.Status}</div>

                }
            </td>
            <td>
                {
                    user.Status === "access" ?
                    <button className="button dark color-light" onClick={() => dispatch(block(user.Email))}>Block
                    </button>
                        :
                    <button className="button dark color-light" onClick={() => dispatch(unblock(user.Email))}>Unblock
                    </button>
                }

            </td>
        </tr>
    );
};

export default User;