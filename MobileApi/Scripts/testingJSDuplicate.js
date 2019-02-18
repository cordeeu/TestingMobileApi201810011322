//formButton = document.getElementById("formSubmit");
//formButton.addEventListener("click", dataFormSubmit);

window.onload = function () {
    //oldDataFiles = document.getElementsByClassName("oldDataFiles");
    console.log("Loading Window")
    $("form#data").submit(function (e) {
        e.preventDefault();

        var formData = new FormData(this);
        $.ajax({
            //url: '/Upload/UploadFiles', // the method we are calling
            url: '/Upload/testingDuplicatingJSVoid', // the method we are calling
            type: 'POST',
            data: formData,
            //dataType: 'json',
            success: function () { console.log("SUCCESS: "  + new Date().toUTCString()) },
            error: function () { console.log("erroring in subbing") },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("ENDOFWATCH:" + new Date().toUTCString())
    });

    //formButton = document.getElementById("formSubmit");
    //formButton.addEventListener("click", dataFormSubmit);

}


//function dataFormSubmit() {
//    var killmerhonda = "1515";
//    //console.log("$FORM#DATA_START00010101")
    
//}