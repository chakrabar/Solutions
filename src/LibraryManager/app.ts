class HelloWorld {
    constructor(public message: string) {}
}

const hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');

// === variables ===

let myStr = 'hello people';
// myStr = 34; // error

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

// === enum ===

enum Category { Biography, Poetry, Fiction } // 0, 1, 2
enum TypeOfPeople { Stupid = 1, Smart, Nerd, Asswhole } // 1, 2, 3, 4
enum TypeOfFish { SweetWater = 2, SaltWater = 5, Acquarium = 9 } // 1, 2, 3, 4

const fishType : TypeOfFish = TypeOfFish.Acquarium;
console.log('fishType = ' + fishType + " = " + TypeOfFish[fishType]); // string value by index

// === array ===

const arr1 : string[] = ['an', 'array'];
const arr2 : Array<string> = ['another', 'array'];

console.log([...arr1, ...arr2]);

// === tuples ===
// with first few types defined and members have to be the same type in same order
// rest can be either of the declared types! and can be added with push(). 
// members are accessed with index. Array methods can also be used.

let myTuple: [number, string] = [1, "one"];

myTuple.push(3);

console.log(myTuple);

const one = myTuple[1];

console.log(one);


