//Muestra la ventana de Creación o Edición (si se manda un Id) de clientes
function ShowClientModal(OnCompleate, CloseCallBack,id)
{
    ShowLoading('static');

    var param = {};
    var url   = "/Catalog/BeginAddClient/";

    if (parseInt(id) > 0)
    {
        param  = { id: id }
        url = "/Catalog/BeginUpdateClient/";
    }
        
    ExecuteAjax(url, param, function (response)
    {
        HideLoading(function ()
        {
            if (!$.isPlainObject(response))
            {
                $("#divCatalogModal").html(response);

                //coloco el header
                $("#EditClientHeader").html('<span class="fa fa-user-o"></span> Agregar nuevo cliente');

                //oculto el loading de la modal
                $("#EditClientLoading").children().hide();

                //evento del boton save
                $("#EditClientSave").unbind('click').click(function (e) {
                    SaveClient(OnCompleate);
                });

                //evento del boton cancel
                $("#EditClientCancel").unbind('click').click(function (e) {
                    $('#EditClientModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                        //si hay callback al cerrar lo ejecuto
                        if (CloseCallBack != null)
                            CloseCallBack();

                        //elimino la modal del HTML
                        $("#divCatalogModal").html("");
                    });

                    //cierro la modal
                    $("#EditClientModal").modal('hide');
                });

                //muestro la modal
                $("#EditClientModal").modal({ backdrop: 'static' });

            }
            else
                ShowMessage(response.Result, response.Message, null, null, 'static');
        });
    });
}


function SaveClient(OnCompleate)
{
    var form = $("#ClientForm");

    if (!form.valid())
        return;

    //oculto temporalmente el contenido de la modal
    $("#EditClientContent").hide();

    //hago visible el gift loading
    $("#EditClientLoading").show();

    var client = {
        Name: $("#Name").val(), BusinessName: $("#BusinessName").val(), FTR: $("#FTR").val(), Street: $("#Street").val(),
        InNumber: $("#InNumber").val(), OutNumber: $("#OutNumber").val(), Location: $("#Location").val(), Email: $("#Email").val(),
        ZipCode: $("#ZipCode").val(), Phone: $("#Phone").val(), ClientId: $("#ClientId").val(), CityId: $("#CityId").val()
    }

    //URL para agregar
    var url = "/Catalog/AddClient/";

    //URL para actualización
    if (parseInt(client.ClientId) > 0)
        url = "/Catalog/UpdateClient/";


    ExecuteAjax(url, { client: client }, function (response)
    {
        if (OnCompleate != null)
            OnCompleate(response.Id);

        $('#EditClientModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e)
        {
            //muestro mensaje en basa al response, al ocultar la modal
            ShowMessage(response.Header, response.Body, response.Result, null, function ()
            {
                //si no se completa la operación vuelvo a sacar la modal
                if(response.Result != 'success')
                    $("#EditClientModal").modal({ backdrop: 'static' });
                //de lo contrario la elimino por completo del html
                else                    
                    $("#divCatalogModal").html("");

            }, 'static');
        });

        $("#EditClientModal").modal("hide");
    });
}
