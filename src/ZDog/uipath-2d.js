let _isSpinning = true;
const _orange = '#FA4516';
const _blue = '#0085CA';
const _scale = 2;

// create illo
let illo = new Zdog.Illustration({
    element: '.zdog-canvas',
    dragRotate: true,
    scale: _scale,
    onDragStart: function () {
        _isSpinning = false;
    },
    onDragEnd: function () {
        _isSpinning = true;
    },
});

let box = new Zdog.Rect({
    addTo: illo,
    width: 160,
    height: 160,
    stroke: 40,
    color: _orange,
});

let u = new Zdog.Shape({
    addTo: box,
    path: [
        { x: -40, y: -40 },   // start
        { x: -40, y: 20 },
        {
            arc: [
                { x: -40, y: 40 }, // corner
                { x: -20, y: 40 }, // end point
            ]
        },
        {
            arc: [ // start next arc from last end point
                { x: 0, y: 40 }, // corner
                { x: 0, y: 20 }, // end point
            ]
        },
        { x: 0, y: 20 },   // start
        { x: 0, y: -40 },
    ],
    closed: false,
    stroke: 40, // 20
    color: _orange
});

let i_base = new Zdog.Shape({
    addTo: box,
    path: [
        { x: 40, y: -20 },
        { x: 40, y: 40 },
    ],
    closed: false,
    stroke: 40,
    color: _orange
});

let i_dot = new Zdog.Shape({
    addTo: box,
    translate: { x: 40, y: -45 },
    stroke: 40,
    color: _orange
});

// update & render
// illo.updateRenderGraph();

function animate() {
    // rotate
    if (_isSpinning) {
        // rotate illo each frame
        illo.rotate.y += 0.03;
        illo.rotate.x += 0.02;
        illo.rotate.z += 0.01;
    }

    illo.updateRenderGraph();
    // animate next frame
    requestAnimationFrame(animate);
}
// start animation
animate();