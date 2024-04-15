// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    document.addEventListener('DOMContentLoaded', function () {
    const textarea = document.getElementById('autoResizeTextarea');
    textarea.addEventListener('input', autoResize, false);

    function autoResize() {
    this.style.height = 'auto';
    this.style.height = (this.scrollHeight) + 'px';
}
});
