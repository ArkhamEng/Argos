function ShowEditAccount(Container,OnCompleate,OnClose,id)
{
    ShowLoading('static');

    var param = {};
    var title = '<span class="fa fa-files-o"></span> Registro de nueva cuenta';
    var url   = '/Operative/BeginAccount/)';

    if (parseInt(id) > 0)
    {
        title = '<span class="fa fa-files-o"></span> Editar datos de la cuenta';
        url = '/Operative/BeginEditAccount/)';
        param = { id: id };
    }


    ExecuteAjax(url, param, function (response)
    {
        HideLoading(function ()
        {
            $(Container).html(response);

            $("#EditAccountTitle").html(title);

            $('#btnEditAccountAccept').off('click').click(function ()
            {
                AcceptCreateAccount(OnCompleate, id);
            });

            $("#btnEditAccountCancel").off('click').click(function ()
            {
                $("#ModalBeginAccount").off("hidden.bs.modal").on("hidden.bs.modal", function ()
                {
                    if (OnClose != null)
                        OnClose();

                    $(Container).html('');
                });
               
                $("#ModalBeginAccount").modal('hide');
            });

            $("#ModalBeginAccount").modal({ backdrop: 'static' });
        });
    });
}

function AcceptCreateAccount(OnCompleate, id)
{
    var form = $("#frmBeginAccount");

    if (!form.valid())
        return;

    var url = "/Operative/CreateAccount";

    $("#ModalBeginAccount").off("hidden.bs.modal").on("hidden.bs.modal", function ()
    {
        $("#ModalBeginAccount").off("hidden.bs.modal");

        ShowLoading('static');

        var account =
            {
                ClientId: $("#ClientId").val(),
                HireDate: $("#SaleDate").val(),
                AccountTypeId: $("#AccountTypeId").val(),
                HirePrice: $("#HirePrice").val()
            };

        ExecuteAjax(url, { model: account }, function (response)
        {
            if (response.Result != "success")
            {
                HideLoading(function ()
                {
                    ShowMessage(response.Header, response.Message, 'warning', null, null, 'static');
                });
            }
            else
            {
                if (OnCompleate != null)
                    OnCompleate(response.Id);
            }
        });
    });

    $("#ModalBeginAccount").modal('hide');
}