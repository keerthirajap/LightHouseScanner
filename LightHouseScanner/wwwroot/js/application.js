window.localStorage = {
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    }
};

window.scrollDown = function () {

    console.log("scrollDown");

    smoothScrollBy(500, 3000); 

    //window.scrollBy({
    //    top: 500, // Adjust this value to control how much to scroll
    //    behavior: 'smooth'
    //});
};


function smoothScrollBy(distance, duration) {
    const start = window.scrollY;
    const startTime = performance.now();

    function step(timestamp) {
        const elapsed = timestamp - startTime;
        const progress = Math.min(elapsed / duration, 1); // Normalize progress to 1
        const easeInOut = (t) => t < 0.5
            ? 2 * t * t
            : -1 + (4 - 2 * t) * t; // Ease in-out function

        const scrollTop = start + distance * easeInOut(progress);
        window.scrollTo(0, scrollTop);

        if (progress < 1) {
            requestAnimationFrame(step);
        }
    }

    requestAnimationFrame(step);
}


window.scrollToElementById = function (elementId) {
    console.log("scrollToElementById", elementId);

    var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({
            behavior: 'smooth'
        });
    }
};