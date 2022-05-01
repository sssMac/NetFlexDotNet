import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './components/App';
import  {store} from "./reducers";
import  {Provider} from "react-redux";
import {BrowserRouter} from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <Provider store={store}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </Provider>

);

