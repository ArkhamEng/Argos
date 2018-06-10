//Muestra la ventana de Creación o Edición (si se manda un Id) de clientes
function ShowClientModal(OnCompleate, CloseCallBack, id)
{
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddClient/";
    var text = '<span class="fa fa-users"></span> Agregar nuevo cliente';

    if (parseInt(id) > 0) {
        param = { id: id }
        url = "/Catalog/BeginUpdateClient/";
        text = '<span class="fa fa-users"></span> Editar datos del cliente';
    }

    ExecuteAjax(url, param, function (response)
    {
        HideLoading(function () {
            if (!$.isPlainObject(response))
            {
                $("#divCatalogModal").html(response);

                //coloco el header
                $("#EditClientHeader").html(text);

                //oculto el loading de la modal
                $("#EditClientLoading").children().hide();

                //evento del boton save
                $("#EditClientSave").off('click').click(function (e) {
                    SaveClient(OnCompleate);
                });

                //evento del boton cancel
                $("#EditClientCancel").off('click').click(function (e) {
                    $('#EditClientModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(id) > 0) {
                            ExecuteAjax('/Catalog/UnLockClient/', { id: id }, function (response) {
                                //no hago nada
                            });
                        }

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


function SaveClient(OnCompleate) {
    var form = $("#ClientForm");

    if (!form.valid())
        return;

    //oculto temporalmente el contenido de la modal
    $("#EditClientContent").hide();

    //hago visible el gift loading
    $("#EditClientLoading").show();

    var client = {
        Name: $("#cliName").val(), BusinessName: $("#cliBusinessName").val(), FTR: $("#cliFTR").val(), Street: $("#cliStreet").val(),
        InNumber: $("#cliInNumber").val(), OutNumber: $("#cliOutNumber").val(), Location: $("#cliLocation").val(), Email: $("#cliEmail").val(),
        ZipCode: $("#cliZipCode").val(), Phone: $("#cliPhone").val(), PersonId: $("#cliClientId").val(), TownId: $("#cliCityId").val()
    };
    console.log(client);
    //URL para agregar
    var url = "/Catalog/AddClient/";

    //URL para actualización
    if (parseInt(client.PersonId) > 0)
        url = "/Catalog/UpdateClient/";


    ExecuteAjax(url, { client: client }, function (response) {
        if (OnCompleate != null)
            OnCompleate(response.Id);

        $('#EditClientModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
            //muestro mensaje en basa al response, al ocultar la modal
            ShowMessage(response.Header, response.Body, response.Result, null, function () {
                //si no se completa la operación vuelvo a sacar la modal
                if (response.Result != 'success')
                    $("#EditClientModal").modal({ backdrop: 'static' });
                    //de lo contrario la elimino por completo del html
                else
                    $("#divCatalogModal").html("");

            }, 'static');
        });

        $("#EditClientModal").modal("hide");
    });
}


//Muestra la ventana de Creación o Edición (si se manda un Id) de empleados
function ShowEmployeeModal(OnCompleate, CloseCallBack, id) {
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddEmployee/";
    var text = '<span class="fa fa-black-tie"></span> Agregar nuevo empleado';

    if (parseInt(id) > 0) {
        param = { id: id }
        url = "/Catalog/BeginUpdateEmployee/";
        text = '<span class="fa fa-black-tie"></span> Editar datos del empleado';
    }

    ExecuteAjax(url, param, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                $("#divCatalogModal").html(response);

                //coloco el header
                $("#EditEmployeeHeader").html(text);

                //oculto el loading de la modal
                $("#EditEmployeeLoading").children().hide();

                //evento del boton save
                $("#EditEmployeeSave").off('click').click(function (e) {
                    SaveEmployee(OnCompleate);
                });

                //evento del boton cancel
                $("#EditEmployeeCancel").off('click').click(function (e) {
                    $('#EditEmployeeModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(id) > 0) {
                            ExecuteAjax('/Catalog/UnLockPerson/', { id: id }, function (response) {
                                //no hago nada
                            });
                        }

                        //si hay callback al cerrar lo ejecuto
                        if (CloseCallBack != null)
                            CloseCallBack();

                        //elimino la modal del HTML
                        $("#divCatalogModal").html("");
                    });

                    //cierro la modal
                    $("#EditEmployeeModal").modal('hide');
                });

                //muestro la modal
                $("#EditEmployeeModal").modal({ backdrop: 'static' });

            }
            else
                ShowMessage(response.Result, response.Message, null, null, 'static');
        });
    });
}

function SaveEmployee(OnCompleate) {
    var form = $("#EmployeeForm");

    if (!form.valid())
        return;

    //oculto temporalmente el contenido de la modal
    $("#EditEmployeeContent").hide();

    //hago visible el gift loading
    $("#EditEmployeeLoading").show();

    var employee = {
        Name: $("#empName").val(), JobPositionId: $("#empJobPositionId").val(), FTR: $("#empFTR").val(), Street: $("#empStreet").val(),
        Commission: $("#empCommission").val(), InNumber: $("#empInNumber").val(), OutNumber: $("#empOutNumber").val(), Location: $("#empLocation").val(),
        Email: $("#empEmail").val(), SSN: $("#empSSN").val(), ZipCode: $("#empZipCode").val(), Phone: $("#empPhone").val(), BirthDate: $("#empBirthDate").val(),
        PersonId: $("#empEmployeeId").val(), TownId: $("#empCityId").val(), Gender: $("input[name='Entity.Gender']:checked").val(), HireDate: $("#empHireDate").val()
    };

    //URL para agregar
    var url = "/Catalog/AddEmployee/";

    //URL para actualización
    if (parseInt(employee.PersonId) > 0)
        url = "/Catalog/UpdateEmployee/";


    ExecuteAjax(url, { employee: employee }, function (response) {
        if (OnCompleate != null)
            OnCompleate(response.Id);

        $('#EditEmployeeModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
            //muestro mensaje en basa al response, al ocultar la modal
            ShowMessage(response.Header, response.Body, response.Result, null, function () {
                //si no se completa la operación vuelvo a sacar la modal
                if (response.Result != 'success')
                    $("#EditEmployeeModal").modal({ backdrop: 'static' });
                    //de lo contrario la elimino por completo del html
                else
                    $("#divCatalogModal").html("");

            }, 'static');
        });

        $("#EditEmployeeModal").modal("hide");
    });
}

