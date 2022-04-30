import React, {useEffect, useState} from 'react';
import './adminPanel.scss'
import {NavLink} from "react-router-dom";

const AdminPanel = ({children}) => {

    return (
        <div className="AdminPanel">
            <div className="LogoManager">
                <img />
            </div>
            <div className="tabMenu">
                <ul className="tabBtns">
                    <li className="tabLi" >
                        <NavLink className="tabBtnLink" to="/AdminPanel/Users" >
                            <button className="tabBtn">
                                Users
                            </button>
                        </NavLink>
                    </li>
                    <li className="tabLi">
                        <NavLink className="tabBtnLink" to="/AdminPanel/Roles">
                            <button className="tabBtn">
                                Roles
                            </button>
                        </NavLink>
                    </li>
                    <li className="tabLi">
                        <NavLink className="tabBtnLink" to="/AdminPanel/Subscriptions">
                            <button className="tabBtn">
                                Subscriptions
                            </button>
                        </NavLink>
                    </li>
                    <li className="tabLi">
                        <NavLink className="tabBtnLink" to="/AdminPanel/Movies">
                            <button className="tabBtn">
                                Movies
                            </button>
                        </NavLink>
                    </li>
                    <li className="tabLi">
                        <NavLink className="tabBtnLink" to="/AdminPanel/Serials">
                            <button className="tabBtn">
                                Serials
                            </button>
                        </NavLink>
                    </li>
                    <li className="tabLi">
                        <NavLink className="tabBtnLink" to="/AdminPanel/Genres">
                            <button className="tabBtn">
                                Genres
                            </button>
                        </NavLink>
                    </li>
                </ul>
            </div>
            {children}
        </div>

    );
};

export default AdminPanel;

/*
* <div className="test">
                {
                    data ? (
                        data.map(item => <>
                            <span>{item.username}</span>
                            <span>{item.email}</span>
                        </>)
                    ) : <span>Загрузка...</span>
                }
            </div>
* */