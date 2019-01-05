//var myDatabaseTypes = ["woody", "wetland"];
var myDatabaseTypes = ["woody", "wetland", "testType01"];
//var myDatabaseTypes = ["woody"];
//var databaseTypes = document.getElementById("databaseTypes");
//var btn = document.getElementById("proccessBtn")
var plantTypeValue = "test";// = document.getElementById("databaseType");;
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
//var uploadFileSelect = document.getElementById("uploadFileSelect");
var statusMessage = document.getElementById("errorMessage");
//var form = document.getElementById("godkillmeForm"); // You need to use standard javascript object here

$("form#data").submit(function (e) {
    e.preventDefault();

    var formData = new FormData(this);

    $.ajax({
        url: '/Upload/UncleButtz', // the method we are calling
        type: 'POST',
        data: formData,
        success: function (data) {
            console.log("we made it to the land of grand")
            //alert(data)
            //window.location.replace('messyaround')
            document.getElementById("uploadFile").value = "";
            displayStatusMessage("Successful");
        },
        error: function (data) {
            console.log("failme")
            //alert(data)
            displayStatusMessage("ajaxFail")
        },
        cache: false,
        contentType: false,
        processData: false
    });
});


databaseTypes.addEventListener("change", plantTypeChange);
function plantTypeChange() {
    plantTypeValue = databaseTypes.value;
    console.log("plantTypeValue changed to: " + plantTypeValue)
    //displayErrorMessage(plantTypeValue);
    //document.getElementById("databaseType").value = plantTypeValue;

}

//btn.addEventListener("click", uncleButt)

window.onload = function () {
    for (i = 0; i < databaseTypeList.length; i++) {
        for (k = 0; k < myDatabaseTypes.length; k++) {
            if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                databaseTypes.innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
            }
        }
    };
    plantTypeValue = databaseTypes.value;
    //document.getElementById("databaseType").value = plantTypeValue;
};

function displayStatusMessage(error) {
    console.log("error" + error)
    switch (error) {
        case "woody":
            statusMessage.innerHTML = "ERROR: you messed up, woody: " + error
            break;
        case "wetland":
            statusMessage.innerHTML = "you messed up, wetlandy: " + error
            break;
        case "Successful":
            statusMessage.innerHTML = error
            break;
        case "ajaxFail":
            statusMessage.innerHTML = "ERROR: Processing Error: " + error
            break;
        default:
            statusMessage.innerHTML = "Default ERROR: Processing Error: " + error
            break;
    }
}