//Muestra la ventana de Creación o Edición (si se manda un Id) de proveedores
function ShowSupplierModal(OnCompleate, CloseCallBack, id) {
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddSupplier/";
    var text = '<span class="fa fa-handshake-o"></span> Agregar nuevo proveedor';

    if (parseInt(id) > 0) {
        param = { id: id }
        url = "/Catalog/BeginUpdateSupplier/";
        text = '<span class="fa fa-handshake-o"></span> Editar datos del proveedor';
    }

    ExecuteAjax(url, param, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                $("#divCatalogModal").html(response);

                //coloco el header
                $("#EditSupplierHeader").html(text);

                //oculto el loading de la modal
                $("#EditSupplierLoading").children().hide();

                //evento del boton save
                $("#EditSupplierSave").unbind('click').click(function (e) {
                    SaveSupplier(OnCompleate);
                });

                //evento del boton cancel
                $("#EditSupplierCancel").unbind('click').click(function (e) {
                    $('#EditSupplierModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(id) > 0) {
                            ExecuteAjax('/Catalog/UnLockPerson/', { id: id }, function (response) {
                                //no hago nada
                            });
                        }
                        //si hay callback al cerrar lo ejecuto
                        if (CloseCallBack != null)
                            CloseCallBack();

                        //elimino la modal del HTML
                        $("#divCatalogModal").html("");
                    });

                    //cierro la modal
                    $("#EditSupplierModal").modal('hide');
                });

                //muestro la modal
                $("#EditSupplierModal").modal({ backdrop: 'static' });

            }
            else
                ShowMessage(response.Result, response.Message, null, null, 'static');
        });
    });
}


