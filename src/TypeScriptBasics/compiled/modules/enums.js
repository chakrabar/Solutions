"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Category;
(function (Category) {
    Category[Category["Biography"] = 0] = "Biography";
    Category[Category["Poetry"] = 1] = "Poetry";
    Category[Category["Fiction"] = 2] = "Fiction";
})(Category || (Category = {}));
exports.Category = Category;
var TypeOfPeople;
(function (TypeOfPeople) {
    TypeOfPeople[TypeOfPeople["Stupid"] = 1] = "Stupid";
    TypeOfPeople[TypeOfPeople["Smart"] = 2] = "Smart";
    TypeOfPeople[TypeOfPeople["Nerd"] = 3] = "Nerd";
    TypeOfPeople[TypeOfPeople["Asswhole"] = 4] = "Asswhole";
})(TypeOfPeople || (TypeOfPeople = {}));
exports.TypeOfPeople = TypeOfPeople;
var TypeOfFish;
(function (TypeOfFish) {
    TypeOfFish[TypeOfFish["SweetWater"] = 2] = "SweetWater";
    TypeOfFish[TypeOfFish["SaltWater"] = 5] = "SaltWater";
    TypeOfFish[TypeOfFish["Acquarium"] = 9] = "Acquarium";
})(TypeOfFish || (TypeOfFish = {}));
exports.TypeOfFish = TypeOfFish;
var fishType = TypeOfFish.Acquarium;
console.log('fishType = ' + fishType + " = " + TypeOfFish[fishType]);
//# sourceMappingURL=enums.js.map