"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var classes_1 = require("./classes");
var oldie;
var goldie;
goldie = new Array(5);
function GenericFunction(thing) {
    console.log(thing);
    console.log(typeof thing);
    return thing;
}
exports.GenericFunction = GenericFunction;
var emp = new classes_1.Employee('Arghya', 'Mr', 'Chakrabarty');
var myEmp = GenericFunction(emp);
var hrRecords;
var Inventory = (function () {
    function Inventory() {
        var _this = this;
        this._items = new Array();
        this.addItem = function (ietm) {
            _this._items.push(ietm);
        };
        this.getCount = function () {
            return _this._items.length;
        };
        this.getItems = function () {
            return _this._items;
        };
    }
    return Inventory;
}());
var TestGenericInterface = function () {
    var myEMp = new classes_1.Employee('Arghya', 'Mr', 'Chakrabarty');
    var emp22 = GenericFunction(myEMp);
    var emplpoyees;
    emplpoyees = new Inventory();
    emplpoyees.addItem(myEMp);
    emplpoyees.addItem(emp22);
    console.log(emplpoyees.getCount());
    console.log(emplpoyees.getItems());
};
exports.TestGenericInterface = TestGenericInterface;
//# sourceMappingURL=generics.js.map