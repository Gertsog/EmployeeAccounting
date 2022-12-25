import * as React from 'react'
import { IDepartment } from '../types/types';
import DepartmentItem from './DepartmentItem';

interface DepartmentListProps {
    departments: IDepartment[]
}

const DepartmentList: React.FC<DepartmentListProps> = ({departments}) => {
    return (
        <tbody>
            {departments.map(department =>
                <DepartmentItem key={department.id} department={department} />
            )}
        </tbody>
    );
};

export default DepartmentList;