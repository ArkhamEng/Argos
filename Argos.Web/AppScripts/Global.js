﻿
function DateValid()
{
    //validación de formato de fecha
    $.validator.addMethod("date", function (value, element)
    {
        if (typeof($(element).data('val-required')) == typeof(undefined) && (value == '' || value =='__/__/____'))
        {
            console.log("No se valida un carajo"); return true;
        }
            
        var v = (this.optional(element) ||  moment(value,'DD/MM/YYYY',true).isValid());
        return v;

    }, "El formato debe ser dd/mm/yyyy");
}


function SetPointer(element) {
    $(element).css('cursor', 'pointer');
}

//Hace click en el boton del datatable
function ExcelDT() {
    $(".buttons-excel").click();
}

function Compleate(textbox, list, url, onSelected) {
    textbox.autocomplete(
      {
          source: function (request, response) {
              ExecuteAjax(url, { filter: request.term }, function (json) {
                  list.empty();
                  for (var i = 0; i < json.length; i++) {
                      list.append($('<option data-id=' + json[i].Id + '></option>').val(json[i].Label).html(json[i].Value));
                  }
              });
          },
          minLength: 4
      });

    //this is executed when an option from DataList is selected
    textbox.off('input').bind('input', function () {
        var val = this.value;

        if (list.find('option').filter(function () {
            return this.value.toUpperCase() === val.toUpperCase();
        }).length) {
            var option = list.find('option').filter(function () {
                return this.value.toUpperCase() === val.toUpperCase();
            });

            var value = option.text();
            var id = option.data("id");

            if (onSelected != null) {
                onSelected(id, value);
            }
        }
    });
}

//AJAX CALL
function ExecuteAjax(url, parameters, callback) {
    $.ajax({
        url: url,
        type: "POST",
        data: parameters,
        success: function (response)
        {
            if ($.isPlainObject(response) && typeof (response.Code) != "undefined" && response.Code != 200)
            {
                HideLoading();
                HideModLoading();
                ShowNotify(response.Header, response.Result, response.Body, 3500);

                switch (response.Code)
                {
                    case 401:
                        window.location = response.Extra;
                        break;
                }
            }
            else {
                callback(response);
            }
        },
        error: function ()
        {
            HideLoading();
            HideModLoading();
            ShowNotify("Error Inesperado!", "dark", "ocurrio un error, quiza has perdido la conexion a internet!", 3500);
        }
    });
}

function SumitAjax(url, formData, callback, errorCallBack)
{
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        cache: false,
        success: function (response)
        {
            if ($.isPlainObject(response) && typeof (response.Code) != "undefined" && response.Code != 200)
            {
                HideLoading();
                HideModLoading();
                ShowNotify(response.Header, response.Result, response.Body, 3500);

                switch (response.Code)
                {
                    case 401:
                        window.location = response.Extra;
                        break;
                }
            }
            else
            {
                HideLoading();
                HideModLoading();
                callback(response);
            }
        },
        error: function ()
        {
            HideLoading();
            ShowNotify("Error Inesperado!", "dark", "ocurrio un error, quiza has perdido la conexion a internet!", 3500);

            if (errorCallBack != null)
                errorCallBack();
        }
    });
}


function ResizeDT() 
{
    $($.fn.dataTable.tables(true)).DataTable().responsive.recalc();
}



//Realiza paginación  sobre una tabla
function Paginate(table, iniRecords, responsive, filter, scrollX, buttonContainer, onPagination)
{
    var searching = false;

    if (typeof (filter) != 'undefined')
        searching = true;

    var oTable = $(table).DataTable(
       {
           destroy: true,
           // keys: true,
           scrollX: scrollX,
           scrollCollapse: true,
           fixedHeader: true,
           //fixedColumns:{
           //    leftColumns: 3,
           //    rightColumns: 1 },
           responsive: responsive,
           "fnDrawCallback": function (oSettings)
           {
               if(onPagination != null && typeof(onPagination) != undefined)
               {
                   onPagination();
               }
           },
           "lengthChange": false,
           "searching": searching,
           "order": [], //versiones mas nuevas
           "aaSorting":[],
           "lengthMenu": [[5, 10, 20, 50, 100, -1], [5, 10, 20, 50, 100, "All"]],
           "pageLength": iniRecords,
           "language": {
               "search": "filtrar resultados",
               "lengthMenu": "mostrar  _MENU_ ",
               "zeroRecords": "no hay datos disponibles",
               "info": "página _PAGE_ de _PAGES_",
               "infoEmpty": "",
               "infoFiltered": "(filtrado de _MAX_ total registros)",
               "paginate": {
                   "previous": "Anterior",
                   "next": "Siguiente"
               }
           },
       });

    if (typeof (filter) != 'undefined')
    {
        $(filter).keyup(function () {
            oTable.data().search(this.value).draw();
        });

        $(table + "_filter").addClass("hidden");
    }

    if (typeof (buttonContainer) != 'undefined')
    {
        $(buttonContainer).html("");

        new $.fn.dataTable.Buttons(oTable, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    text: '<i class="fa fa-files-o"></i> Copiar',
                    titleAttr: 'Copy',
                    className: "btn btn-default"
                },
                {
                    extend: 'excelHtml5',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    titleAttr: 'Excel',
                    className: "btn btn-default"
                },
                 {
                     extend: 'pdfHtml5',
                     text: '<i class="fa fa-file-pdf-o"></i> PDF',
                     titleAttr: 'PDF',
                     className: "btn btn-default"
                 }
            ]
        }).container().appendTo($(buttonContainer));
    }
}

