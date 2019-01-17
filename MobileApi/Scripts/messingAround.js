//var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
var myDatabaseTypes = ["WoodyPlant", "testType01"];
var plantTypeValue = "";
var statusMessage;
statusMessage = document.getElementById("resultMessage");
window.onload = function () {
    console.log("window.onload START")
    //statusMessage = document.getElementById("resultMessage");
    console.log(statusMessage);

    var databaseTypeTemplate = document.getElementById("databaseTypes");
    databaseTypeTemplate.addEventListener("change", plantTypeChange);
    var databaseTypes = document.getElementsByClassName("databaseTypes");

    var formButton = document.getElementById("formSubmit");
    formButton.addEventListener("click", dataFormSubmit);
    console.log(formButton);

    console.log(databaseTypes);
    var downloadTemplate = document.getElementById("downloadTemplate");
    for (j = 0; j < databaseTypes.length; j++) {
        for (i = 0; i < databaseTypeList.length; i++) {
            for (k = 0; k < myDatabaseTypes.length; k++) {
                if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                    databaseTypes[j].innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
                };
            };
        };
    };
    plantTypeValue = databaseTypeTemplate.value;
    downloadTemplate.href = "../Datafolder/" + plantTypeValue + "/Template.xlsx";
    //urlExists(downloadTemplate.href)
    console.log("window.onload END")
};

//TESTING**********************
var statusResultMessage = [
    {
        "status": "successStatus",
        "message": "Unknown Success",
    }
];
console.log(statusResultMessage)

var formTestButton = document.getElementById("formImageSubmit");
formTestButton.addEventListener("click", testingSubmit);
function testingSubmit() {
    statusMessage.setAttribute("class", statusResultMessage[0].status)
    console.log(statusResultMessage.status)
    console.log(statusResultMessage.message)
    statusMessage.innerHTML = "Status Test: "
    statusMessage.innerHTML += statusResultMessage[0].message;
    console.log(statusMessage)
    console.log("weinittowinit")
}


//TESTING**********************

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
//var statusMessage = document.getElementById("errorMessage");
function dataFormSubmit() {
    console.log("$FORM#DATA_START")
    $("form#data").submit(function (e) {
        e.preventDefault();

        var formData = new FormData(this);
        $.ajax({
            url: '/Upload/UploadFiles', // the method we are calling
            type: 'POST',
            data: formData,
            success: function (data) {
                document.getElementById("uploadFile").value = "";
                displayStatusMessage(data);
                //testingSubmit();
            },
            error: function (data) {
                displayStatusMessage("ajaxFail")
            },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("$FORM#DATA_END")
    });
}
//databaseTypes.addEventListener("change", plantTypeChange);
function plantTypeChange() {
    plantTypeValue = databaseTypes.value;
    downloadTemplate.href = "../Datafolder/" + plantTypeValue + "/Template.xlsx";
    console.log(downloadTemplate)
    console.log("plantTypeValue changed smankdlight to: " + plantTypeValue)
}



function displayStatusMessage(error) {
    //console.log("displayErrorMessage (): ..START.." + error)
    var status = "Status: ";
    statusMessage.setAttribute("class", "failStatus")

    switch (error) {
        case "dataSuccess":
            statusMessage.innerHTML = status + error
            break;
        case "dataCatch":
            statusMessage.innerHTML = status + error
            break;
        case "Successful":
            statusMessage.innerHTML = status + error
            break;
        case "ajaxFail":
            statusMessage.innerHTML = status + error
            break;
        default:
            statusMessage.innerHTML = status + error
            break;
    }

    statusMessage.innerHTML += "<p><li text-decoration: underline>TIPS:</li><li>Database uploads must use the template format</li><li>Be sure to delete ALL Empty Rows including trailing Rows</li><li>The plant_imported_id column cannot contain a blank or non-Integer value</li></p > "
    //console.log("displayErrorMessage (): ..END.." + error)
}

