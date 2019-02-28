console.log("hariecarie")
var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
//var myDatabaseTypes = ["WoodyPlant", "testType01"];
//var myDatabaseTypes = ["testType01", "WoodyPlant"];
var oldDataFiles = document.getElementById("oldDataFiles");
//var databaseTypes = document.getElementsByClassName("databaseTypes");
var databaseTypes = document.getElementById("dbTypeID");
databaseTypes.addEventListener("change", getOldFiles);
var downloadTemplate = document.getElementById("downloadTemplate");
var statusMessage = document.getElementById("resultMessage");
console.log(downloadTemplate)

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
}

function showOldDataFiles(oldDataFilesListFound) {
    var oldDataFilesList = oldDataFilesListFound
    oldDataFiles.innerHTML = "";
    for (i = 0; i < oldDataFilesList.length; i++) {
        //console.log(oldDataFilesList[i].Name)
        //console.log(oldDataFilesList[i].Date)
        oldDataFiles.innerHTML += "<option value=" + oldDataFilesList[i].Name + ">" + oldDataFilesList[i].Date + "</option>";
    };
}

function getOldFiles() {
    var dbTypeOptions = document.getElementById("dbTypeID");
    var dbTypeSelected = dbTypeOptions.value;
    downloadTemplate.href = "../Datafolder/" + dbTypeSelected + "/Template.xlsx";
    $.ajax({
        url: '/Upload/GetPreviousDataFiles',
        type: 'POST',
        data: { "dbType": dbTypeSelected },
        dataType: 'json',
        success: function (data) {
            showOldDataFiles(data)
            console.log("GetPreviousdataFiles SUCCESS: " + new Date().toUTCString())
        },
        error: function (data) {
            alert("Get Files Method Not Retrieved              " + data)
        },
    });
}

function displayStatusMessage(error) {
    console.log("displayErrorMessage (): ..START.." + error)
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



// OLD code
//POPULATE DATABASE TYPES
//////for (j = 0; j < databaseTypes.length; j++) {
//////    for (i = 0; i < databaseTypeList.length; i++) {
//////        for (k = 0; k < myDatabaseTypes.length; k++) {
//////            if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
//////                databaseTypes[j].innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
//////            };
//////        };
//////    };
//////};

