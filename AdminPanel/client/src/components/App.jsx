import React, {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {auth} from "../actions/user";
import {Routes, Route, Navigate} from 'react-router-dom'
import { css } from "@emotion/react";
import * as ReactBootStrap from 'react-bootstrap'
import './app.scss'
import BarLoader from 'react-spinners/BarLoader'

import AdminPanel from "./AdminPanel/AdminPanel";
import Login from "./AuthorizePanel/RegLog/login";
import Registration from "./AuthorizePanel/RegLog/registration";
import Users from "./AdminPanel/userManager/users";
import Movies from "./AdminPanel/moviesManager/movies";
import Serials from "./AdminPanel/serialsManager/serials";
import Roles from "./AdminPanel/rolesManager/roles";
import Genres from "./AdminPanel/genresManager/genres";
import Subscriptions from "./AdminPanel/subscriptionsManager/subscriptions";
import AuthorizePanel from "./AuthorizePanel/AuthorizePanel";
import Reviews from "./AdminPanel/reviewsManager/reviews";
import Modal from "../utils/modal/modal";




function App() {
    const isAuth = useSelector(state => state.user.isAuth)
    const dispatch = useDispatch()
    const [loading, setLoading] = useState(false);

    const override = css`
      display: block;
      margin-top: 25%;
      margin-left: 45%;
      border-color: red;
      z-index: 9999;
`;

    useEffect(() => {
        dispatch(auth());
        setLoading(true);
        setTimeout(() => {
            setLoading(false)
        }, 2000)
    }, [isAuth])

  return (
      <div className="app">
          {loading ? (
              <BarLoader color={"#A80D20"} loading={loading} css={override} size={150} />
              ) : (
              !isAuth ?
                  <Routes>
                      <Route path="/" element={<AuthorizePanel />}>
                          <Route index path="SignIn" element={<Login />}/>
                          <Route path="/SignUp" element={<Registration />}/>
                          <Route path="/" element={<Navigate to='SignIn' replace />}/>
                          <Route path="*" element={<Navigate to='SignIn' replace />}/>
                      </Route>
                  </Routes>
                  :
                  <Routes>
                      <Route path="/" element={<AdminPanel />} >
                          <Route index path="AdminPanel/Users" element={<Users />}/>
                          <Route path="AdminPanel/Movies" element={<Movies />}/>
                          <Route path="AdminPanel/Serials" element={<Serials />}/>
                          <Route path="AdminPanel/Roles" element={<Roles />}/>
                          <Route path="AdminPanel/Genres" element={<Genres />}/>
                          <Route path="AdminPanel/Reviews" element={<Reviews />}/>
                          <Route path="AdminPanel/Subscriptions" element={<Subscriptions />}/>
                          <Route path="/" element={<Navigate to='AdminPanel/Users' replace />}/>
                          <Route path="*" element={<Navigate to='AdminPanel/Users' replace />}/>
                      </Route>
                  </Routes> )
              }
      </div>
  );
}

export default App;
