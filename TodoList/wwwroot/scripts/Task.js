import TaskService from "./TaskService.js";

var taskService = new TaskService();




// create task

$("#create-task").click(function () {
    var taskHTML = taskService.createTask();
    $(".tasks > .tasks__text").remove();
    $(".tasks__inner").append(taskHTML.responseText);
});




// remove task

$("body").on("click", ".tasks__button--remove", function () {
    var $button = $(this);
    var id = $button.attr("data-id");
    var result = taskService.removeTask(id);

    if (result.responseJSON == true) {
        var $task = $button.parent();
        $task.remove();
    } else {
        console.log(result);
    }
});


// update task

var tmp_text = '';
$("body").on("focus", ".task__input", function () {
    tmp_text = $(this).text();
});

$("body").on("blur", ".task__input", function () {
    if ($(this).text() == "")
        $(this).focus();

    if (tmp_text == $(this).text())
        return;

    var result = updateTask($(this).parent());
    console.log(result);
    if (result.responseJSON == false)
        $(this).text(tmp_text);
});

$("body").on("keypress", ".task__input", function (event) {
    if (event.which == 13) {
        event.preventDefault();
        $(this).blur();
    }
});

$("body").on("change", ".task__completed", function () {
    var result = updateTask($(this).parent());

    if (result == false)
        $(this).checked(!$(this).is(":checked"));
});

function updateTask($task) {
    var completed = $task.find(".task__completed").is(":checked");
    var text = $task.find(".task__text").text();
    var time = $task.find(".task__expiration-time").text();
    var id = $task.find(".tasks__button--remove").attr("data-id");

    if (isNaN(Date.parse(time)))
        return { responseJSON: false, responseText: "false" };

    return taskService.updateTask({ id, completed, text, expirationTime: time });
}


// update order task

$(".tasks__inner").sortable({
    update: function (event, ui) {
        var data = $(this).sortable("toArray", { attribute: "data-id" });
        taskService.updateOrderTask(data.map(Number));
    }
});