//SHOWS MODAL WITH CUSTON FUNCTIONS AND CONTENT
function ShowModal(html, backdrop, size) {
    if (size == 'lg')
        $("#ModalDialog").addClass('modal-lg');

    if (size == 'sm')
        $("#ModalDialog").addClass('modal-sm');

    $("#ModalLoading").children().hide();

    $("#ModalContent").html(html);

    $("#SiteModal").modal({ backdrop: backdrop });
}

//Hide Main Modal
function HideModal(callback, removeContent) {
    $('#SiteModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
        if (callback != null)
            callback();

        if (removeContent)
            $("#SiteModalContent").html('');
    });

    $("#SiteModal").modal('hide');

}

//Show Child Modal 
function ShowChildModal(content) {
    $("#ChildModal").off("shown.bs.modal").on('shown.bs.modal', function () {
        $('#SiteModal').css('opacity', .7);
        $('#SiteModal').unbind();
    });

    $("#ChildModalContent").html(content);
    $("#ChildModal").css("margin-top", "100px");
    $("#ChildModal").modal({ backdrop: 'static' });
}

//Hide Child Modal 
function HideChildModal() {
    $('#ChildModal').off('hidden.bs.modal').on('hidden.bs.modal', function () {
        $('#SiteModal').css('opacity', 1);
        $('#SiteModal').removeData("modal").modal({});
        $('body').addClass("modal-open");
        $("#ChildModalContent").html("");
    });

    $('#ChildModal').modal("hide");
}


//Loading In Modal
function ShowModLoading() {
    $("#ModalContent").children().hide();
    $("#ModalLoading").children().show();
}

function HideModLoading() {
    $("#ModalLoading").children().hide();
    $("#ModalContent").children().show();
}

//DROP DOWN CASCADE
function SetCascade(ddlParent, ddlChild, url) {
    $(ddlParent).unbind('change').change(function (e) {
        if ($(ddlParent).val() != '') {
            var parentId = $(ddlParent).val();

            ExecuteAjax(url, { id: parentId }, function (data) {
                $(ddlChild).empty();
                $(ddlChild).append($('<option></option>').val("").html(""));

                for (var i = 0; i < data.length; i++) {
                    $(ddlChild).append($('<option></option>').val(data[i].Value).html(data[i].Text));
                }
                if (data.length > 0)
                    $(ddlChild).attr("readonly", false);
                else
                    $(ddlChild).attr("readonly", true);
            });
        }
        else {
            $(ddlChild).empty();
            $(ddlChild).attr("readonly", true);
        }
    });
}


//LOADING CONTROL FUNCTIONS
function ShowLoading(backdrop) {
    $("#Loading").modal({ backdrop: backdrop });
}

function HideLoading(callback) {
    $("#Loading").off("hidden.bs.modal").on("hidden.bs.modal", function (e) {
        if (callback != null)
            callback();
    });
    $("#Loading").modal('hide');
}



//CONFIRM CONTROL FUNCTIONS
function ShowMessage(textHeader, textBody, type, confirmCallBack, cancelCallBack, backdrop) {
    //header and body text
    $("#MessageHeader").text(textHeader);
    $("#MessageBody").text(textBody);

    $("#MessageGroup").children().hide();
    $("#MessageOk").show();

    //setting Image and header color
    if (type == 'success') {
        $("#MessageContent").attr("class", 'modal-content panel panel-success');
        $("#MessageImage").attr("src", '/Images/success.png');
        $("#MessageOk").attr('class', 'btn btn-success');
    }
    else if (type == 'warning') {
        $("#MessageContent").attr("class", 'modal-content panel panel-warning');
        $("#MessageImage").attr("src", '/Images/warning.png');
        $("#MessageOk").attr('class', 'btn btn-warning');
    }
    else if (type == 'confirm') {
        $("#MessageContent").attr("class", 'modal-content panel panel-info');
        $("#MessageImage").attr("src", '/Images/question.png');
        $("#MessageGroup").children().show();
        $("#MessageOk").hide();
    }


    //binding button acctions
    $("#MessageConfirm").unbind('click').click(function (e) {
        HideMessage(true, function () {
            ShowLoading('static');

            if (confirmCallBack != null)
                confirmCallBack();
        });
    });

    $("#MessageCancel").unbind('click').click(function (e) {
        HideMessage(true, cancelCallBack);
    });

    $("#MessageOk").unbind('click').click(function (e) {

        HideMessage(true, cancelCallBack);
    });

    //rise modal
    $("#ModalMessage").modal({ backdrop: backdrop });
}

function HideMessage(cleaUp, callback) {
    $('#ModalMessage').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
        if (callback != null)
            callback();

        if (cleaUp) {
            $("#MessageHeader").text('');

            $("#MessageBody").html('');
            $("#MessageContent").attr("class", 'modal-content');
            $("#MessageImage").attr("src", '');
        }
    });
    $("#ModalMessage").modal('hide');
}

function ShowNotify(title, type, message, delay) {
    var dly = 2500;

    if (isNaN("") || typeof (delay) != 'undefined')
        dly = delay;

    if (typeof (PNotify) === 'undefined') {
        console.log("PNotify no definido");
        return;
    }

    if (type == 'danger')
        type = "error";

    new PNotify({
        title: title,
        type: type,
        text: message,
        styling: 'bootstrap3',
        delay: dly,
        hide: true,
    });
};



function iCheckInit() {
    if ($("input.flat")[0]) {
        $(document).ready(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        });
    }
}