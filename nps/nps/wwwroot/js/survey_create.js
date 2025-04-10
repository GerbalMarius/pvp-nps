let allExistingQuestions = [];

document.addEventListener("DOMContentLoaded", () => {
    allExistingQuestions = JSON.parse(atob(existingQuestions)); // from Razor-encoded data
    addRatingQuestion();
});

document.addEventListener("change", (e) => {
    if (e.target.classList.contains("existing-question-select")) {
        refreshExistingQuestionOptions();
    }
});

function toggleSurveyMode() {
    const useExisting = document.getElementById("useExistingSurveyToggle").checked;
    document.getElementById("existing-survey-section").classList.toggle("d-none", !useExisting);
    document.getElementById("new-survey-section").classList.toggle("d-none", useExisting);
}

function addQuestion() {
    const container = document.getElementById("questions-container");
    const index = container.querySelectorAll(".survey-question-block").length;

    const div = document.createElement("div");
    div.classList.add("survey-question-block");
    div.setAttribute("data-index", index);

    div.innerHTML = `
            <div class="form-check form-switch mb-2">
                <input class="form-check-input" type="checkbox" onchange="toggleExisting(this)" />
                <label class="form-check-label">Use Existing Question</label>
            </div>

            <div class="existing-question-section d-none mb-2">
                <label>Select Existing Question</label>
                <select name="SurveyForm.Questions[${index}].ExistingQuestionId"
                        class="form-select existing-question-select">
                </select>
            </div>

            <div class="new-question-section">
                <div class="mb-2">
                    <label>Question Text</label>
                    <input name="SurveyForm.Questions[${index}].QuestionText" class="form-control" />
                </div>

                <div class="mb-2">
                    <label>Question Type</label>
                    <select name="SurveyForm.Questions[${index}].Type"
                            class="form-select" onchange="toggleTypeFields(this)">
                        <option value="" disabled selected>-- Select --</option>
                        <option value="SingleChoice">Single Choice</option>
                        <option value="DropDown">Dropdown</option>
                        <option value="CheckBox">Checkbox</option>
                        <option value="Text">Text</option>
                    </select>
                </div>

                <div class="mb-2 type-field type-singlechoice d-none">
                    <label>Max Options</label>
                    <input type="number" name="SurveyForm.Questions[${index}].MaxOptions" class="form-control" />
                </div>

                <div class="mb-2 type-field type-singlechoice type-dropdown type-checkbox d-none">
                    <label>Answer Choices (separated by , ; . or space)</label>
                    <input name="SurveyForm.Questions[${index}].Choices" class="form-control" value="" />
                </div>
            </div>

            <button type="button" class="btn-red" onclick="removeQuestion(this)">Remove</button>
        `;

    container.appendChild(div);
    refreshExistingQuestionOptions();
}

function refreshExistingQuestionOptions() {
    document.querySelectorAll('.existing-question-select').forEach(select => {
        const currentValue = select.value;

        // Get all selected IDs excluding the one in this select
        const selectedIds = Array.from(document.querySelectorAll('.existing-question-select'))
            .filter(s => s !== select)
            .map(s => s.value)
            .filter(Boolean);

        select.innerHTML = ['<option value="" disabled>-- Select --</option>']
            .concat(
                allExistingQuestions.map(q => {
                    const isDisabled = selectedIds.includes(String(q.id));
                    const isSelected = String(q.id) === currentValue;
                    return `<option value="${q.id}" ${isSelected ? 'selected' : ''} ${isDisabled ? 'disabled' : ''}>${q.text}</option>`;
                })
            ).join('');
    });
}

function removeQuestion(button) {
    const block = button.closest(".survey-question-block");
    block.remove();
    refreshExistingQuestionOptions();
}

function toggleExisting(checkbox) {
    const block = checkbox.closest(".survey-question-block");
    const existingSection = block.querySelector(".existing-question-section");
    const newSection = block.querySelector(".new-question-section");
    const index = block.getAttribute("data-index");

    // Remove old hidden input if any
    block.querySelectorAll(`input[name="SurveyForm.Questions[${index}].UseExisting"]`).forEach(el => el.remove());

    if (checkbox.checked) {
        const hidden = document.createElement("input");
        hidden.type = "hidden";
        hidden.name = `SurveyForm.Questions[${index}].UseExisting`;
        hidden.value = "true";
        block.appendChild(hidden);

        existingSection.classList.remove("d-none");
        newSection.classList.add("d-none");

        setTimeout(refreshExistingQuestionOptions, 0);
    } else {
        existingSection.classList.add("d-none");
        newSection.classList.remove("d-none");

        refreshExistingQuestionOptions();
    }
}

function toggleTypeFields(select) {
    const block = select.closest(".survey-question-block");
    const type = select.value.toLowerCase();

    block.querySelectorAll(".type-field").forEach(el => el.classList.add("d-none"));

    if (type === "singlechoice") {
        block.querySelectorAll(".type-singlechoice").forEach(el => el.classList.remove("d-none"));
    } else if (type === "dropdown") {
        block.querySelectorAll(".type-dropdown").forEach(el => el.classList.remove("d-none"));
    } else if (type === "checkbox") {
        block.querySelectorAll(".type-checkbox").forEach(el => el.classList.remove("d-none"));
    }
}

function addRatingQuestion() {
    const container = document.getElementById("questions-container");

    const div = document.createElement("div");
    div.classList.add("survey-question-block", "border", "p-3", "mb-3");

    div.innerHTML = `
            <hidden><input type="hidden" name="SurveyForm.Questions[0].UseExisting" value="true" /></hidden>
            <hidden><input type="hidden" name="SurveyForm.Questions[0].ExistingQuestionId" value="1" /></hidden>

            <div class="existing-question-section mb-2">
                <label>Rating Question (Required)</label>
                <select class="form-select" disabled>
                    <option selected>How would you rate our service?</option>
                </select>
            </div>
        `;

    container.appendChild(div);
}