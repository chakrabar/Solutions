class Employee {
    // simple property
    name : string;

    // property as getter setter
    get age() : number {
        return this._ageYrs; // private members
    }
    set age(yrs : number) {
        this._ageYrs = yrs; // _ is for readability, not required as such
    }
    
    // constructor with parameter, property shortcut, optional parameter
    // class can have only one constructor
    constructor(firstName : string, public title : string, lastName? : string) {
        // public title => becomes a property of the class and gets initialized with passed value => works for any access modifier
        this.name = `${this.title}. ${firstName} ${lastName}`; // a property defined above
        // always use this. to refer to members inside class
    }

    // static property, attached to the class NOT an instance => cannot use instance members
    static description = 'I\'m a person'; // accessed as Employee.description

    // a member function
    greet(guestName : string) : void {
        console.log(`Hello ${guestName}`);
    }

    // === access modifiers ===
    // all members of class are PUBLIC BY DEFAULT. but they can also be made private/protected
    private _ageYrs : number = 0; // how is it handled in JS ??
    protected getInstance() : Employee {
        return this; // it just appears in JS, there is no protected!
    }
}

// === class extension through inheritance ===

class SmartEmployee extends Employee {
    constructor(public smartness : number, firstName : string, title : string) {
        super(firstName, title); // this call to super() IS MANDATORY
    }
    // IF no constructor is defined, it'll automatically use super class constructor

    // new member
    getName() : string {
        this.greet('guest'); // greet is now it's own method
        // const age = this._ageYrs; // this does not work => private in super
        // const age = super.age; // uh ho, super has only the methods
        const instance = this.getInstance(); // this works => protected
        return `smart ${this.name}`; // name is now it's own property
    }

    // overriding an protected // also works for public
    getInstance() : SmartEmployee {
        console.log(super.getInstance()); // protected members are available on super
        return this;
    }
}

// === abstrct classes ===
// can be inherited, but cannot be instantiated directly
// can have method definition or implementation

abstract class EmployeeBase {
    private _employeeId : number; // can ahve any type of members like a normal class
    constructor() {
        this._employeeId = 100; // bad example
    }

    // and can also have abstract members => no implementation => must be implemented in child class
    abstract ShowSwag() : void; // abstract methods
    abstract hairStyle : string; // abstract property
}

class CoolEmployee extends EmployeeBase {
    hairStyle: string; // must implement abstract property

    constructor() {
        super();
        this.hairStyle = 'funky';
    }

    ShowSwag(): void {
        console.log('See ma I\'m soo cool'); // must implement abstract method
    }    
}

// === class expressions ===
// above classes are all defined using class statements
// But, like ES6, classes can also be defined using class expression
const Animal = class {
    constructor(public name : string) {}
}

const cat  = new Animal('CAT.');
// ==> ==> ==> well, I'm not gonna use this

export { Employee, SmartEmployee, CoolEmployee };