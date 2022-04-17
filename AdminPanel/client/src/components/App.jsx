import React from "react";
import './app.css'
import {BrowserRouter, Route, Switch} from "react-router-dom";
import Registration from "./SignUp/registration";
import Login from "./SignIn/login";

function App() {
  return (
      <BrowserRouter>
          <div className="app">
              <Switch>
                  <Route path="/signup" component={Registration}/>
              </Switch>
              <Switch>
                  <Route path="/signin" component={Login}/>
              </Switch>
          </div>
      </BrowserRouter>
  );
}

export default App;