function SaveSupplier(OnCompleate) {
    var form = $("#SupplierForm");

    if (!form.valid())
        return;

    //oculto temporalmente el contenido de la modal
    $("#EditSupplierContent").hide();

    //hago visible el gift loading
    $("#EditSupplierLoading").show();

    var supplier = {
        Name: $("#supName").val(), BusinessName: $("#supBusinessName").val(), FTR: $("#supFTR").val(), Street: $("#supStreet").val(),
        InNumber: $("#supInNumber").val(), OutNumber: $("#supOutNumber").val(), Location: $("#supLocation").val(), Email: $("#supEmail").val(),
        ZipCode: $("#supZipCode").val(), Phone: $("#supPhone").val(), PersonId: $("#supSupplierId").val(), TownId: $("#supCityId").val(),
        WebSite: $("#supWebSite").val()
    }

    //URL para agregar
    var url = "/Catalog/AddSupplier/";

    //URL para actualización
    if (parseInt(supplier.PersonId) > 0)
        url = "/Catalog/UpdateSupplier/";


    ExecuteAjax(url, { supplier: supplier }, function (response) {
        if (OnCompleate != null)
            OnCompleate(response.Id);

        $('#EditSupplierModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
            //muestro mensaje en basa al response, al ocultar la modal
            ShowMessage(response.Header, response.Body, response.Result, null, function () {
                //si no se completa la operación vuelvo a sacar la modal
                if (response.Result != 'success')
                    $("#EditSupplierModal").modal({ backdrop: 'static' });
                    //de lo contrario la elimino por completo del html
                else
                    $("#divCatalogModal").html("");

            }, 'static');
        });

        $("#EditSupplierModal").modal("hide");
    });
}


//MUESTRA LA VENTANA DE CAPTURA DE PRODUCTO
function ShowProductModal(OnCompleate, CloseCallBack, id)
{
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddProduct/";
    var text = '<span class="fa fa-users"></span> Agregar nuevo Producto';

    if (parseInt(id) > 0) {
        param = { id: id }
        url = "/Catalog/BeginUpdateProduct/";
        text = '<span class="fa fa-users"></span> Editar datos del Producto';
    }

    ExecuteAjax(url, param, function (response)
    {
        HideLoading(function ()
        {
            if (!$.isPlainObject(response))
            {
                $("#divCatalogModal").html(response);

                SubmitProduct(OnCompleate);

                //coloco el header
                $("#EditProductHeader").html(text);

                //oculto el loading de la modal
                $("#EditProductLoading").children().hide();


                //evento del boton cancel
                $("#EditProductCancel").off('click').click(function (e)
                {
                    $('#EditProductModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e)
                    {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(id) > 0)
                        {
                            ExecuteAjax('/Catalog/UnLockProduct/', { id: id }, function (response)
                            {
                                if (response.Result != 'success')
                                    alert("Ocurrio un error al desbloquear el producto");
                            });
                        }

                        //si hay callback al cerrar lo ejecuto
                        if (CloseCallBack != null)
                            CloseCallBack();

                        //elimino la modal del HTML
                        $("#divCatalogModal").html("");
                    });

                    //cierro la modal
                    $("#EditProductModal").modal('hide');
                });

                //muestro la modal
                $("#EditProductModal").modal({ backdrop: 'static' });

            }
            else
                ShowMessage(response.Result, response.Message, null, null, 'static');
        });
    });

    function SubmitProduct(SuccessCallBack)
    {
        $("#frmProducts").off('submit').on('submit', function (e)
        {
            e.preventDefault();

            var $form = $(e.target),
            formData = new FormData(),
            params = $form.serializeArray(),
            files = [];

            $('#tbImages tr').each(function (index, row)
            {
                var input = $(row).find('[type="file"]');

                if (input[0] != undefined)
                {
                    var f = input[0].files[0];
                    files.push(f);
                }
            });

            //ya que el tab container omite la validación del tab q no se muestra
            //checo el tab activo y si es necesario habilito el que contine los campos a validar
            if (needActivate)
            {
                $("#general").addClass("active");

                if (!$form.valid())
                {
                    $("#general").removeClass("active");
                    ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica");
                    return;
                }
            }
            
            if (!$form.valid())
            {
                ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica",false);
                return;
            }
                

            $("#EditProductContent").hide();
            $("#EditProductLoading").children().show();

            //agrego todos los campos del formulario
            $.each(params, function (i, val)
            {
                formData.append(val.name, val.value);
            });

            //agrego las imagenes nuevas
            $.each(files, function (i, file)
            {
                formData.append('NewImages[' + i + ']', file);
            });

            //agrego los id, de las imagenes a borrar
            $.each(ToDelete, function (i, id)
            {
                formData.append('ToDelete[' + i + ']', id);
            });

            $.ajax({
                url: $form.attr('action'),
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (result)
                {
                    $("#EditProductModal").off("hidden.bs.modal").on("hidden.bs.modal", function ()
                    {
                        SuccessCallBack(result);
                    });
                    $("#EditProductModal").modal('hide');
                }
            });
        });
    }
}