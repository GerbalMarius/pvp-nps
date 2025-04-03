function selectNPS(value, name, questionId) {

    let questionContainer = document.getElementById(`question-${questionId}`);
    let buttons = questionContainer.querySelectorAll('.rating-btn');

    buttons.forEach(btn => btn.classList.remove('active'));

    
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

//used for expansion of navbar through clicking of burger menu.
function toggleNavBar(event, navBarId) {
    event.stopPropagation();
    document.getElementById(navBarId).classList.toggle('expanded');
}
//if clicked outside of menu, shrink it to show only icons.
function shrinkNavBar(event, navBarId) {
    let navBar = document.getElementById(navBarId);
    if (navBar.classList.contains('expanded')) {
        navBar.classList.remove('expanded');
    }
}
//opening of dropdown
function toggleDropDown() {
    let dropdown = document.querySelector('.dropdown');
    dropdown.classList.toggle('active');
}
//if clicked of close.
document.addEventListener('click', event => {
    let dropdown = document.querySelector('.dropdown');
    if (!dropdown.contains(event.target)) {
        dropdown.classList.remove('active');
    }
});

