// rotating flag variable
let isSpinning = true;

// create illo
let illo = new Zdog.Illustration({
    // set canvas with selector
    element: '.zdog-canvas',
    // zoom up 4x
    // zoom: 4,
    // enable rotating scene with dragging
    dragRotate: true,
    // stop rotation when dragging starts
    onDragStart: function () {
        isSpinning = false;
    },
    onDragEnd: function() {
        isSpinning = true;
    },
});

// circle
let c = new Zdog.Ellipse({
    addTo: illo,
    diameter: 80,
    // position closer
    translate: { z: 40 },
    stroke: 20,
    color: '#636',
});

let c2 = new Zdog.Ellipse({
    addTo: c,
    diameter: 30,
    quarters: 2, // semi-circle
    translate: { z: 40 },
    stroke: 10,
    color: '#636',
    rotate: { x: -Zdog.TAU/8 }, // rotate 45° CCW
    backface: false,
});

// square
let r = new Zdog.Rect({
    addTo: illo,
    width: 80,
    height: 80,
    // position further back
    translate: { z: -40 },
    stroke: 20,
    color: '#E62',
    // fill: true,
});

// update & render
// illo.updateRenderGraph();

function animate() {
    // rotate
    if (isSpinning) {
        // rotate illo each frame
        illo.rotate.x += 0.03;
    }

    illo.updateRenderGraph();
    // animate next frame
    requestAnimationFrame(animate);
}
// start animation
animate();