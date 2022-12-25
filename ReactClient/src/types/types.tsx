export interface IDepartment {
    id: number,
    name: string
}

export interface IEmployee {
    id: number,
    firstName: string,
    lastName: string,
    fatherName: string,
    position: string,
    salary: number,
    departmentId: number,
    departmentName: string
    //department: IDepartment
}