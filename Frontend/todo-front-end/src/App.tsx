import React from 'react';
import './App.css';
import MainPanelComponent from "./components/MainPanelComponent/MainPanelComponent";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import RegisterComponent from "./components/authentication/RegisterComponent/RegisterComponent"
import LoginComponent from "./components/authentication/LoginComponent/LoginComponent";


function App() {
    return (
        <div className="App">
            <Router>
                <Routes>
                    <Route path="/"         element={<MainPanelComponent/>}/>
                    <Route path="/register" element={<RegisterComponent/>}/>
                    <Route path="/login"    element={<LoginComponent/>}/>
                </Routes>
            </Router>
        </div>
    );
}

export default App;
