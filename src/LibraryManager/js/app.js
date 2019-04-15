var HelloWorld = (function () {
    function HelloWorld(message) {
        this.message = message;
    }
    return HelloWorld;
}());
var hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');
var myStr = 'hello people';
function num() { return 1; }
var welDefinedStr = 'this is explicitly typed';
function num2(k) { return k + 2; }
function getArr() {
    return [
        { name: 'AC', age: 30 },
        { name: 'PRC', age: 29 }
    ];
}
var checkLegalAge = function (age) { return age >= 18 ? 'Welcome' : 'Sorry, you are underage'; };
var ageCheckFunc = checkLegalAge;
console.log(ageCheckFunc(15));
var getWelcomeMessage = function (name, age, country) {
    if (age === void 0) { age = 18; }
    return "Hello " + name + ", " + age + " years, from " + (country ? country : 'your country');
};
getWelcomeMessage('AC', 30, 'India');
getWelcomeMessage('AC');
getWelcomeMessage('AC', 30);
getWelcomeMessage('AC', null, 'India');
var printFirstN = function (n) {
    var users = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        users[_i - 1] = arguments[_i];
    }
    if (n > 0 && users && users.length) {
        var count = 0;
        while (count < n && count < users.length) {
            console.log(users[count]);
            ++count;
        }
    }
};
printFirstN(2, 'Penny', 'Sheldon', 'Amy', 'Leonard');
function GetBookTitles(arg) {
    if (typeof arg == 'string') {
        return ['A', 'B'];
    }
    else if (typeof arg == 'number') {
        return ['C', 'D'];
    }
}
var books = GetBookTitles('cool author');
console.log(books);
var Category;
(function (Category) {
    Category[Category["Biography"] = 0] = "Biography";
    Category[Category["Poetry"] = 1] = "Poetry";
    Category[Category["Fiction"] = 2] = "Fiction";
})(Category || (Category = {}));
var TypeOfPeople;
(function (TypeOfPeople) {
    TypeOfPeople[TypeOfPeople["Stupid"] = 1] = "Stupid";
    TypeOfPeople[TypeOfPeople["Smart"] = 2] = "Smart";
    TypeOfPeople[TypeOfPeople["Nerd"] = 3] = "Nerd";
    TypeOfPeople[TypeOfPeople["Asswhole"] = 4] = "Asswhole";
})(TypeOfPeople || (TypeOfPeople = {}));
var TypeOfFish;
(function (TypeOfFish) {
    TypeOfFish[TypeOfFish["SweetWater"] = 2] = "SweetWater";
    TypeOfFish[TypeOfFish["SaltWater"] = 5] = "SaltWater";
    TypeOfFish[TypeOfFish["Acquarium"] = 9] = "Acquarium";
})(TypeOfFish || (TypeOfFish = {}));
var fishType = TypeOfFish.Acquarium;
console.log('fishType = ' + fishType + " = " + TypeOfFish[fishType]);
var arr1 = ['an', 'array'];
var arr2 = ['another', 'array'];
console.log(arr1.concat(arr2));
var myTuple = [1, "one"];
myTuple.push(3);
console.log(myTuple);
var one = myTuple[1];
console.log(one);
//# sourceMappingURL=app.js.map