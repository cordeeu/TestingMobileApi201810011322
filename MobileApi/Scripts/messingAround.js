var myDatabaseTypes = ["woody", "wetland"];
//var myDatabaseTypes = ["woody", "wetland","testType01"];
//var myDatabaseTypes = ["woody"];
var databaseTypes = document.getElementById("databaseTypes");
var btn = document.getElementById("proccessBtn")
var plantTypeValue;
var uploadFileSelect = document.getElementById("uploadFileSelect");
var errorMessage = document.getElementById("errorMessage");
databaseTypes.addEventListener("change", plantTypeChange);
var databaseTypeList = [
    {
        "value": "woody",
        "display": "Woody Plants",
    },
    {
        "value": "wetland",
        "display": "Wetlands",
    },
    {
        "value": "testType01",
        "display": "Test type 01",
    }
];
function plantTypeChange() {
    plantTypeValue = databaseTypes.value;
    console.log("plantTypeValue changed to: " + plantTypeValue)
    displayErrorMessage(plantTypeValue);
}

btn.addEventListener("click", uncleButt)

window.onload = function () {
    for (i = 0; i < databaseTypeList.length; i++) {
        for (k = 0; k < myDatabaseTypes.length; k++) {
            if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                databaseTypes.innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
            }
        }
    };
    plantTypeValue = databaseTypes.value;
};

function displayErrorMessage(error) {
    switch (error) {
        case "woody":
            errorMessage.innerHTML = "ERROR: you messed up, woody"
            break;
        case "wetland":
            errorMessage.innerHTML = "you messed up, wetlandy"
            break;
        default:
            errorMessage.innerHTML = "ERROR: Processing Error"
    }
}

function uncleButt() {
    console.log("we in here")
    $.ajax({
        type: "POST",
        url: "/Upload/UploadFiles", // the method we are calling
        contentType: "application/json; charset=utf-8",
        data: { dbType: plantTypeValue, uploadFile: uploadFileSelect },
        dataType: "json",
        success: function (msg) {
            console.log("WIN :" + uploadFileSelect);
        },
        error: function (msg) {
            console.log("FAIL :" + uploadFileSelect);
            //console.log(msg);
        }
    });
}
