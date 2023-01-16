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
require("./App.css");
var react_router_dom_1 = require("react-router-dom");
var EmployeesPage_1 = require("./components/EmployeesPage");
var DepartmentsPage_1 = require("./components/DepartmentsPage");
var App = function () {
    return ((0, jsx_runtime_1.jsx)("div", __assign({ className: "App container" }, { children: (0, jsx_runtime_1.jsx)(react_router_dom_1.BrowserRouter, { children: (0, jsx_runtime_1.jsxs)("div", { children: [(0, jsx_runtime_1.jsxs)("div", { children: [(0, jsx_runtime_1.jsx)(react_router_dom_1.NavLink, __assign({ to: "/employees", className: "btn btn-light btn-outline-primary" }, { children: "Employees" })), (0, jsx_runtime_1.jsx)(react_router_dom_1.NavLink, __assign({ to: "/departments", className: "btn btn-light btn-outline-primary" }, { children: "Departments" }))] }), (0, jsx_runtime_1.jsxs)(react_router_dom_1.Routes, { children: [(0, jsx_runtime_1.jsx)(react_router_dom_1.Route, { path: "/employees", element: (0, jsx_runtime_1.jsx)(EmployeesPage_1.default, {}) }), (0, jsx_runtime_1.jsx)(react_router_dom_1.Route, { path: "/departments", element: (0, jsx_runtime_1.jsx)(DepartmentsPage_1.default, {}) })] })] }) }) })));
};
exports.default = App;
//# sourceMappingURL=App.js.map