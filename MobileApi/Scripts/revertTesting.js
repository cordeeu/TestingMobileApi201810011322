//var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
var myDatabaseTypes = ["WoodyPlant", "testType01"];
var plantTypeValue = "";
var statusMessage;
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
    }
];

window.onload = function () {
    var formButton = document.getElementById("formSubmit");
    formButton.addEventListener("click", dataFormSubmit);
    console.log(formButton);
}
//var statusMessage = document.getElementById("errorMessage");
function dataFormSubmit() {
    console.log("$FORM#DATA_START")
    $("form#data").submit(function (e) {
        e.preventDefault();

        var formData = new FormData(this);
        $.ajax({
            url: '/Upload/GetPreviousDataFiles', // the method we are calling
            type: 'GET',
            data: formData,
            success: function (data) {
                
                displayStatusMessage(data);
                //testingSubmit();
            },
            error: function (data) {
                console.log("error in subbing")
                console.log(data)
            },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("$FORM#DATA_END")
    });
}
function aFunction(aInput) {
    aValue = aInput;
}

