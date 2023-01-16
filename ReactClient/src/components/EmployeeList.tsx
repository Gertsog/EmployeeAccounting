import {FC} from 'react'
import {IEmployee} from '../types/types';
import EmployeeItem from './EmployeeItem';
import {variables} from '../Variables';

interface EmployeeListProps {
    employees: IEmployee[]
}

const handleDelete = (employee: IEmployee) => {
    if (window.confirm('Are you sure?')) {
        //fetch(variables.API_URL + 'employee/' + id, {
        fetch(variables.API_URL + 'employee', {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: employee.id,
                firstName: employee.firstName,
                lastName: employee.lastName,
                fatherName: employee.fatherName,
                position: employee.position,
                salary: employee.salary,
                departmentId: employee.departmentId,
                departmentName: employee.departmentName
            })
        })
        .then(response => response.json)
        .then((result) => {
            alert(result);
        }, (error) => {
            alert('Failed');
        });
    }
}

const EmployeeList: FC<EmployeeListProps> = ({employees}) => {
    return (
        <table className="table table-striped">
            <thead>
                <tr>
                    <th>EmployeeId</th>
                    <th>FirstName</th>
                    <th>LastName</th>
                    <th>FatherName</th>
                    <th>Position</th>
                    <th>Salary</th>
                    <th>Department</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee =>
                    <EmployeeItem key={employee.id} employee={employee} onDelete={handleDelete} />
                )}
            </tbody>
        </table>
    );
};

export default EmployeeList;