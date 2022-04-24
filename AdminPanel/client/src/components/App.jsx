import React, {useEffect} from "react";
import './app.css'
import {BrowserRouter, Route, Switch} from "react-router-dom";
import Registration from "./autorization/registration";
import Login from "./autorization/login";
import {useDispatch, useSelector} from "react-redux";
import {auth} from "../actions/user";

function App() {
    const isAuth = useSelector(state => state.user.isAuth)
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(auth())
    }, [])

  return (

      <BrowserRouter>
          <div className="app">
              {!isAuth &&
                  <Switch>
                      <Route path="/signup" component={Registration}/>
                      <Route path="/signin" component={Login}/>
                  </Switch>
              }

          </div>
      </BrowserRouter>
  );
}

export default App;
