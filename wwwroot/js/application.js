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

    window.scrollBy({
        top: 500, // Adjust this value to control how much to scroll
        behavior: 'smooth'
    });
};

window.scrollToElementById = function (elementId) {
    console.log("scrollToElementById", elementId);

    var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({
            behavior: 'smooth'
        });
    }
};