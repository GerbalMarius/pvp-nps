function selectNPS(value, name, questionId) {

    let questionContainer = document.getElementById(`question-${questionId}`);
    let buttons = questionContainer.querySelectorAll('.rating-btn');

    buttons.forEach(btn => btn.classList.remove('active'));


    let selectedButton = questionContainer.querySelector(`.rating-btn[data-value="${value}"]`);
    if (selectedButton) {
        selectedButton.classList.add('active');
    }

    let inputField = document.getElementById(name);
    if (inputField) {
        inputField.value = value;
    }

}

function toggleNavBar(navBarId) {
    const event = window.event;
    if (event) event.stopPropagation();
    document.getElementById(navBarId).classList.toggle('expanded');
}

function shrinkNavBar(navBarId) {
    const navBar = document.getElementById(navBarId);
    if (navBar && navBar.classList.contains('expanded')) {
        navBar.classList.remove('expanded');
    }
}

function stopClickPropagation() {
    const event = window.event;
    if (event) event.stopPropagation();
}

function toggleDropDown() {
    const dropdown = document.querySelector('.dropdown');
    if (dropdown) {
        dropdown.classList.toggle('active');
    }
}

document.addEventListener('click', () => {
    const event = window.event;
    const dropdown = document.querySelector('.dropdown');
    if (dropdown && !dropdown.contains(event.target)) {
        dropdown.classList.remove('active');
    }
});
