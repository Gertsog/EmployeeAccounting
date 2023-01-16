import {FC, useEffect, useState} from 'react'
import {IDepartment, IEmployee} from '../types/types';
import {variables} from '../Variables';
import EmployeeList from './EmployeeList';

const EmployeesPage: FC = () => {
    const [employees, setEmployees] = useState<IEmployee[]>([]);
    const [departments, setDepartments] = useState<IDepartment[]>([]);

    useEffect(() => {
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
