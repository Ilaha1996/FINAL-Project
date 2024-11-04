// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const dateInput = document.getElementById("date");
    const startTimeInput = document.getElementById("startTime");

    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    dateInput.setAttribute("min", tomorrow.toISOString().split("T")[0]);

    startTimeInput.addEventListener("input", function () {
        const time = this.value;
        if (time < "09:00" || time > "18:00") {
            alert("Start time must be between 09:00 and 18:00.");
            this.value = ""; 
        }
    });
});
