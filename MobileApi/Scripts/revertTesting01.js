var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
//var myDatabaseTypes = ["WoodyPlant", "testType01"];
//var myDatabaseTypes = ["testType01", "WoodyPlant"];
var plantTypeValue = "";
var statusMessage;
var dbTypeSelected;
var formButton;



//var oldDataFilesList01;
//oldDataFilesList01 = [{ "name": "WoodyPlant_Archive_data2019-01-03--14-24-50.xlsx", "date": "03-Jan-19 14:24:50" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-29-34.xlsx", "date": "03-Jan-19 14:29:34" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-30-46.xlsx", "date": "03-Jan-19 14:30:46" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-36-56.xlsx", "date": "03-Jan-19 14:36:56" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-39-21.xlsx", "date": "03-Jan-19 14:39:21" }, { "name": "WoodyPlant_Archive_data2019-01-10--12-45-21.xlsx", "date": "10-Jan-19 12:45:21" },]
//var oldDataFilesList03 = [{ "name": "WoodyPlant_Archive_data2019-01-03--14-24-50.xlsx", "date": "03-Jan-19 14:24:50" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-29-34.xlsx", "date": "03-Jan-19 14:29:34" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-30-46.xlsx", "date": "03-Jan-19 14:30:46" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-36-56.xlsx", "date": "03-Jan-19 14:36:56" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-39-21.xlsx", "date": "03-Jan-19 14:39:21" }, { "name": "WoodyPlant_Archive_data2019-01-10--12-45-21.xlsx", "date": "10-Jan-19 12:45:21" },]

var oldDataFiles = document.getElementById("oldDataFiles");

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

//var killMeTender = document.getElementById("killME")
//killMeTender.addEventListener("click", getOldFiles())
window.onload = function () {
    //POPULATE DATABASE TYPES
    console.log(killMeTender);
    console.log("where are we");
    var databaseTypes = document.getElementsByClassName("databaseTypes");
    for (j = 0; j < databaseTypes.length; j++) {
        for (i = 0; i < databaseTypeList.length; i++) {
            for (k = 0; k < myDatabaseTypes.length; k++) {
                if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                    databaseTypes[j].innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
                };
            };
        };
    };

    dbTypeSelected = document.getElementById("dbTypeID");
    //oldDataFiles = document.getElementsByClassName("oldDataFiles");
    //showOldDataFiles();

    //$("form#data").submit(runSubmit());
    $("form#data").submit(function (e) {
        console.log("startofsubmit")
        e.preventDefault();

        var formData = new FormData(this);
        $.ajax({
            url: '/Upload/GetPreviousDataFilesTest', // method testing
            //url: '/Upload/GetPreviousDataFiles', 
            type: 'POST',
            data: formData,
            dataType: 'json',
            success: function (data) {

                alert(data)
                showOldDataFiles(data)
                console.log("GetPreviousdataFiles SUCCESS: " + new Date().toUTCString())
            },
            error: function (data) {
                alert("Get Files Method Not Found")
            },
            //cache: false,
            contentType: false,
            processData: false
        });
        console.log("ENDOFWATCH:" + new Date().toUTCString())
    });


    //formButton = document.getElementById("formSubmit");
    //formButton.addEventListener("click", dataFormSubmit);


}

function showOldDataFiles(oldDataFilesListFound) {
    var oldDataFilesList = oldDataFilesListFound
    console.log("showolddatafails")
    //var oldDataFilesList = oldDataFilesList03;

    oldDataFiles.innerHTML = "";
    //oldDataFilesList = oldDataFilesList03;

    for (i = 0; i < oldDataFilesList.length; i++) {
        console.log(oldDataFilesList[i].Name)
        console.log(oldDataFilesList[i].ate)
        oldDataFiles.innerHTML += "<option value=" + oldDataFilesList[i].Name + ">" + oldDataFilesList[i].Date + "</option>";
    };

    console.log("showingolddatafilesEND")
}

function getOldFiles(someValue) {
    console.log("getOldYeller a sleeping dog lying ")
    var typeSelect = document.getElementById("dbTypeID");
    //var grabIt = /*{ 'dbType': typeSelect.value }*/

    //$.ajax({
    //    //url: '/Upload/UploadFiles', // the method we are calling
    //    url: '/Upload/GetPreviousDataFilesTest', // the method we are calling
    //    type: 'POST',
    //    data: { 'dbType': typeSelect.value },
    //    //dataType: 'json',
    //    success: function () { console.log("SUCCESS: " + new Date().toUTCString()) },
    //    error: function () { console.log("erroring in subbing") },
    //    cache: false,
    //    contentType: false,
    //    processData: false
    //});
}


