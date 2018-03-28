function SetPointer(element) {
    $(element).css('cursor', 'pointer');
}


function Compleate(textbox,list,url,onSelected)
{
    $(textbox).autocomplete(
      {
          source: function (request, response) 
          {
              ExecuteAjax(url, { filter: request.term }, function (json) 
              {
                  $(list).empty();
                  for (var i = 0; i < json.length; i++) {
                      $(list).append($('<option data-id=' + json[i].Id + '></option>').val(json[i].Label).html(json[i].Value));
                  }
              });
          },
          minLength: 4
      });

    //this is executed when an option from DataList is selected
    $(textbox).bind('input', function ()
    {
        var val = this.value;
        if ($(list).find('option').filter(function () {
            return this.value.toUpperCase() === val.toUpperCase();
        }).length) {
            var option = $(list).find('option').filter(function () {
                return this.value.toUpperCase() === val.toUpperCase();
            });


            var value = option.text();
            var id = option.data("id");
          
            if (onSelected != null)
            {
                onSelected(id,value);
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
        error: function (data) {
            callback("Error al ejecutar Ajax")
        },
        success: function (data) {
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
function Paginate(table, iniRecords, allowSearch) {
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
function ShowModal(header, html, confirmCallBack, cancelCallBack, backdrop, isRegularSize)
{
    if (!isRegularSize)
        $("#ModalDialog").attr('class', 'modal-dialog modal-lg');
    else
        $("#ModalDialog").attr('class', 'modal-dialog');

    $("#ModalLoading").children().hide();
    $("#SiteModalHeader").html(header);

    $("#SiteModalBody").html(html);

    $("#SiteModalConfirm").unbind('click').click(function (e) {
        confirmCallBack();
    });

    $("#SiteModalCancel").unbind('click').click(function (e) {
        HideModal(true, cancelCallBack);
    });

    $("#SiteModal").modal({ backdrop: backdrop });
}

//REPLACE MODAL CONTENT
function ReplaceModal(header, html)
{
    $("#SiteModalHeader").html(header);

    $("#SiteModalBody").html(html);

    HideModLoading();
}


function HideModal(cleaUp, callback)
{
    $('#SiteModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e)
    {
        if (callback != null)
            callback();

        if (cleaUp) {
            $("#SiteModalHeader").html('');

            $("#SiteModalBody").html('');
        }
    });

    HideModLoading();
    $("#SiteModal").modal('hide');

}


//Loading In Modal
function ShowModLoading()
{
    $("#ModalContent").children().hide();
    $("#ModalLoading").children().show();
}

function HideModLoading()
{
    $("#ModalLoading").children().hide();
    $("#ModalContent").children().show();
}

//DROP DOWN CASCADE
function SetCascade(ddlParent, ddlChild, url) {
    $(ddlParent).unbind('change').change(function (e) {
        console.log("Cascade execution")
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
function ShowLoading(backdrop)
{
    $("#Loading").modal({ backdrop: backdrop });
}

function HideLoading(callback)
{
    $("#Loading").off("hidden.bs.modal").on("hidden.bs.modal", function (e)
    {
        if (callback != null)
            callback();
    });
    $("#Loading").modal('hide');
}



//CONFIRM CONTROL FUNCTIONS
function ShowMessage(textHeader, textBody, type, confirmCallBack, cancelCallBack, backdrop)
{
    //header and body text
    $("#MessageHeader").text(textHeader);
    $("#MessageBody").text(textBody);

    $("#MessageGroup").children().hide();
    $("#MessageOk").show();

    //setting Image and header color
    if (type == 'success')
    {
        $("#MessageContent").attr("class", 'modal-content panel panel-success');
        $("#MessageImage").attr("src", '/Images/success.png');
        $("#MessageOk").attr('class','btn btn-success');
    }
    else if (type == 'warning')
    {
        $("#MessageContent").attr("class", 'modal-content panel panel-warning');
        $("#MessageImage").attr("src", '/Images/warning.png');
        $("#MessageOk").attr('class', 'btn btn-warning');
    }
    else if (type == 'confirm')
    {
        $("#MessageContent").attr("class", 'modal-content panel panel-info');
        $("#MessageImage").attr("src", '/Images/question.png');
        $("#MessageGroup").children().show();
        $("#MessageOk").hide();
    }


    //binding button acctions
    $("#MessageConfirm").unbind('click').click(function (e)
    {
        HideMessage(true, function ()
        {
            ShowLoading('static');

            if (confirmCallBack != null)
                confirmCallBack();
        });
    });

    $("#MessageCancel").unbind('click').click(function (e)
    {
        HideMessage(true, cancelCallBack);
    });

    $("#MessageOk").unbind('click').click(function (e)
    {

        HideMessage(true, cancelCallBack);
    });

    //rise modal
    $("#ModalMessage").modal({ backdrop: backdrop });
}

function HideMessage(cleaUp, callback)
{
    $('#ModalMessage').off('hidden.bs.modal').on('hidden.bs.modal', function (e)
    {
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

