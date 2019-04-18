"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var Employee = (function () {
    function Employee(firstName, title, lastName) {
        this.title = title;
        this._ageYrs = 0;
        this.name = this.title + ". " + firstName + " " + lastName;
    }
    Object.defineProperty(Employee.prototype, "age", {
        get: function () {
            return this._ageYrs;
        },
        set: function (yrs) {
            this._ageYrs = yrs;
        },
        enumerable: true,
        configurable: true
    });
    Employee.prototype.greet = function (guestName) {
        console.log("Hello " + guestName);
    };
    Employee.prototype.getInstance = function () {
        return this;
    };
    Employee.description = 'I\'m a person';
    return Employee;
}());
exports.Employee = Employee;
var SmartEmployee = (function (_super) {
    __extends(SmartEmployee, _super);
    function SmartEmployee(smartness, firstName, title) {
        var _this = _super.call(this, firstName, title) || this;
        _this.smartness = smartness;
        return _this;
    }
    SmartEmployee.prototype.getName = function () {
        this.greet('guest');
        var instance = this.getInstance();
        return "smart " + this.name;
    };
    SmartEmployee.prototype.getInstance = function () {
        console.log(_super.prototype.getInstance.call(this));
        return this;
    };
    return SmartEmployee;
}(Employee));
exports.SmartEmployee = SmartEmployee;
var EmployeeBase = (function () {
    function EmployeeBase() {
        this._employeeId = 100;
    }
    return EmployeeBase;
}());
var CoolEmployee = (function (_super) {
    __extends(CoolEmployee, _super);
    function CoolEmployee() {
        var _this = _super.call(this) || this;
        _this.hairStyle = 'funky';
        return _this;
    }
    CoolEmployee.prototype.ShowSwag = function () {
        console.log('See ma I\'m soo cool');
    };
    return CoolEmployee;
}(EmployeeBase));
exports.CoolEmployee = CoolEmployee;
var Animal = (function () {
    function class_1(name) {
        this.name = name;
    }
    return class_1;
}());
var cat = new Animal('CAT.');
//# sourceMappingURL=classes.js.map