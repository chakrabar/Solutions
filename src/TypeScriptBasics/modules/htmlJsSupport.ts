// TypeScript by default includes a lib.d.ts & lib.dom.d.ts files that enables it to use HTML & plain JS stuffs

window.onload = function() {
    var calc = new Calculator('X', 'Y', 'Output');
}

class Calculator {
    private x : HTMLInputElement;
    private y : HTMLInputElement;
    private output : HTMLSpanElement;

    constructor(xId : string, yId : string, outputId : string) {
        this.x = document.querySelector(`#${xId}`);
        this.y = <HTMLInputElement>document.getElementById(yId); // TypeScript casting
        this.output = <HTMLSpanElement>document.getElementById(outputId); // TypeScript casting
        this.wireEvents();
    }

    wireEvents = () => {
        document.getElementById('Add')
            .addEventListener('click', event => {
                this.output.innerHTML = `${this.add(parseInt(this.x.value), parseInt(this.y.value))}`;
            })
    }

    add : (x : number, y : number) => number = (x : number, y : number) => x + y;
}