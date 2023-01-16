import {FC, useEffect, useState} from 'react'
import {IDepartment} from '../types/types';
import {variables} from '../Variables';
import DepartmentList from './DepartmentList';

const DepartmentsPage: FC = () => {
    const [departments, setDepartments] = useState<IDepartment[]>([]);

    useEffect(() => {
        fetchDepartments();
    }, []);

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
        <DepartmentList departments={departments} />
    );
};

export default DepartmentsPage;