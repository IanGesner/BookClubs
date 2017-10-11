var fileSelect = document.getElementById("uploadPictureBtn");
var fileElement = document.getElementById("hiddenFileInput");

fileSelect.addEventListener("click", function (e) {
    if (fileElement) {
        fileElement.click();
    }
    console.log(e);
    e.preventDefault();
}, false);

fileElement.addEventListener("change", function () { outputDebug(this.files) });
fileElement.addEventListener("change", function () { updatePreview(this.files[0]) });

function outputDebug(files) {
    console.log("File length is" + files.length + ".\r\f Files are:");
    for (i = 0; i < files.length; i++) {
        console.log(files[i]);
    }
}

function updatePreview(file) {
    var imageType = /^image\//;

    if (!imageType.test(file.type)) {
        return;
    }

    var img = document.getElementById("profileImage");
    img.file = file;

    var reader = new FileReader();
    reader.onload = (function (aImg) { return function (e) { aImg.src = e.target.result; }; })(img);
    reader.readAsDataURL(file);
    console.log('Image loaded.');
}
