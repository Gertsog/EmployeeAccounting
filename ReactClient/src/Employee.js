import React, { Component } from 'react'
import { variables } from './Variables.js';

export class Employee extends Component {

    constructor(props) {
        super(props);

        this.state = {
            departments: [],
            employees: [],
            modalTitle: "",
            Id: 0,
            FirstName: "",
            LastName: "",
            FatherName: "",
            Position: "",
            Salary: 0,
            Department: ""
        }
    }

    refreshList() {
        fetch(variables.API_URL + 'employee')
            .then(respose => respose.json())
            .then(data => {
                this.setState({ employees: data });
            });

        fetch(variables.API_URL + 'department')
            .then(respose => respose.json())
            .then(data => {
                this.setState({ departments: data });
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    changeEmployeeName = (e) => {
        this.setState({ FirstName: e.target.value });
    }

    cahngeDeaprtment = (e) => {
        this.setState({ Department: e.target.value });
    }

    addClick() {
        this.setState({
            modalTitle: "Add Employee",
            Id: 0,
            FirstName: "",
            LastName: "",
            FatherName: "",
            Position: "",
            Salary: 0,
            Department: ""
        });
    }

    editClick(employee) {
        this.setState({
            modalTitle: "Edit Employee",
            Id: employee.Id,
            FirstName: employee.FirstName,
            LastName: employee.LastName,
            FatherName: employee.FatherName,
            Position: employee.Position,
            Salary: employee.Salary,
            Department: employee.Department
        });
    }

    createClick() {
        fetch(variables.API_URL + 'employee', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                FirstName: this.state.FirstName,
                LastName: this.state.LastName,
                FatherName: this.state.FatherName,
                Position: this.state.Position,
                Salary: this.state.Salary,
                Department: this.state.Department
            })
        })
        .then(response => response.json)
        .then((result) => {
            alert(result);
            this.refreshList();
        }, (error) => {
            alert('Failed');
        });
    }

    updateClick() {
        fetch(variables.API_URL + 'employee', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: this.state.Id,
                FirstName: this.state.FirstName,
                LastName: this.state.LastName,
                FatherName: this.state.FatherName,
                Position: this.state.Position,
                Salary: this.state.Salary,
                Department: this.state.Department
            })
        })
        .then(response => response.json)
        .then((result) => {
            alert(result);
            this.refreshList();
        }, (error) => {
            alert('Failed');
        });
    }

    deleteClick() {
        if (window.confirm('Are you sure?')) {
            //fetch(variables.API_URL + 'employee/' + id, {
            fetch(variables.API_URL + 'employee', {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Id: this.state.Id,
                    FirstName: this.state.FirstName,
                    LastName: this.state.LastName,
                    FatherName: this.state.FatherName,
                    Position: this.state.Position,
                    Salary: this.state.Salary,
                    Department: this.state.Department
                })
            })
            .then(response => response.json)
            .then((result) => {
                alert(result);
                this.refreshList();
            }, (error) => {
                alert('Failed');
            });
        }
    }

    render() {
        const {
            departments,
            employees,
            modalTitle,
            Id,
            FirstName,
            LastName,
            FatherName,
            Position,
            Salary,
            Department
        } = this.state;

        return (
            <div>
                <button type="button" className="btn btn-primary m-2 float-end"
                    data-bs-toggle="modal" data-bs-target="#exampleModal"
                    onClick={() => this.addClick()}> New Employee
                </button>
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
                        {employees.map(e =>
                            <tr key={e.Id}>
                                <td>
                                    {e.Id}
                                </td>
                                <td>
                                    {e.FirstName}
                                </td>
                                <td>
                                    {e.LastName}
                                </td>
                                <td>
                                    {e.FatherName}
                                </td>
                                <td>
                                    {e.Position}
                                </td>
                                <td>
                                    {e.Salary}
                                </td>
                                <td>
                                    {e.DepartmentName}
                                </td>
                                <td>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal" data-bs-target="#exampleModal"
                                        onClick={() => this.editClick(e)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light mr-1"
                                        data-bs-toggle="modal" data-bs-target="#exampleModal"
                                        onClick={() => this.deleteClick(e)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>
                                </td>
                            </tr>)
                        }
                    </tbody>
                </table>

                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                </button>
                            </div>

                            <div className="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">

                                    <div className="p-2 w-50 bd-highlight">

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">FirstName</span>
                                            <input type="text" className="form-control" value={FirstName}
                                                onChange={this.changeEmployeeName} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Department</span>
                                            <select className="form-select" value={Department}
                                                onChange={this.changeDepartment}>
                                                {departments.map(department => <option key={department.Id}>
                                                    {department.Name}
                                                </option>)}
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                {Id == 0
                                    ? <button type="button" className="btn btn-primary float-start"
                                        onClick={() => this.createClick()}>Create</button>
                                    : null
                                }
                                {Id != 0
                                    ? <button type="button" className="btn btn-primary float-start"
                                        onClick={() => this.updateClick()}>Update</button>
                                    : null
                                }
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        )
    }
}
