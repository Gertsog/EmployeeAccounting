import './App.css';
import * as React from 'react';
import EmployeeList from './components/EmployeeList';
import { IDepartment, IEmployee } from './types/types';
import { variables } from './Variables';

const App = () => {
    const [employees, setEmployees] = React.useState<IEmployee[]>([]);
    const [departments, setDepartments] = React.useState<IDepartment[]>([]);

    React.useEffect(() => {
        fetchEmployees();
        fetchDepartments();
    }, []);

    async function fetchEmployees() {
        try {
            fetch(variables.API_URL + 'employee')
            .then(respose => respose.json())
            .then(setEmployees);
        } catch (e) {
            alert(e);
        }
    }

    async function fetchDepartments() {
        try {
            fetch(variables.API_URL + 'department')
            .then(respose => respose.json())
            .then(setDepartments);
        } catch (e) {
            alert(e);
        }
    }

    return (
        <div className="App container">
            <EmployeeList employees={employees} />
        </div>
    );
};

export default App;
