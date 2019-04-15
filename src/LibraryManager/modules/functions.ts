// === functions ===

function num() { return 1; }

// myStr = num(); // error

let welDefinedStr : string = 'this is explicitly typed'

function num2(k: number) : number { return k + 2; }

function getArr() {
    return [
        {name: 'AC', age: 30},
        {name: 'PRC', age: 29}
    ]
}

// function types

const checkLegalAge = (age : number) : string => age >= 18 ? 'Welcome' : 'Sorry, you are underage';

const ageCheckFunc : (userAge : number) => string = checkLegalAge;

console.log(ageCheckFunc(15)); // Sorry, you are underage

// parameters (in TypeScript, all parameters are required by default)
// required, default (with default value, which is also treated as optional), and optional
// NOTE: a required parameter cannot follow an optional parameter
const getWelcomeMessage = (name : string, age : number = 18, country? : string) =>
    `Hello ${name}, ${age} years, from ${country ? country : 'your country'}`;

// all valid calls to the function
getWelcomeMessage('AC', 30, 'India');
getWelcomeMessage('AC');
getWelcomeMessage('AC', 30);
getWelcomeMessage('AC', null, 'India');
// getWelcomeMessage(); not allowed, name is required parameter

// REST PARAMETER : any number of parameters that'll be captured in an array
// can appear only after required parameters (if any)
const printFirstN = (n : number, ...users : string[]) : void => {
    if (n > 0 && users && users.length) {
        let count = 0;
        while (count < n && count < users.length) {
            console.log(users[count]);
            ++count;
        }
    }
}

printFirstN(2, 'Penny', 'Sheldon', 'Amy', 'Leonard'); // Penny Sheldon

// == function overloads ==
// You basically define same function multiple types with different parameters
// Then you immediately implement a single function, and check parameter type in the body!

function GetBookTitles(author : string) : string[];
function GetBookTitles(maxPrice : number) : string[];
function GetBookTitles(arg : any) : string[] {
    if (typeof arg == 'string') {
        return ['A', 'B'];
    } else if (typeof arg == 'number') {
        return ['C', 'D'];
    }
}

const books = GetBookTitles('cool author'); // A, B
console.log(books);

export { GetBookTitles }; // only one export for the overloads