import React, { Component } from 'react';
import { variables } from './Variables.js';

export class Employee extends Component {
//сделано максимально не по канону, когда-нибудь я займусь переделкой
    constructor(props) {
        super(props);

        this.state = {
            departments: [],
            employees: [],
            modalTitle: "",
            id: 0,
            firstName: "",
            lastName: "",
            fatherName: "",
            position: "",
            salary: 0,
            departmentName: "",
            firstNameFilter: "",
            lastNameFilter: "",
            fatherNameFilter: "",
            positionFilter: "",
            salaryFilter: 0,
            departmentNameFilter: "",
            employeesWithoutFilter: []
        }
    }

    refreshList() {
        fetch(variables.API_URL + 'employee')
            .then(respose => respose.json())
            .then(data => {
                this.setState({ employees: data, employeesWithoutFilter: data });
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

    FilterFn() {
        var firstNameFilter = this.state.firstNameFilter;
        var lastNameFilter = this.state.lastNameFilter;
        var fatherNameFilter = this.state.fatherNameFilter;
        var positionFilter = this.state.positionFilter;
        var salaryFilter = this.state.salaryFilter;
        var departmentNameFilter = this.state.departmentNameFilter;

        var filteredData = this.state.employeesWithoutFilter.filter(
            function (el) {
                return el.firstName.toString().toLowerCase().includes(
                    firstNameFilter.toString().trim().toLowerCase())
                && el.lastName.toString().toLowerCase().includes(
                    lastNameFilter.toString().trim().toLowerCase())
                && el.fatherName.toString().toLowerCase().includes(
                    fatherNameFilter.toString().trim().toLowerCase())
                && el.position.toString().toLowerCase().includes(
                    positionFilter.toString().trim().toLowerCase())
                && el.salary.toString().toLowerCase().includes(
                    salaryFilter.toString().trim().toLowerCase())
                && el.departmentName.toString().toLowerCase().includes(
                    departmentNameFilter.toString().trim().toLowerCase());
            }
        );

        this.setState({ employees: filteredData });
    }

    sortResult(prop, asc) {
        var sortedData = this.state.employeesWithoutFilter.sort(function (a, b) {
            if (asc) {
                return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
            } else {
                return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
            }
        });

        this.setState({ employees: sortedData });
    }

    changeFirstNameFilter = (e) => {
        this.state.firstNameFilter = e.target.value;
        this.FilterFn();
    }

    changeLastNameFilter = (e) => {
        this.state.lastNameFilter = e.target.value;
        this.FilterFn();
    }

    changeFatherNameFilter = (e) => {
        this.state.fatherNameFilter = e.target.value;
        this.FilterFn();
    }

    changePositionFilter = (e) => {
        this.state.positionFilter = e.target.value;
        this.FilterFn();
    }

    changeSalaryFilter = (e) => {
        this.state.salaryFilter = e.target.value;
        this.FilterFn();
    }

    changeDepartmentNameFilter = (e) => {
        this.state.departmentNameFilter = e.target.value;
        this.FilterFn();
    }

    changeFirstName = (e) => {
        this.setState({ firstName: e.target.value });
    }

    changeLastName = (e) => {
        this.setState({ lirstName: e.target.value });
    }

    changeFatherName = (e) => {
        this.setState({ fatherName: e.target.value });
    }

    changePosition = (e) => {
        this.setState({ position: e.target.value });
    }

    changeSalary = (e) => {
        this.setState({ salary: e.target.value });
    }

    changeDepartment = (e) => {
        this.setState({ departmentName: e.target.value });
    }

    addClick() {
        this.setState({
            modalTitle: "Add Employee",
            id: 0,
            firstName: "",
            lastName: "",
            fatherName: "",
            position: "",
            salary: 0,
            departmentName: ""
        });
    }

    editClick(employee) {
        this.setState({
            modalTitle: "Edit Employee",
            id: employee.id,
            firstName: employee.firstName,
            lastName: employee.lastName,
            fatherName: employee.fatherName,
            position: employee.position,
            salary: employee.salary,
            departmentName: employee.departmentName
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
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                fatherName: this.state.fatherName,
                position: this.state.position,
                salary: this.state.salary,
                departmentName: this.state.departmentName
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
                id: this.state.id,
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                fatherName: this.state.fatherName,
                position: this.state.position,
                salary: this.state.salary,
                departmentName: this.state.departmentName
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
                    id: this.state.id,
                    firstName: this.state.firstName,
                    lastName: this.state.lastName,
                    fatherName: this.state.fatherName,
                    position: this.state.position,
                    salary: this.state.salary,
                    departmentName: this.state.departmentName
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
            id,
            firstName,
            lastName,
            fatherName,
            position,
            salary,
            departmentName
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
                                <input className="form-control m-2"
                                    onChange={this.changeFirstNameFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("firstName", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("firstName", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                FirstName
                            </th>
                            <th>
                                <input className="form-control m-2"
                                    onChange={this.changeLastNameFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("lastName", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("lastName", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                LastName
                            </th>
                            <th>
                                <input className="form-control m-2"
                                    onChange={this.changeFatherNameFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("fatherName", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("fatherName", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                FatherName
                            </th>
                            <th>
                                <input className="form-control m-2"
                                    onChange={this.changePositionFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("position", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("position", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                Position
                            </th>
                            <th>
                                <input className="form-control m-2"
                                    onChange={this.changeSalaryFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("salary", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("salary", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                Salary
                            </th>
                            <th>
                                <input className="form-control m-2"
                                    onChange={this.changeDepartmentNameFilter}
                                    placeholder="Filter" />

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("departmentName", true)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-down-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                                    </svg>
                                </button>

                                <button type="button" className="btn btn-light"
                                    onClick={() => this.sortResult("departmentName", false)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-arrow-up-square-fill" viewBox="0 0 16 16">
                                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                                    </svg>
                                </button>
                                <br></br>
                                Department
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {employees.map(e =>
                            <tr key={e.id}>
                                <td>
                                    {e.firstName}
                                </td>
                                <td>
                                    {e.lastName}
                                </td>
                                <td>
                                    {e.fatherName}
                                </td>
                                <td>
                                    {e.position}
                                </td>
                                <td>
                                    {e.salary}
                                </td>
                                <td>
                                    {e.departmentName}
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
                                            <input type="text" className="form-control" value={firstName}
                                                onChange={this.changeFirstName} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">LastName</span>
                                            <input type="text" className="form-control" value={lastName}
                                                onChange={this.changeLastName} />
                                        </div>
                                        
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">FatherName</span>
                                            <input type="text" className="form-control" value={fatherName}
                                                onChange={this.changeFatherName} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Position</span>
                                            <input type="text" className="form-control" value={position}
                                                onChange={this.changePosition} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Salary</span>
                                            <input type="text" className="form-control" value={salary}
                                                onChange={this.changeSalary} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Department</span>
                                            <select className="form-select" value={departmentName}
                                                onChange={this.changeDepartment}>
                                                {departments.map(department => <option key={department.id}>
                                                    {department.name}
                                                </option>)}
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                {id == 0
                                    ? <button type="button" className="btn btn-primary float-start"
                                        onClick={() => this.createClick()}>Create</button>
                                    : null
                                }
                                {id != 0
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
