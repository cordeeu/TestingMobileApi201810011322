//var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
var myDatabaseTypes = ["WoodyPlant", "testType01"];
var plantTypeValue = "";
var statusMessage;
var formButton;
var aValue = [
    {
        "value": "WoodyPlant",
        "display": "Woody Plants",
    },
    {
        "value": "Wetland",
        "display": "Wetlands",
    },
    {
        "value": "testType01",
        "display": "Test type 01",
    },
];

window.onload = function () {
    formButton = document.getElementById("formSubmit");
    formButton.addEventListener("click", dataFormSubmit);
    console.log("click FOrm");
    console.log(formButton);
}
//var statusMessage = document.getElementById("errorMessage");
function dataFormSubmit() {
    var killmerhonda = "1515";
    console.log("$FORM#DATA_START00010101")
    $("form#data").submit(function (e) {
        e.preventDefault();
        console.log(formButton)

        var formData = new FormData(this);
        $.ajax({
            //url: '/Upload/UploadFiles', // the method we are calling
            url: '/Upload/GetPreviousDataFiles', // the method we are calling
            type: 'POST',
            data: formData,
            //dataType: 'json',
            success: function (data) {
                //displayStatusMessage(data);
                console.log("SUCCESS: " + killmerhonda + new Date().toUTCString())
                console.log(data);
                //testingSubmit();
            },
            error: function (data) {
                console.log("error in subbing")
                console.log(data);
                killMeSenseless(data);
                console.log("TiMESTAMP: " + killmerhonda + new Date().toUTCString())
            },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("$FORM#DATA_END")
    });
}
function killMeSenseless(data) {

    console.log("killmesenslesSTart")
    console.log(data)
    console.log("KIllmesenslessEND")
}
function aFunction(aInput) {
    aValue = aInput;
}

