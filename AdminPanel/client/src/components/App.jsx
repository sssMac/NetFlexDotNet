import React, {useEffect} from "react";
import './app.scss'
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {auth} from "../actions/user";
import {privateRoutes, publicRoutes} from "../routes/Routes";
import AdminPanel from "./AdminPanel/AdminPanel";
import {logout} from "../reducers/userReducer";


function App() {
    const isAuth = useSelector(state => state.user.isAuth)
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(auth())
    }, [])

  return (

      <BrowserRouter>
          <div className="app">
              {!isAuth ?
                  <Switch>
                      {publicRoutes.map((route) => (
                          <Route
                              component={route.component}
                              path={route.path}
                              exact={route.exact}
                              key={route.path}
                          />
                      ))}
                  </Switch>
                  :
                  <Switch>
                        <AdminPanel>
                            {privateRoutes.map((route) => (
                                <Route
                                    component={route.component}
                                    path={route.path}
                                    exact={route.exact}
                                    key={route.path}
                                />
                            ))}
                            <Redirect to="/adminPanel"/>
                        </AdminPanel>
                  </Switch>
              }

          </div>
      </BrowserRouter>
  );
}

export default App;
