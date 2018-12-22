//var myDatabaseTypes = ["woody", "wetland"];
var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
//var myDatabaseTypes = ["woody"];
var plantTypeValue = "";
//var databaseTypes = document.getElementById("databaseTypes");
var databaseTypeList = [
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
var errorMessage = document.getElementById("errorMessage");

$("form#data").submit(function (e) {
    e.preventDefault();
    console.log("$FORM#DATA_START")

    var formData = new FormData(this);
    $.ajax({
        url: '/Upload/UploadFiles', // the method we are calling
        type: 'POST',
        data: formData,
        success: function (data) {
            document.getElementById("uploadFile").value = "";
            displayErrorMessage(data);
        },
        error: function (data) {
            displayErrorMessage("ajaxFail")
        },
        cache: false,
        contentType: false,
        processData: false
    });
    console.log("$FORM#DATA_END")
});


//databaseTypes.addEventListener("change", plantTypeChange);
function plantTypeChange() {
    plantTypeValue = databaseTypes.value;
    console.log("plantTypeValue changed CANDLELIGHT to: " + plantTypeValue)
}


window.onload = function () {
    console.log("window.onload START")
    var databaseTypes = document.getElementById("databaseTypes");
    databaseTypes.addEventListener("change", plantTypeChange);
    for (i = 0; i < databaseTypeList.length; i++) {
        for (k = 0; k < myDatabaseTypes.length; k++) {
            if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                databaseTypes.innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
            }
        }
    };
    plantTypeValue = databaseTypes.value;
    console.log("window.onload END")
};

function displayErrorMessage(error) {
    console.log("displayErrorMessage (): ..START.." + error)
    var status = "Status: ";
    switch (error) {
        case "dataSuccess":
            errorMessage.innerHTML = status + error
            break;
        case "dataCatch":
            errorMessage.innerHTML = status + error
            break;
        case "Successful":
            errorMessage.innerHTML = status + error
            break;
        case "ajaxFail":
            errorMessage.innerHTML = status + error
            break;
        default:
            errorMessage.innerHTML = status + error
            break;
    }
    console.log("displayErrorMessage (): ..END.." + error)
}