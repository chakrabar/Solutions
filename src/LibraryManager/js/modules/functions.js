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
export { GetBookTitles };
//# sourceMappingURL=functions.js.map