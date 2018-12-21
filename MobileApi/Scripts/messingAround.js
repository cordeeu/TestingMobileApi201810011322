//var myDatabaseTypes = ["woody", "wetland"];
var myDatabaseTypes = ["woody", "wetland", "testType01"];
//var myDatabaseTypes = ["woody"];
var databaseTypes = document.getElementById("databaseTypes");
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
var uploadFileSelect = document.getElementById("uploadFileSelect");
var errorMessage = document.getElementById("errorMessage");
var form = document.getElementById("godkillmeForm"); // You need to use standard javascript object here
//var form = $('godkillmeForm')[0]; // You need to use standard javascript object here

var btn = document.getElementById("formSubmit");
btn.addEventListener("click", processDataUpload);

function processDataUpload() {

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
                displayErrorMessage("Success");
            },
            error: function (data) {
                console.log("failme")
                //alert(data)
                displayErrorMessage("ajaxFail")
            },
            cache: false,
            contentType: false,
            processData: false
        });
    });
    //console.log("killme")
    //var fd = new FormData();
    //fd.append('file', $('#file')[0].files[0]);

    //console.log(fd)
    //$.ajax({

    //    url: '/Upload/UncleButtz', // the method we are calling
    //    type: 'post',
    //    dataType: 'json',
    //    contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
    //    processData: false, // NEEDED, DON'T OMIT THIS
    //    // ... Other options like success and etc
    //    data:fd,
    //    //data: { dbType: plantTypeValue, uploadFile: uploadFileSelect },
    //    success: function (msg) {
    //        console.log("WIN :" + uploadFileSelect + "   win   " + plantTypeValue);
    //    },
    //    error: function (msg) {
    //        console.log("FAIL :" + uploadFileSelect + "   fail   " + plantTypeValue);
    //        //console.log(msg);
    //    }
    //});
}

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

function displayErrorMessage(error) {
    console.log("error" + error)
    switch (error) {
        case "woody":
            errorMessage.innerHTML = "ERROR: you messed up, woody: " + error
            break;
        case "wetland":
            errorMessage.innerHTML = "you messed up, wetlandy: " + error
            break;
        case "Success":
            errorMessage.innerHTML = "Process: " + error
            break;
        case "ajaxFail":
            errorMessage.innerHTML = "ERROR: Processing Error: " + error
            break;
        default:
            errorMessage.innerHTML = "Default ERROR: Processing Error: " + error
            break;
    }
}

function uncleButt() {
    console.log("uncleButt up in here")


    $('#form').submit(function () {
        $.ajax({
            url: $('#form').attr('action'),
            type: 'POST',
            data: $('#form').serialize(),
            success: function () {
                console.log('form submitted.');
            }
        });
        return false;
    });
    /*
    console.log("we in here")
    $.ajax({
        url: '/Upload/UncleButtz', // the method we are calling
        type: 'post',
        dataType: 'json',
        contentTye: 'application/json; charset=utf-8',
        data: { dbType: plantTypeValue, uploadFile: uploadFileSelect },
        success: function (msg) {
            console.log("WIN :" + uploadFileSelect + "   win   " + plantTypeValue);
        },
        error: function (msg) {
            console.log("FAIL :" + uploadFileSelect + "   fail   " + plantTypeValue);
            //console.log(msg);
        }
    });
    */
}
