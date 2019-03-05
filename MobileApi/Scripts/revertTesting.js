console.log("killme")
//var downloadTemplate = document.getElementById("downloadTemplate");
//var myDatabaseTypes = ["WoodyPlant", "testType01"];
//var myDatabaseTypes = ["testType01", "WoodyPlant"];
var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
var oldDataFiles = document.getElementById("oldDataFiles");
var databaseTypes = document.getElementById("dbTypeID");
databaseTypes.addEventListener("change", getOldFiles);
var statusMessage = document.getElementById("resultMessage");
var FilePath;
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
    },
];

window.onload = function () {
    //POPULATE DATABASE TYPES
    for (i = 0; i < databaseTypeList.length; i++) {
        for (k = 0; k < myDatabaseTypes.length; k++) {
            if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                databaseTypes.innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
            };
        };
    };
    getOldFiles();
    dataFormSubmit();
}

function showOldDataFiles(oldDataFilesList) {
    //var oldDataFilesList = oldDataFilesListFound
    console.log(oldDataFiles)
    oldDataFiles.innerHTML = "";
    for (i = 0; i < oldDataFilesList.length; i++) {
        oldDataFiles.innerHTML += "<option value=" + oldDataFilesList[i].PathName + ">" + oldDataFilesList[i].Date + "</option>";
    };
}

function getOldFiles() {
    var dbTypeOptions = document.getElementById("dbTypeID");
    var dbTypeSelected = dbTypeOptions.value;
    //downloadTemplate.href = "../Datafolder/" + dbTypeSelected + "/Template.xlsx";
    $.ajax({
        url: '/Upload/GetPreviousDataFiles',
        type: 'POST',
        data: { "dbType": dbTypeSelected },
        dataType: 'json',
        success: function (data) {
            showOldDataFiles(data)

            console.log(data)
            displayStatusMessage("success", "Archive Data Files Available");
            //alert(data)
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            oldDataFiles.innerHTML = "";
            alert(customErrorMessage);
            displayStatusMessage(xhr.status, customErrorMessage);

        }
    });
}

function displayStatusMessage(xhr, status) {
    switch (xhr) {
        case "success":
            statusMessage.setAttribute("class", "successStatus")
            statusMessage.innerHTML = "Status: " + status;
            break;
        case 410:
            statusMessage.setAttribute("class", "failStatus")
            statusMessage.innerHTML = "Status: " + status;
            break;
        default:
            statusMessage.setAttribute("class", "")
            statusMessage.innerHTML = status + " " + xhr;
            break;
    }
}

function dataFormSubmit() {
    $("form#data").submit(function (e) {
        e.preventDefault();

        var formData = new FormData(this);
        console.log(formData)
        $.ajax({
            url: '/Upload/RevertDatabase',
            type: 'POST',
            data: formData,
            //data: { "dbFilePath": oldDataFiles.value},
            success: function (data) {
                console.log(data)
                displayStatusMessage("success", data);
            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                oldDataFiles.innerHTML = "";
                alert(customErrorMessage);
                displayStatusMessage(xhr.status, customErrorMessage);

            },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("suicide is for winners")
    });
}