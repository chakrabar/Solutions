# Web Component

A native `HTML5` way of building reusable HTML elements that operates in full native capacity with own template, styling and scripts.

## Main HTML APIs for Web Component

1. HTML imports - a single line code to import HTML, style & script
2. Custom elements - build new HTML elements (name must have a -dash) ??
3. HTML templates - fragments HTML DOM that are rendered on-demand with JS
4. Shadow DOM - part of DOM that is hidden within the element, and that is not affected or has no affect outside of the element. Other styles and scripts has no effect on this

This is how an `HTML import` looks like in use

```html
<!-- importing custom element -->
<link rel="import" href="slider-nav.html" id="slider-nav" />

<!-- using the element in HTML-->
<slider-nav id="slider" title="rating"></slider-nav>

<!-- the template : slider-nav.html -->
<script src="slidernav.js" type="text/javascript"></script>
<template> <!-- these exists inside a shadow dom -->
    <style>div{color:yellow}</style>
    <div data-role="container">...</div>
</template>

<!-- this actually renders with a shadow DOM -->
<slider-nav id="slider" title="rating">
    #Shadow DOM - no effect of outside style/script
    <style>div{color:yellow}</style>
    <div data-role="container">...</div>
</slider-nav>
```

One drawback with web component is, it's slightly complex to built even a simple one by hand. The **[Polymer](https://www.polymer-project.org/)** JS library by Google makes it much easier to make web components. It gives nice syntactic sugar around native web component, and provides:

* Data binding
* Computer properties
* Templates
* Gesture events
* Simplified components