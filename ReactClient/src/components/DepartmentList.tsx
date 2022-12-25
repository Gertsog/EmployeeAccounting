import * as React from 'react'
import { IDepartment } from '../types/types';
import DepartmentItem from './DepartmentItem';

interface DepartmentListProps {
    departments: IDepartment[]
}

const DepartmentList: React.FC<DepartmentListProps> = ({departments}) => {
    return (
        <table className="table table-striped">
            <thead>
                <tr>
                    <th>
                        DepartmentId
                    </th>
                    <th>
                        DepartmentName
                    </th>
                </tr>
            </thead>
            <tbody>
                {departments.map(department =>
                    <DepartmentItem key={department.id} department={department} />
                )}
            </tbody>
        </table>
    );
};

export default DepartmentList;