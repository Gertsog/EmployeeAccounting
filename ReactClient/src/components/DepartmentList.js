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
var DepartmentItem_1 = require("./DepartmentItem");
var DepartmentList = function (_a) {
    var departments = _a.departments;
    return ((0, jsx_runtime_1.jsxs)("table", __assign({ className: "table table-striped" }, { children: [(0, jsx_runtime_1.jsx)("thead", { children: (0, jsx_runtime_1.jsxs)("tr", { children: [(0, jsx_runtime_1.jsx)("th", { children: "DepartmentId" }), (0, jsx_runtime_1.jsx)("th", { children: "DepartmentName" }), (0, jsx_runtime_1.jsx)("th", {})] }) }), (0, jsx_runtime_1.jsx)("tbody", { children: departments.map(function (department) {
                    return (0, jsx_runtime_1.jsx)(DepartmentItem_1.default, { department: department }, department.id);
                }) })] })));
};
exports.default = DepartmentList;
//# sourceMappingURL=DepartmentList.js.map