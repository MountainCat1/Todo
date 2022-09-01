import React from 'react';
import './App.css';
import MainPanel from "./components/MainPanel/MainPanel";
import {BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import { RegisterComponent } from "./components/RegisterComponent/RegisterComponent"


function App() {
  return (
      <div className="App">
          <Router>
              <Routes>
                  <Route path="/"  element={<MainPanel/>} />
                  <Route path="/register" element={<RegisterComponent/>}/>
              </Routes>
          </Router>
    </div>
  );
}

export default App;
