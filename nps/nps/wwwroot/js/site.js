function selectNPS(value, name, questionId) {
    // Get all buttons in the same question container
    let questionContainer = document.getElementById(`question-${questionId}`);
    let buttons = questionContainer.querySelectorAll('.rating-btn');

    // Remove active class from all buttons in this question only
    buttons.forEach(btn => btn.classList.remove('active'));

    // Add active class to the selected button
    let selectedButton = questionContainer.querySelector(`.rating-btn[data-value="${value}"]`);
    if (selectedButton) {
        selectedButton.classList.add('active');
    }

    // Update hidden input field
    let inputField = document.getElementById(name);
    if (inputField) {
        inputField.value = value;
    }
}