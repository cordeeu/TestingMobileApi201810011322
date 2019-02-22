var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
//var myDatabaseTypes = ["WoodyPlant", "testType01"];
//var myDatabaseTypes = ["testType01", "WoodyPlant"];
var plantTypeValue = "";
var statusMessage;
var dbTypeSelected;
var formButton;
var oldDataFilesList;

oldDataFilesList = [{ "name": "WoodyPlant_Archive_data2019-01-03--14-24-50.xlsx", "date": "03-Jan-19 14:24:50" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-29-34.xlsx", "date": "03-Jan-19 14:29:34" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-30-46.xlsx", "date": "03-Jan-19 14:30:46" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-36-56.xlsx", "date": "03-Jan-19 14:36:56" }, { "name": "WoodyPlant_Archive_data2019-01-03--14-39-21.xlsx", "date": "03-Jan-19 14:39:21" }, { "name": "WoodyPlant_Archive_data2019-01-10--12-45-21.xlsx", "date": "10-Jan-19 12:45:21" },]
var oldDataFiles = document.getElementById("oldDataFiles");
console.log("oldDataFiles")
console.log(oldDataFiles)
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
    //oldDataFiles = document.getElementsByClassName("oldDataFiles");
    showOldDataFiles();

    //$("form#data").submit(runSubmit());
    $("form#data").submit(function (e) {
        e.preventDefault();

        var formData = new FormData(this);
        $.ajax({
            //url: '/Upload/UploadFiles', // the method we are calling
            url: '/Upload/GetPreviousDataFiles', // the method we are calling
            type: 'POST',
            data: formData,
            dataType: 'json',
            success: function () {
                //alert(data)
                alert("success Alert message")
                document.write("success document.write message")
                console.log("SUCCESS: " + new Date().toUTCString())
            },
            error: function () {
                //alert(data)
                alert("error Alert message")
                document.write("error document.write message")
                console.log("erroring in subbing")
            },
            cache: false,
            contentType: false,
            processData: false
        });
        console.log("ENDOFWATCH:" + new Date().toUTCString())
    });

    console.log("oldDataFiles")
    console.log(oldDataFiles)

    //formButton = document.getElementById("formSubmit");
    //formButton.addEventListener("click", dataFormSubmit);

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
    console.log(dbTypeSelected.value)
    console.log(dbTypeSelected)
    console.log(oldDataFiles)
}

function showOldDataFiles() {
    console.log("showingolddatafiles")
    console.log(oldDataFilesList.length)
    console.log(oldDataFiles)

    for (i = 0; i < oldDataFilesList.length; i++) {
        console.log(oldDataFilesList[i].name)
        console.log(oldDataFilesList[i].date)
        oldDataFiles.innerHTML += "<option value=" + oldDataFilesList[i].name + ">" + oldDataFilesList[i].date + "</option>";
    };

    console.log("showingolddatafilesEND")
}

//function runSubmit() {
//    console.log("killmepleasejesus")

//        //e.preventDefault();

//        var formData = new FormData();
//        $.ajax({
//            //url: '/Upload/UploadFiles', // the method we are calling
//            url: '/Upload/testingDuplicatingJSVoid', // the method we are calling
//            type: 'POST',
//            data: formData,
//            //dataType: 'json',
//            success: function () { console.log("SUCCESS: " + new Date().toUTCString()) },
//            error: function () { console.log("erroring in subbing") },
//            cache: false,
//            contentType: false,
//            processData: false
//        });
//        console.log("ENDOFWATCH:" + new Date().toUTCString())

//}