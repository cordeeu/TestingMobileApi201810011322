//var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
//var myDatabaseTypes = ["WoodyPlant", "testType01"];
var myDatabaseTypes = ["testType01", "WoodyPlant"];
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
    console.log("oldDataFiles")
    console.log(oldDataFiles)

    formButton = document.getElementById("formSubmit");
    formButton.addEventListener("click", dataFormSubmit);
    var databaseTypes = document.getElementsByClassName("databaseTypes");

    console.log("click FOrm");
    console.log(formButton);



    for (j = 0; j < databaseTypes.length; j++) {
        for (i = 0; i < databaseTypeList.length; i++) {
            for (k = 0; k < myDatabaseTypes.length; k++) {
                if (Object.values(databaseTypeList[i]).indexOf(myDatabaseTypes[k]) > -1) {
                    databaseTypes[j].innerHTML += "<option value=" + databaseTypeList[i].value + ">" + databaseTypeList[i].display + "</option>";
                };
            };
        };
    };

    console
    dbTypeSelected = document.getElementById("dbTypeID");
    console.log(dbTypeSelected.value)
    console.log(dbTypeSelected)
    console.log(oldDataFiles)
    var smallsvile = "woody";
    $.ajax({
        //url: '/Upload/UploadFiles', // the method we are calling
        url: '/Upload/GetPreviousDataFiles', // the method we are calling
        type: 'POST',
        //data: dbType = dbTypeSelected.value,
        contentType: "application/json; charset=utf-8",
        data: {dbType:smallsvile},
        dataType: 'json',
        success: function (bob) {
            //displayStatusMessage(data);
            //console.log("bob");
            //console.log(bob);
            //oldDataFilesList = bob;
            //console.log("oldDataFilesList");
            console.log(oldDataFilesList);

            showOldDataFiles();
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

