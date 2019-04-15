import { GetBookTitles } from './modules/functions';
var HelloWorld = (function () {
    function HelloWorld(message) {
        this.message = message;
    }
    return HelloWorld;
}());
var hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');
console.log('Books: ' + JSON.stringify(GetBookTitles(100)));
//# sourceMappingURL=app.js.map