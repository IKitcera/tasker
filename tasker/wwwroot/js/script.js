function FocusOnAddTask() {
    document.getElementById('higgenOp').hidden = false;
}

function SelectedCategory() {
    return document.getElementById('selectCategory').select.value;
}
function OkAddItem() {
    document.getElementById('higgenOp').hidden = true;
}

function editCategoryClick(id) {
    var s = "showCategory" + id.toString();
    var e = "editCategory" + id.toString();
    document.getElementById(s).hidden = true;
    document.getElementById(e).hidden = false;
}

function editTaskClick(id) {
    var s = "showTask" + id.toString();
    var e = "editTask" + id.toString();
    document.getElementById(s).hidden = true;
    document.getElementById(e).hidden = false;
}

function finishEditCategory(id) {
    var s = "showCategory" + id.toString();
    var e = "editCategory" + id.toString();
    document.getElementById(e).hidden = true;
    document.getElementById(s).hidden = false;
}

function finishEditTask(id) {
    var s = "showTask" + id.toString();
    var e = "editTask" + id.toString();
    document.getElementById(e).hidden = true;
    document.getElementById(s).hidden = false;
}