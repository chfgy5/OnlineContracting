// Used by AgentTableEditable to check user entered date and save changes to the table 
function changeData(info_id, carrier_name, column_name) {
    var element = document.getElementById(info_id + "-" + carrier_name + "-" + column_name);
    var newValue = -1;
    while (newValue < 0) {
        newValue = prompt("Please enter a new value (0-4)", element.innerText);
        if (isNaN(newValue) || newValue > 5) { newValue = -1; }// Checks valid value. not checking neg, because while loop does.
    }
    if (newValue !== element.innerText && newValue !== null) {
        element.innerText = newValue; // changes front end
        $.post("/Admin/UpdateInfoTable", { id: info_id, column: column_name, value: newValue });// Changes backend
        alert("Changes Saved");
    }
    addStyle();
}

// Saves date entered on _AgentTableEditable to database
function saveDate(id, date) {
    $.post("/Admin/UpdateFollowUp", { id: id, date: date });
}

// Saves note entered on _AgentTableEditable to database
function saveNote(id, note) {
    console.log(id); console.log(note);
    $.post("/Admin/UpdateNote", { id: id, note: note });
}

// Used on _AgentTable and _AgentTableEditable to give cells color
function addStyle() {
    $('tr td').each(function () {
        if ($(this).text() === "0") $(this).css('background-color', 'black');
        else if ($(this).text() === "1") $(this).css('background-color', 'green');
        else if ($(this).text() === "2") $(this).css('background-color', 'red');
        else if ($(this).text() === "4") $(this).css('background-color', 'gray');
        else if ($(this).text() === "3") $(this).css('background-color', 'orange');
    });
}

// Used on SuperAdmin to only display one selection at a time
function showAdvisorStates(selection) {
    oldTable = document.getElementById(selection.oldvalue);
    if (oldTable !== null) { // Hides old table, Check if null for first selection
        oldTable.style.display = "none";
    }
    // Shows newly selected advisors table
    document.getElementById(selection.value).style.display = "block";
}

// Used in ContractForm to move cursor through fields such as SSN/NPN/etc
function autotab(current, to) {
    // For some reason "===" fails in the if statement...
    if (current.getAttribute && current.value.length == current.getAttribute("maxlength")) {
        to.focus();
    }
}

// Used for checking SSN/NPN, passsed user entered value and div id to be displayed if value is wrong
function checkNum(value, divId) {
    if ($.isNumeric(value) || value == "") {
        document.getElementById(divId).style.display = "none";
    } else {
        document.getElementById(divId).style.display = "block";
    }
}

// http://javascriptkit.com/script/script2/validatedate.shtml
function checkDate(value){
    var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
    if (!validformat.test(value))
        document.getElementById("dateInvalid").style.display = "block";
    else { //Detailed check for valid date ranges
        var monthfield = value.split("/")[0]
        var dayfield = value.split("/")[1]
        var yearfield = value.split("/")[2]
        var dayobj = new Date(yearfield, monthfield - 1, dayfield)
        if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
            document.getElementById("dateInvalid").style.display = "block";
        else
            document.getElementById("dateInvalid").style.display = "none";
    }
}