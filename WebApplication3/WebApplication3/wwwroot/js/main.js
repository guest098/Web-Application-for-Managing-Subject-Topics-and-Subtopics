const API_BASE_URL = "https://localhost:7148/api";

$(document).ready(function () {
    loadSubjects();
});

function loadSubjects() {
    $.get(`${API_BASE_URL}/subjects?t=${new Date().getTime()}`, function (data) {
        let subjects = data.$values || [];
        let tableContent = "";
        subjects.forEach((subject, index) => {
            tableContent += `
                <tr>
                    <td>${index + 1}</td>
                    <td>${subject.name}</td>
                    <td>${subject.description}</td>
                    <td>
                        <button class="btn btn-info btn-sm" onclick="showSubtopics(${subject.id}, '${subject.name}')">Show Subtopics</button>
                        <button class="btn btn-warning btn-sm" onclick="editSubject(${subject.id}, '${subject.name}', '${subject.description}')">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteSubject(${subject.id})">Delete</button>
                    </td>
                </tr>`;
        });
        $("#subjectTableBody").html(tableContent);
    }).fail(function (xhr) {
        console.error("AJAX Error:", xhr.responseText);
    });
}

function showSubtopics(subjectId, subjectName) {
    $("#selectedTopic").text(subjectName).attr("data-id", subjectId);
    $("#subtopicsSection").show();

    $.get(`${API_BASE_URL}/subtopics/subject/${subjectId}?t=${new Date().getTime()}`, function (subtopicsData) {
        let subtopics = subtopicsData.$values || [];
        let tableContent = subtopics.length === 0 ? `<tr><td colspan="4" class="text-center">No subtopics available</td></tr>` : "";
        subtopics.forEach((sub, index) => {
            tableContent += `
                <tr>
                    <td>${index + 1}</td>
                    <td>${sub.name}</td>
                    <td>${sub.description}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="editSubtopic(${sub.id}, '${sub.name}', '${sub.description}')">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteSubtopic(${sub.id})">Delete</button>
                    </td>
                </tr>`;
        });
        $("#subtopicTableBody").html(tableContent);
    }).fail(function (xhr) {
        console.error("Error loading subtopics:", xhr.responseText);
    });
}

function saveSubject() {
    let id = $("#subjectId").val();
    let name = $("#subjectName").val();
    let description = $("#subjectDescription").val();
    if (!name.trim()) return alert("Please enter a subject name.");

    let subjectData = { id: id || 0, name: name, description: description };
    let url = id ? `${API_BASE_URL}/subjects/${id}` : `${API_BASE_URL}/subjects`;
    let method = id ? "PUT" : "POST";

    $.ajax({
        url: url,
        type: method,
        contentType: "application/json",
        data: JSON.stringify(subjectData),
        success: function (response) {
            hideSubjectForm();
            loadSubjects();
            setTimeout(() => { showSubtopics(response.id, response.name); }, 300);
        },
        error: function (xhr) {
            console.error("Error saving subject:", xhr.responseText);
        }
    });
}

function editSubject(id, name, description) {
    $("#subjectId").val(id);
    $("#subjectName").val(name);
    $("#subjectDescription").val(description);
    $("#subjectForm").show();
}

function deleteSubject(id) {
    if (!confirm("Are you sure you want to delete this subject?")) return;
    $.ajax({
        url: `${API_BASE_URL}/subjects/${id}`,
        type: "DELETE"
    }).done(function () {
        loadSubjects();
        $("#subtopicsSection").hide();
    }).fail(function (xhr) {
        console.error("Error deleting subject:", xhr.responseText);
    });
}

function saveSubtopic() {
    let id = $("#subtopicId").val();
    let name = $("#subtopicName").val();
    let description = $("#subtopicDescription").val();
    let subjectId = $("#selectedTopic").attr("data-id");
    if (!name.trim() || !subjectId) return alert("Please enter a subtopic name and select a subject.");

    let subtopicData = { id: id || 0, name: name, description: description, subjectId: subjectId };
    let url = id ? `${API_BASE_URL}/subtopics/${id}` : `${API_BASE_URL}/subtopics`;
    let method = id ? "PUT" : "POST";

    $.ajax({
        url: url,
        type: method,
        contentType: "application/json",
        data: JSON.stringify(subtopicData),
        success: function () {
            hideSubtopicForm();
            showSubtopics(subjectId, $("#selectedTopic").text());
        },
        error: function (xhr) {
            console.error("Error saving subtopic:", xhr.responseText);
        }
    });
}

function editSubtopic(id, name, description) {
    $("#subtopicId").val(id);
    $("#subtopicName").val(name);
    $("#subtopicDescription").val(description);
    $("#subtopicForm").show();
}

function deleteSubtopic(id) {
    if (!confirm("Are you sure you want to delete this subtopic?")) return;
    $.ajax({
        url: `${API_BASE_URL}/subtopics/${id}`,
        type: "DELETE"
    }).done(function () {
        let subjectId = $("#selectedTopic").attr("data-id");
        showSubtopics(subjectId, $("#selectedTopic").text());
    }).fail(function (xhr) {
        console.error("Error deleting subtopic:", xhr.responseText);
    });
}

function showSubjectForm() {
    $("#subjectId, #subjectName, #subjectDescription").val("");
    $("#subjectForm").show();
}

function hideSubjectForm() {
    $("#subjectForm").hide();
}

function showSubtopicForm() {
    $("#subtopicId, #subtopicName, #subtopicDescription").val("");
    $("#subtopicForm").show();
}

function hideSubtopicForm() {
    $("#subtopicForm").hide();
}
