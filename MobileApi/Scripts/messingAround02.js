var form = $('form')[0]; // You need to use standard javascript object here
var formData = new FormData(form);

var btn = document.getElementById("proccessBtn")
btn.addEventListener("click", processDataUpload);
function processDataUpload() {

$.ajax({

    url: '/Upload/UncleButtz', // the method we are calling
    type: 'post',
    dataType: 'json',
    contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
    processData: false, // NEEDED, DON'T OMIT THIS
    // ... Other options like success and etc
    data: { dbType: plantTypeValue, uploadFile: uploadFileSelect },
    success: function (msg) {
        console.log("WIN :" + uploadFileSelect + "   win   " + plantTypeValue);
    },
    error: function (msg) {
        console.log("FAIL :" + uploadFileSelect + "   fail   " + plantTypeValue);
        //console.log(msg);
    }
});
}