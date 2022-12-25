import * as React from 'react'
import { IEmployee } from '../types/types';
import EmployeeItem from './EmployeeItem';

interface EmployeeListProps {
    employees: IEmployee[]
}

const EmployeeList: React.FC<EmployeeListProps> = ({employees}) => {
    return (
        <table className="table table-striped">
            <thead>
                <tr>
                    <th>
                        EmployeeId
                    </th>
                    <th>
                        FirstName
                    </th>
                    <th>
                        LastName
                    </th>
                    <th>
                        FatherName
                    </th>
                    <th>
                        Position
                    </th>
                    <th>
                        Salary
                    </th>
                    <th>
                        Department
                    </th>
                </tr>
            </thead>
            <tbody>
                {employees.map(employee =>
                    <EmployeeItem key={employee.id} employee={employee} />
                )}
            </tbody>
        </table>
    );
};

export default EmployeeList;