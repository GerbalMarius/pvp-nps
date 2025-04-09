document.addEventListener("DOMContentLoaded", () => {
    addRatingQuestion();
});
function addQuestion(existingQuestions) {
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
                    <select name="SurveyForm.Questions[${index}].ExistingQuestionId" class="form-select">
                        <option value="">-- Select --</option>
                        ${existingQuestions.map(q => `<option value="${q.id}">${q.text}</option>`).join("")}
                    </select>
                </div>

                <div class="new-question-section">
                    <div class="mb-2">
                        <label>Question Text</label>
                        <input name="SurveyForm.Questions[${index}].QuestionText" class="form-control" />
                    </div>

                    <div class="mb-2">
                        <label>Question Type</label>
                        <select name="SurveyForm.Questions[${index}].Type" class="form-select" onchange="toggleTypeFields(this)">
                            <option value="">-- Select --</option>
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
                        <label>Answer Choices (comma-separated)</label>
                        <input name="SurveyForm.Questions[${index}].Choices" class="form-control" />
                    </div>
                </div>

                <button type="button" class="btn btn-sm btn-outline-danger mt-2" onclick="removeQuestion(this)">Remove</button>
            `;
    container.appendChild(div);
}

function removeQuestion(button) {
    const block = button.closest(".survey-question-block");
    block.remove();
}

function toggleExisting(checkbox) {
    const block = checkbox.closest(".survey-question-block");
    const existingSection = block.querySelector(".existing-question-section");
    const newSection = block.querySelector(".new-question-section");

    const index = block.getAttribute("data-index");

   
    block.querySelectorAll(`input[name="SurveyForm.Questions[${index}].UseExisting"]`).forEach(el => el.remove());

    if (checkbox.checked) {
        
        const hidden = document.createElement("input");
        hidden.type = "hidden";
        hidden.name = `SurveyForm.Questions[${index}].UseExisting`;
        hidden.value = "true";
        block.appendChild(hidden);

        existingSection.classList.remove("d-none");
        newSection.classList.add("d-none");
    } else {
        existingSection.classList.add("d-none");
        newSection.classList.remove("d-none");
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
        <input type="hidden" name="SurveyForm.Questions[0].UseExisting" value="true" />
        <input type="hidden" name="SurveyForm.Questions[0].ExistingQuestionId" value="1" />


        <div class="existing-question-section mb-2">
            <label>Rating Question (Required)</label>
            <select class="form-select" disabled>
                <option selected>How would you rate our service?</option>
            </select>
        </div>
    `;

    container.appendChild(div);
}
