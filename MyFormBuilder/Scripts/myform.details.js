
$(document).ready(function () {

    var frmData = $('#FormLayout').val();
    
    //const wrap = $('#fb-render');
    //const formRender = wrap.formRender();
    //// then
    //wrap.formRender('render', formData);
    //// or
    //formRender.actions.render(formData)

    const wrap = $('#fb-render');
    //alert(frmData);
    var formRenderOptions = {
        formData: frmData

    }
    var formRenderInstance = wrap.formRender(formRenderOptions);


    //SUBMIT the form
    $('form').submit(function () {


        var jsonObject = {
            "MyFormId": $('#Id').val(),
            "SubmittedData": $('#AjaxPostForm').serializeArray(),
            "id": "0",
            "DateTimeCreated": new Date($.now()),
            "ApplicationUserID": "TBD"
        };

        console.log(JSON.stringify(jsonObject));
        $.ajax({
            url: '/MyForms/SubmitForm/',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonObject),
            success: function (result) {

                alert("success " + result);
            },
            error: function (result) {
                alert("Failed");
            }

        });
        return false;
    });

});


function objectifyForm(formArray) {//serialize data function

    
    var indexed_array = {};

    $.map(formArray, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    console.log(indexed_array);

    return indexed_array;
}
