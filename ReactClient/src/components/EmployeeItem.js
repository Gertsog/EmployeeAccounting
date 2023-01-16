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
var EmployeeItem = function (_a) {
    var employee = _a.employee, onDelete = _a.onDelete;
    return ((0, jsx_runtime_1.jsxs)("tr", { children: [(0, jsx_runtime_1.jsx)("td", { children: employee.id }), (0, jsx_runtime_1.jsx)("td", { children: employee.firstName }), (0, jsx_runtime_1.jsx)("td", { children: employee.lastName }), (0, jsx_runtime_1.jsx)("td", { children: employee.fatherName }), (0, jsx_runtime_1.jsx)("td", { children: employee.position }), (0, jsx_runtime_1.jsx)("td", { children: employee.salary }), (0, jsx_runtime_1.jsx)("td", { children: employee.departmentName }), (0, jsx_runtime_1.jsxs)("td", { children: [(0, jsx_runtime_1.jsx)("button", __assign({ type: "button", className: "btn btn-light mr-1", "data-bs-toggle": "modal", "data-bs-target": "#exampleModal" }, { children: (0, jsx_runtime_1.jsxs)("svg", __assign({ xmlns: "http://www.w3.org/2000/svg", width: "16", height: "16", fill: "currentColor", className: "bi bi-pencil-square", viewBox: "0 0 16 16" }, { children: [(0, jsx_runtime_1.jsx)("path", { d: "M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" }), (0, jsx_runtime_1.jsx)("path", { fillRule: "evenodd", d: "M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" })] })) })), (0, jsx_runtime_1.jsx)("button", __assign({ type: "button", className: "btn btn-light mr-1", "data-bs-toggle": "modal", "data-bs-target": "#exampleModal", onClick: function () { return onDelete(employee); } }, { children: (0, jsx_runtime_1.jsx)("svg", __assign({ xmlns: "http://www.w3.org/2000/svg", width: "16", height: "16", fill: "currentColor", className: "bi bi-trash-fill", viewBox: "0 0 16 16" }, { children: (0, jsx_runtime_1.jsx)("path", { d: "M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" }) })) }))] })] }, employee.id));
};
exports.default = EmployeeItem;
//# sourceMappingURL=EmployeeItem.js.map