//var myDatabaseTypes = ["WoodyPlant", "Wetland", "testType01"];
var myDatabaseTypes = ["WoodyPlant", "Wetland"];
//var myDatabaseTypes = ["Wetland", "testType01"];
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
    var downloadTemplate = document.getElementById("downloadTemplate");
    plantTypeValue = databaseTypes.value;
    downloadTemplate.href = "../Datafolder/" + plantTypeValue + "/Template.xlsx";
    console.log(downloadTemplate)
    console.log("plantTypeValue changed smankdlight to: " + plantTypeValue)
}


window.onload = function () {
    console.log("window.onload START")
    var databaseTypeTemplate = document.getElementById("databaseTypes");
    databaseTypeTemplate.addEventListener("change", plantTypeChange);
    var databaseTypes = document.getElementsByClassName("databaseTypes");
    console.log(databaseTypes);
    var downloadTemplate = document.getElementById("downloadTemplate");
    for (j = 0; j < databaseTypes.length;j++){
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

function displayErrorMessage(error) {
    //console.log("displayErrorMessage (): ..START.." + error)
    var status = "Status: ";
    errorMessage.setAttribute("class", "failStatus")

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

    errorMessage.innerHTML += "<p><li text-decoration: underline>TIPS:</li><li>Database uploads must use the template format</li><li>Be sure to delete ALL Empty Rows including trailing Rows</li><li>The plant_imported_id column cannot contain a blank or non-Integer value</li></p > "
    //console.log("displayErrorMessage (): ..END.." + error)
}

//function urlExists(url, callback) {
//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState === 4) {
//            callback(xhr.status < 400);
//        }
//    };
//    xhr.open('HEAD', url);
//    xhr.send();
//}

//urlExists(someUrl, function (exists) {
//    console.log('"%s" exists?', someUrl, exists);
//});