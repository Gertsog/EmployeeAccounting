import './App.css';
import * as React from 'react';
import { BrowserRouter, NavLink, Route, Routes } from 'react-router-dom';
import EmployeesPage from './components/EmployeesPage';
import DepartmentsPage from './components/DepartmentsPage';

const App = () => {

    return (
        <div className="App container">
            <BrowserRouter>
                <div>
                    <div>
                        <NavLink to="/employees" className="btn btn-light btn-outline-primary">
                            Employees
                        </NavLink>
                        <NavLink to="/departments" className="btn btn-light btn-outline-primary">
                            Departments
                        </NavLink>
                    </div>
                    <Routes>
                        <Route path="/employees" element={<EmployeesPage />} />
                        <Route path="/departments" element={<DepartmentsPage />} />
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    );
};

export default App;
