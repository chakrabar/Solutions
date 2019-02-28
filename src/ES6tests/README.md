## Learning ES6

What's new
* New syntax
* ES6 Modules & Classes
* New Types & Object extensions
* Symbols
* Iterators, Generators & Promises
* Array & Collection classes
* The Reflect API
* The Proxy API

Popular ES6 browser-compatibility chart http://kangax.github.io/compat-table/es6/

`"use strict";` -> Introduced in ES5. Means JavaScript will be used in strict mode and cause error in incorrect scenarios. For example, an undeclared variable would cause error in scrict mode. This line can be used only at top of script or function.

#### New syntax

##### No variable hoisting

Using a variable before it is declared, would throw error. For this, ES6 `let` or `const` has to be used. Does not apply to `var`. Actually, `var` should not be used in ES6.

Also, non defined variable like `let something;` gets initialized to `undefined`

##### Block scoping

A variable can be redefined within a block, and that would disappear outside that block.

```javascript
'use strict';
let prodId = 5;
{
    let prodId = 100;
    let userId = 99;
}
console.log(prodId); // 5
console.log(userId); // Reference Error: userId is not defined

function updateCount() { // no error as it is not invoked yet
    count += 1;
}
let count = 7;
updateCount(); // works fine, as count is now defined
console.log(count); // 8
```

##### Closure behaviour

Traditional closure with `var` => closure over variable reference

```javascript
'use strict';
var updateFunctions = [];
for (var i = 0; i < 2; i++) {
    updateFunctions.push(function() { return i; });
}
console.log(updateFunctions[0]()); // 2 ..!! traditional closure

// ES6
let updateFunctions = [];
for (let i = 0; i < 2; i++) { // this LET is important
    updateFunctions.push(function() { return i; });
}
console.log(updateFunctions[0]()); // 0 closure over individual captured values
// this hurts performance a bit, but makes it the way behave most people would expect
```

##### `const`

Variables defined with `const` cannot be changed. In case of objects, it's property values can be updated. And it must be initialized at declaration.

```javascript
const var1; // error
const var2 = 5;
var2 = 10; // error

const PROD_ID = 5;
if (PROD_ID < 10)
{
    const PROD_ID = 100; // works fine because of block-scope
}
console.log(PROD_ID); // 5

const obj = { name: 'ac' };
obj.name = 'abc'; // fine
```

##### Arrow functions

Lets us define functions without use of `function` and `return` keywords, in the form of `(inputs) => result` format.

1. No arguments are represented as `() => 'Hello World'`
2. Single argument can skip the paranthesis `x => x * 2`
3. More arguments need paranthesis `(a, b) => a + b`
4. `return` is required when the body is multi-line block

```javascript
const increment = x => ++x; // when there is just one argument, paranthesis is optional
increment(1); // 2
typeof increment; // function

const calc = (a, b) => { // this FAT ARROW symbol CANNOT be on a new line
    const k = a * a;
    const l = b * b;
    return k + l;
}
```

Arrow functions reduce the confusion around `this` specially in event handlers. Now **inside a arrow function `this` is always the context of the running code**, i.e. `Window` object when run in a browser.

* You cannot `bind` a new object to an arrow function. Even if you try, ES6 will simply ignore that. Same with `call` or `apply`
* Arrow functions do not allow the fat arrow symbol to be on a new line
* Arrow functions do not allow access to it's `prototype` property

```javascript
document.addEventListener('click', () => console.log(this));
// on document click it logs
// Window { ... }
```

##### Default function parameters

```javascript
const myFunc = function(id = 100) { // default parameter
    console.log(id);
};
myFunc(); // works & produces 100

const add = function(a = 1, b = 2) { // default parameter
    console.log(a + b);
};
add(undefined, 5); // 6 i.e. 1 + 5

// a fun scope 
const calculate = function(first, second = 2 * first) {
    console.log(first + second);
    console.log(arguments.length);
};
calculate(1); // works and prints 3 & 1 i.e. the actual no. of arguments passed

// DYNAMIC function (was already there, still cool)
const getTotal = new Function("price = 20", "return price;");
getTotal(); // returns 20 :)
```

##### Rest & Spread

Rest can collect multiple things into an array. That parameter with three dots is called a **rest parameter**.

```javascript
const showCategories = function(productId, ...categories) { // ...rest
    console.log(categories instanceof Array);
    console.log(categories);
};
showCategories(1, 'book', 'movie'); // true -> categories = ['book', 'movie']
showCategories('yo'); // true -> categories = []
console.log(showCategories.length); // 1, ignores rest parameters
```

Spread is kind of the opposite of rest. It spreads out an array into a number of values.

```javascript
const prices = [1, 2, 3];
const max = Math.max(...prices); // spreads out prices a bunch of values
console.log(max); // 3

const arr = [...prices]; // workds too
console.log(arr); // [1, 2, 3]

// NOTE
const arr2 = [...[,,]];
console.log(arr2); // [undefined, undefined] -> since nothing is provided, undefined has been captured
// Also, unless something is given, JS assumes there's nothing after last comma (existing behaviour)

// Spread also spreads a string into individual characters
```

##### Object literal extension

Automatically names object properties from variables (similar to `Tuple` declaratio in C#)

```javascript
const name = "Arghya";
const age = 50;
const person = { name, age }; // valid and works
console.log(person); // {name: "Arghya", age: 50}

//function without function keyword
const person2 = { name, age, walk() { console.log(name + ' walking...'); } }; // valid and works
person2.walk(); // Arghya walking...

// Even more weird
const method = "meth";
const person3 = { 
    name, 
    age, 
    "shout"() { console.log(name + ' calling you') }, // works
    [method + '-' + age]() { console.log('method invoked') } // within square brackets event expressions can be used
};
console.log(person3['shout']()); // Arghya calling you
console.log(person3.meth-50()); // method invoked
```

Naming getter and setter properties. This gives accessors over fields. Introduced in ES5, this lets call them as properties than methods. We can also create read-only or set-only properties;

```javascript
// ES5 getter & setter properties (well, I was unaware these even exist)
//general syntax
var person5 = {
    nm: 'Cool guy',
    get name() { // this is not a method, it's a getter property
        return 'Mr. ' + this.nm;
    },
    set name(val) { // this is a setter property
        this.nm = val;
    }
};
person5.name = "Robo";
person5.name; // Mr. Robo


// ES6 dynamic naming getter and setter
const ident = 'prodId';
const prodView = {
    get [ident] () { return true; },
    set [ident] (value) { }
};
```