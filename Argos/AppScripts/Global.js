
//AJAX CALL
function ExecuteAjax(url, parameters, callback)
{
    $.ajax({
        url: url,
        type: "POST",
        data: parameters,
        error: function (data)
        {
            callback("Error al ejecutar Ajax")
        },
        success: function (data)
        {
            callback(data);
        },
        statusCode:
        {
            200: function (data) {
                callback(data);
            },
            401: function (data) {
                callback(data);
            }
        }
    });
}

//DATA TABLE
function Paginate(table, iniRecords,allowSearch)
{
    var oTable = $(table).DataTable(
       {
           destroy: true,
           "lengthChange": false,
           "searching": allowSearch,
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
           }
       });
}

//SHOWS MODAL WITH CUSTON FUNCTIONS AND CONTENT
function ShowModal(header, html, confirmCallBack, cancelCallBack, backdrop)
{
    $("#ModalLoading").children().hide();
    $("#SiteModalHeader").html(header);

    $("#SiteModalBody").html(html);

    $("#SiteModalConfirm").unbind('click').click(function (e)
    {
        confirmCallBack();
    });

    $("#SiteModalCancel").unbind('click').click(function (e)
    {
        HideModal(true, cancelCallBack);
    });

    $("#SiteModal").modal({ backdrop: backdrop });
}

//REPLACE MODAL CONTENT
function ReplaceModal(header, html)
{
    $("#SiteModalHeader").html(header);

    $("#SiteModalBody").html(html);  
}


function HideModal(cleaUp, callback)
{
    $('#SiteModal').off('hidden').on('hidden.bs.modal', function (e)
    {
        callback();

        if (cleaUp)
        {
            $("#SiteModalHeader").html('');

            $("#SiteModalBody").html('');
        }
    });
    $("#SiteModal").modal('hide');
}


//DROP DOWN CASCADE
function SetCascade(ddlParent, ddlChild, url)
{
    $(ddlParent).unbind('change').change(function (e)
    {
       console.log("Cascade execution")
        if ($(ddlParent).val() != '')
        {
            var parentId = $(ddlParent).val();

            ExecuteAjax(url, { id: parentId }, function (data)
            {
                $(ddlChild).empty();
                $(ddlChild).append($('<option></option>').val("").html(""));
                for (var i = 0; i < data.length; i++)
                {
                    $(ddlChild).append($('<option></option>').val(data[i].Value).html(data[i].Text));
                }
                if (data.length > 0)
                    $(ddlChild).attr("readonly", false);
                else
                    $(ddlChild).attr("readonly", true);
            });
        }
        else
        {
            $(ddlChild).empty();
            $(ddlChild).attr("readonly", true);
        }

    });
}


//LOADING CONTROL FUNCTIONS
function ShowLoading(backdrop)
{
    $("#Loading").modal({ backdrop: backdrop });
}

function HideLoading(callback)
{
    $('#Loading').off('hidden').on('hidden.bs.modal', function (e)
    {
        callback();
    });
    $("#Loading").modal('hide');
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

//CONFIRM CONTROL FUNCTIONS
function ShowConfirm(textHeader, textBody, confirmCallBack, cancelCallBack, backdrop)
{
    $("#ConfirmHeader").text(textHeader);

    $("#ConfirmBody").text(textBody);

    $("#btnConfirm").unbind('click').click(function (e)
    {
        confirmCallBack();
    });

    $("#btnCancel").unbind('click').click(function (e)
    {
        HideConfirm(true, cancelCallBack);
    });

    $("#ModalConfirm").modal({ backdrop: backdrop });
}

function HideConfirm(cleaUp,callback)
{
    $('#ModalConfirm').off('hidden').on('hidden.bs.modal', function (e)
    {
        callback();

        if (cleaUp) {
            $("#ConfirmHeader").text('');

            $("#ConfirmBody").html('');
        }
    });
    $("#ModalConfirm").modal('hide');
}
