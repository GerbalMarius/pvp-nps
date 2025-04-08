document.addEventListener('DOMContentLoaded', () => {
    const url = new URL(window.location.href);
    const params = new URLSearchParams(url.search);
    
    const errorType = params.get('errorType');
    
    const wrapper = document.getElementById('wrapper');
    const loginTab = document.getElementById('loginTab');
    const registerTab = document.getElementById('registerTab');
    const sliderIndicator = document.getElementById('sliderIndicator');
    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    const heading = document.getElementById('formHeading');
    const sub = document.getElementById('formSub');

    
    if (errorType === "Register") {
        populateRegister();
    } else {
        populateLogin();
    }

    loginTab.addEventListener('click', () => {
       populateLogin();
    });

    registerTab.addEventListener('click', () => {
       populateRegister();
    });

    function populateLogin() {
        sliderIndicator.style.left = "0%";
        loginTab.classList.add("active");
        registerTab.classList.remove("active");

        loginForm.classList.add("active-form");
        registerForm.classList.remove("active-form");

        heading.textContent = "Welcome Back";
        sub.textContent = "Log in to access your dashboard";

        wrapper.style.minHeight = "20rem";
    }
    
    function populateRegister() {
        sliderIndicator.style.left = "50%";
        registerTab.classList.add("active");
        loginTab.classList.remove("active");

        registerForm.classList.add("active-form");
        loginForm.classList.remove("active-form");

        heading.textContent = "Create Account";
        sub.textContent = "Join us and get started today";

        wrapper.style.minHeight = "28rem";
    }
});

