import * as React from 'react'
import { IDepartment, IEmployee } from '../types/types';
import { variables } from '../Variables';
import EmployeeList from './EmployeeList';

const EmployeesPage: React.FC = () => {
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
        <EmployeeList employees={employees} />
    );
};

export default EmployeesPage;