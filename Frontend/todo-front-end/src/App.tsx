import React from 'react';
import './App.css';
import MainPanel from "./components/MainPanel/MainPanel";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import RegisterComponent from "./components/RegisterComponent/RegisterComponent"
import LoginComponent from "./components/LoginComponent/LoginComponent";


function App() {
    return (
        <div className="App">
            <Router>
                <Routes>
                    <Route path="/"         element={<MainPanel/>}/>
                    <Route path="/register" element={<RegisterComponent/>}/>
                    <Route path="/login"    element={<LoginComponent/>}/>
                </Routes>
            </Router>
        </div>
    );
}

export default App;
