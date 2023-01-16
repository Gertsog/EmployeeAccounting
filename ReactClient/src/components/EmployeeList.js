"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var jsx_runtime_1 = require("react/jsx-runtime");
var EmployeeItem_1 = require("./EmployeeItem");
var Variables_1 = require("../Variables");
var handleDelete = function (employee) {
    if (window.confirm('Are you sure?')) {
        //fetch(variables.API_URL + 'employee/' + id, {
        fetch(Variables_1.variables.API_URL + 'employee', {
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
            .then(function (response) { return response.json; })
            .then(function (result) {
            alert(result);
        }, function (error) {
            alert('Failed');
        });
    }
};
var EmployeeList = function (_a) {
    var employees = _a.employees;
    return ((0, jsx_runtime_1.jsxs)("table", __assign({ className: "table table-striped" }, { children: [(0, jsx_runtime_1.jsx)("thead", { children: (0, jsx_runtime_1.jsxs)("tr", { children: [(0, jsx_runtime_1.jsx)("th", { children: "EmployeeId" }), (0, jsx_runtime_1.jsx)("th", { children: "FirstName" }), (0, jsx_runtime_1.jsx)("th", { children: "LastName" }), (0, jsx_runtime_1.jsx)("th", { children: "FatherName" }), (0, jsx_runtime_1.jsx)("th", { children: "Position" }), (0, jsx_runtime_1.jsx)("th", { children: "Salary" }), (0, jsx_runtime_1.jsx)("th", { children: "Department" }), (0, jsx_runtime_1.jsx)("th", {})] }) }), (0, jsx_runtime_1.jsx)("tbody", { children: employees.map(function (employee) {
                    return (0, jsx_runtime_1.jsx)(EmployeeItem_1.default, { employee: employee, onDelete: handleDelete }, employee.id);
                }) })] })));
};
exports.default = EmployeeList;
//# sourceMappingURL=EmployeeList.js.map