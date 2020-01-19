
$(document).ready(function () {

    var options = {
        onSave: function (formData) {
            
            SaveMyData(formBuilder.formData);
        },
    };
    const fbEditor = document.getElementById("build-wrap");
    const formBuilder = $(fbEditor).formBuilder(options);
 
});

function SaveMyData(data) {
    //alert(data);

    

    var jsonObject = {
        "FormName": $('#txtformName').val(),
        "FormLayout": data
    };

    $.ajax({
        url: "/MyForms/SaveForm/",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(response);
        }
    });


}
