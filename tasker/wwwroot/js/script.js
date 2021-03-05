function FocusOnAddTask() {
    document.getElementById('higgenOp').hidden = false;
}

function SelectedCategory() {
    return document.getElementById('selectCategory').select.value;
}
function OkAddItem() {
    document.getElementById('higgenOp').hidden = true;
}

function checkTask(id) {
    let task = document.getElementById(id.toString());

    console.log(task.classList);

    if (task.checked) {
        if (!task.classList.contains("lineth")) {
            task.classList.add("lineth");
        }
    }
    else {
        if (task.classList.contains("lineth")) {
            task.classList.remove("lineth");
        }
    }
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

function ShowSWBtnClick() {
    document.getElementById('showSWBtn').hidden = true;
    document.getElementById('timerBlock').hidden = false;
}
function HideSWBtnClick() {
    document.getElementById('timerBlock').hidden = true;
    document.getElementById('showSWBtn').hidden = false;
}
function editSWNameClick() {
    document.getElementById('swName').hidden = true;
    document.getElementById('swChangeBtn').hidden = true;
    document.getElementById('swNewName').hidden = false;
    document.getElementById('swChangeOkBtn').hidden = false;
}
function editSWNameOk() {
    document.getElementById('swNewName').hidden = true;
    document.getElementById('swChangeOkBtn').hidden = true;
    document.getElementById('swName').hidden = false;
    document.getElementById('swChangeOkBtn').hidden = false;

    return document.getElementById('swNameChange').value;
}
function startStopWatch() {
    document.getElementById('startSW').hidden = true;
    document.getElementById('resetSW').hidden = true;

    document.getElementById('stopSW').hidden = false;

    function tick() {
        val++;
        var label = document.getElementById("stopwatchMinutes");

        let h = Math.trunc(val / 3600);
        let m = Math.trunc(val / 60) - h * 60;
        let s = val - m * 60 - h * 3600;

        label.innerText = h.toString() + " : " + m.toString() + " : " + s.toString();
    }
    timer = setInterval(tick, 1000);
}
function stopStopWatch() {
    document.getElementById('stopSW').hidden = true;

    document.getElementById('startSW').hidden = false;
    document.getElementById('resetSW').hidden = false;

    clearInterval(timer);
    document.getElementById("minutes").value = Math.round(val / 60);
}
function resetStopWatch() {
    val = 0;
    var label = document.getElementById("stopwatchMinutes");
    label.innerText = val.toString();
    document.getElementById("minutes").value = val.toString();
}

val = 0;
var timer;
