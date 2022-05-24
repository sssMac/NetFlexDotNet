import React, {useEffect, useState} from 'react';
import './PanelsUI-v2.1.min.css'
import {logout} from "../../reducers/userReducer";
import {useDispatch, useSelector} from "react-redux";
import {Link, NavLink, Outlet} from "react-router-dom";


const AdminPanel = () => {
    const user = useSelector(state => state.user)
    const setActive = ({isActive}) => isActive ? "color-magenta" : "color-light";

    const dispatch = useDispatch();
    const handleLogout = (e) => {
        e.preventDefault();

        dispatch(logout())
    }

    return (
        <div className="AdminPanel">
            <div className="menu">
                <NavLink to="AdminPanel/Users" className={setActive}>
                    <div className="menu_button" data-text="users"><i className="fa-solid fa-users" ></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Roles" className={setActive}>
                    <div className="menu_button" data-text="roles"><i className="fa-solid fa-table-list"></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Movies" className={setActive}>
                    <div className="menu_button" data-text="Movies"><i className="fa-solid fa-video"></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Serials" className={setActive}>
                    <div className="menu_button" data-text="Serials"><i className="fa-solid fa-film"></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Genres" className={setActive}>
                    <div className="menu_button" data-text="Genres"><i className="fa-solid fa-bars-staggered"></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Reviews" className={setActive}>
                    <div className="menu_button" data-text="Reviews"><i className="fa-solid fa-book-bookmark"></i>
                    </div>
                </NavLink>
                <NavLink to="AdminPanel/Subscriptions" className={setActive}>
                    <div className="menu_button" data-text="Subscriptions"><i className="fa-solid fa-circle-dollar-to-slot"></i>
                    </div>
                </NavLink>

            </div>
            <div className="heading">
                <div className="block">
                    <div className="text-secondary">Welcome to</div>
                    <div className="text-primary color-light">NetFlex</div>
                </div>
                <div className="block right inline color-light">
                    <div className="menu_button" data-text="Logout">
                        <i onClick={(e)=> handleLogout(e)}  className="fa-solid fa-arrow-right-from-bracket">
                            <Link to='SignIn'/>
                        </i>
                    </div>
                </div>
                <div className="icon">
                            <img
                                src={user.currentUser.Avatar}/>
                </div>
                <div className="text-primary color-light">{user.currentUser.UserName}</div>
            </div>
            <div className="contentContainer">
                <Outlet />
            </div>

        </div>
    );
};

export default AdminPanel;