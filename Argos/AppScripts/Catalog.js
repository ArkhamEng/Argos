function ShowPersonModal(url, param, OnCompleate, CloseCallBack, SubmitFunction)
{
    ExecuteAjax(url, param, function (response)
    {
        HideLoading(function ()
        {
            if (!$.isPlainObject(response))
            {
                ShowModal(response, 'static', 'lg');

                if (param.id != 'undefidend')
                    ShowNotify("Registro bloqueado!", "info", "Dispones de 5 min para realizar cambios, sobre este registro");

                SubmitFunction(OnCompleate);

                //evento del boton cancel
                $("#EditCancel").off('click').click(function (e)
                {
                    HideModal(function ()
                    {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(param.id) > 0)
                        {
                            ExecuteAjax('/Catalog/UnLockPerson/', { id: param.id }, function (response)
                            {
                                ShowNotify(response.Header, response.Result, response.Body);
                            });
                        }
                        //si hay callback al cerrar lo ejecuto
                        if (CloseCallBack != null)
                            CloseCallBack();
                    }, true);
                });
            }
            else
                ShowNotify(response.Header, response.Result, response.Body, 4500);
        });
    });
}


function SubmitProduct(SuccessCallBack)
{
    $("#frmProducts").off('submit').on('submit', function (e)
    {
        e.preventDefault();

        var $form = $(e.target),
        formData = new FormData(),
        params = $form.serializeArray(),
        files = [];

        $('#tbImages tr').each(function (index, row) {
            var input = $(row).find('[type="file"]');

            if (input[0] != undefined) {
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
                ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica",3500);
                return;
            }
        }

        if (!$form.valid())
        {
            ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica", false);
            return;
        }

        $("#EditProductContent").hide();
        $("#EditProductLoading").children().show();

        //agrego todos los campos del formulario
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });

        //agrego las imagenes nuevas
        $.each(files, function (i, file) {
            formData.append('NewImages[' + i + ']', file);
        });

        //agrego los id, de las imagenes a borrar
        $.each(ToDelete, function (i, id) {
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
                if ($.isPlainObject(result) && typeof (result.Code) == 401)
                {
                    ShowNotify(result.Header, result.Result, result.Body, 4500);
                    window.location = result.LogOnUrl;
                }
                else
                {
                    $("#EditProductModal").off("hidden.bs.modal").on("hidden.bs.modal", function ()
                    {
                        ShowNotify(result.Header, result.Result, result.Body);
                        SuccessCallBack(result);
                    });
                    $("#EditProductModal").modal('hide');
                }
            }
        });
    });
}

function SubmitClient(SuccessCallBack)
{
    $("#ClientForm").off('submit').on('submit', function (e)
    {
        e.preventDefault();

        var $form = $(e.target),
        formData = new FormData(),
        params = $form.serializeArray(),
        files = [],
        addresses = [];

        //validaciones
        if (!ValidateAddress($form))
            return;

        if (!$form.valid())
        {
            ShowNotify("Error de validación", "warning", "El formulario contiene errores, por favor verifica", 3500);
            return;
        }

        input = $form.find('[type="file"]')[0];

        if (typeof (input) != "undefined")
        {
            files.push(input.files[0]);
        }

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

        $("#tbAddress tr").each(function (index, row) {

            formData.append('Client.Addresses[' + index + '].AddressId', $(row).find('[id="item_Address_AddressId"]').val());
            formData.append('Client.Addresses[' + index + '].TownId', $(row).find('[id="item_Address_TownId"]').val());
            formData.append('Client.Addresses[' + index + '].ZipCode', $(row).find('[id="item_Address_ZipCode"]').val());
            formData.append('Client.Addresses[' + index + '].Location', $(row).find('[id="item_Address_Location"]').val());
            formData.append('Client.Addresses[' + index + '].Street', $(row).find('[id="item_Address_Street"]').val());
            formData.append('Client.Addresses[' + index + '].AddressTypeId', $(row).find('[id="item_Address_AddressTypeId"]').val());
            formData.append('Client.Addresses[' + index + '].PersonId', $(row).find('[id="item_Address_PersonId"]').val());
        });

        ShowModLoading();

        $.ajax({
            url: $form.attr('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response)
            {
                HideModLoading();

                if ($.isPlainObject(response) && response.Code != 200)
                {
                    ShowNotify(response.Header, response.Result, response.Body, 3500);

                    switch (response.Code)
                    {
                        case 401:
                            window.location = data.LogOnUrl;
                            break;
                    }
                }
                else
                {
                    HideModal(function ()
                    {
                        ShowNotify(response.Header, response.Result, response.Body);
                        SuccessCallBack(response);
                    }, true);
                }
            }
        });
    });
}

function SubmitEmployee(SuccessCallBack)
{
    $("#EmployeeForm").off('submit').on('submit', function (e)
    {
        e.preventDefault();

        var $form = $(e.target),
        formData = new FormData(),
        params = $form.serializeArray(),
        files = [],
        addresses = [];

        //validacion de las direcciones
        if (!ValidateAddress($form))
            return;

        if (!$form.valid()) {
            ShowNotify("Error de validación", "warning", "Algunos de los datos del empleado son incorrectos, por favor verifica", 3500);
            return;
        }

        input = $form.find('[type="file"]')[0];

        if (typeof (input) != "undefined") {
            files.push(input.files[0]);
        }

        //agrego todos los campos del formulario
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });

        //agrego las imagenes nuevas
        $.each(files, function (i, file)
        {
            formData.append('NewImages[' + i + ']', file);
        });

        $("#tbAddress tr").each(function (index, row) {
            formData.append('Employee.Addresses[' + index + '].AddressId', $(row).find('[id="item_Address_AddressId"]').val());
            formData.append('Employee.Addresses[' + index + '].TownId', $(row).find('[id="item_Address_TownId"]').val());
            formData.append('Employee.Addresses[' + index + '].ZipCode', $(row).find('[id="item_Address_ZipCode"]').val());
            formData.append('Employee.Addresses[' + index + '].Location', $(row).find('[id="item_Address_Location"]').val());
            formData.append('Employee.Addresses[' + index + '].Street', $(row).find('[id="item_Address_Street"]').val());
            formData.append('Employee.Addresses[' + index + '].AddressTypeId', $(row).find('[id="item_Address_AddressTypeId"]').val());
            formData.append('Employee.Addresses[' + index + '].PersonId', $(row).find('[id="item_Address_PersonId"]').val());
        });

        ShowModLoading();

        $.ajax({
            url: $form.attr('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                if ($.isPlainObject(response) && response.Code != 200)
                {
                    HideModLoading();

                    ShowNotify(response.Header, response.Result, response.Body, 3500);

                    switch (response.Code)
                    {
                        case 401:
                            window.location = data.LogOnUrl;
                            break;
                    }
                }
                else
                {
                    HideModal(function ()
                    {
                        ShowNotify(response.Header, response.Result, response.Body);
                        SuccessCallBack(response);
                    }, true);
                }
            }
        });
    });
}

function SubmitSupplier(SuccessCallBack)
{
    $("#SupplierForm").off('submit').on('submit', function (e)
    {
        e.preventDefault();

        var $form = $(e.target),
        formData = new FormData(),
        params = $form.serializeArray(),
        files = [],
        addresses = [];

        //validacion de las direcciones
        if (!ValidateAddress($form))
            return;

        if (!$form.valid()) {
            ShowNotify("Error de validación", "warning", "Algunos de los datos del empleado son incorrectos, por favor verifica", 3500);
            return;
        }

        input = $form.find('[type="file"]')[0];

        if (typeof (input) != "undefined") {
            files.push(input.files[0]);
        }

        //agrego todos los campos del formulario
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });

        //agrego las imagenes nuevas
        $.each(files, function (i, file) {
            formData.append('NewImages[' + i + ']', file);
        });

        $("#tbAddress tr").each(function (index, row) {
            formData.append('Supplier.Addresses[' + index + '].AddressId', $(row).find('[id="item_Address_AddressId"]').val());
            formData.append('Supplier.Addresses[' + index + '].TownId', $(row).find('[id="item_Address_TownId"]').val());
            formData.append('Supplier.Addresses[' + index + '].ZipCode', $(row).find('[id="item_Address_ZipCode"]').val());
            formData.append('Supplier.Addresses[' + index + '].Location', $(row).find('[id="item_Address_Location"]').val());
            formData.append('Supplier.Addresses[' + index + '].Street', $(row).find('[id="item_Address_Street"]').val());
            formData.append('Supplier.Addresses[' + index + '].AddressTypeId', $(row).find('[id="item_Address_AddressTypeId"]').val());
            formData.append('Supplier.Addresses[' + index + '].PersonId', $(row).find('[id="item_Address_PersonId"]').val());
        });

        $("#EditSupplierContent").hide();
        $("#EditSupplierLoading").children().show();

        $.ajax({
            url: $form.attr('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) 
            {
                if ($.isPlainObject(response) && response.Code != 200)
                {
                    $("#EditSupplierLoading").hide();
                    $("#EditSupplierContent").show();

                    ShowNotify(response.Header, response.Result, response.Body, 3500);

                    switch (response.Code) {
                        case 401:
                            window.location = data.LogOnUrl;
                            break;
                    }
                }
                else
                {
                    $("#EditSupplierModal").off("hidden.bs.modal").on("hidden.bs.modal", function ()
                    {
                        ShowNotify(response.Header, response.Result, response.Body, 3500);
                        SuccessCallBack(response);
                    }).modal('hide');
                }
            }
        });
    });
}

function ValidateAddress($form) {
    var err = [];
    $('#tbAddress tr').each(function (index, row) {
        var messages = [];
        var name = "Dirección " + (index + 1);

        var town = $(row).find('[id="item_Address_TownId"]');
        var zipCode = $(row).find('[id="item_Address_ZipCode"]');
        var location = $(row).find('[id="item_Address_Location"]');
        var street = $(row).find('[id="item_Address_Street"]');
        var addressType = $(row).find('[id="item_Address_AddressTypeId"]');


        if (!$form.data('validator').element(town))
            messages.push("El municipio es requerido");

        if (!$form.data('validator').element(zipCode))
            messages.push("El CP es requerido");

        if (!$form.data('validator').element(location))
            messages.push("La localidad es requerida");

        if (!$form.data('validator').element(street))
            messages.push("La calle y numero son requeridos");

        if (!$form.data('validator').element(addressType))
            messages.push("El tipo de dirección es requerido");

        if (messages.length > 0)
            err.push({ name: name, Messages: messages });
    });

    if (err.length > 0) {
        $.each(err, function (index, e) {
            var m = "";
            $.each(e.Messages, function (i, msj) {
                console.log(msj);
                m = m + "<a>" + msj + "</a></br>";
            });
            ShowNotify(e.name, "warning", m, "3500");
        });

        return false;
    }
    return true;
}

//Muestra la ventana de Creación o Edición (si se manda un Id) de clientes
function ShowClientModal(OnCompleate, CloseCallBack, id)
{
    ShowLoading('static');
    
    var param = {};
    var url = "/Catalog/BeginAddClient";

    if (id > 0)
    {
        param = { id: id };
        url = "/Catalog/BeginUpdateClient"
    }

    ShowPersonModal(url, param, OnCompleate, CloseCallBack, SubmitClient);
}

//Muestra la ventana de Creación o Edición (si se manda un Id) de empleados
function ShowEmployeeModal(OnCompleate, CloseCallBack, id)
{
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddEmployee";

    if (id > 0) {
        param = { id: id };
        url = "/Catalog/BeginUpdateEmployee"
    }

    ShowPersonModal(url, param, OnCompleate,CloseCallBack, SubmitEmployee);
}


//Muestra la ventana de Creación o Edición (si se manda un Id) de proveedores
function ShowSupplierModal(OnCompleate, CloseCallBack, id)
{
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddSupplier";

    if (id > 0) {
        param = { id: id };
        url = "/Catalog/BeginUpdateSupplier"
    }

    ShowPersonModal(url, param, OnCompleate, CloseCallBack, SubmitSupplier);
}


//MUESTRA LA VENTANA DE CAPTURA DE PRODUCTO
function ShowProductModal(OnCompleate, CloseCallBack, id) {
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAddProduct/";
    var text = '<span class="fa fa-users"></span> Agregar nuevo Producto';

    if (parseInt(id) > 0) {
        param = { id: id }
        url = "/Catalog/BeginUpdateProduct/";
        text = '<span class="fa fa-users"></span> Editar datos del Producto';
    }

    ExecuteAjax(url, param, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                $("#divCatalogModal").html(response);

                SubmitProduct(OnCompleate);

                //coloco el header
                $("#EditProductHeader").html(text);

                //oculto el loading de la modal
                $("#EditProductLoading").children().hide();


                //evento del boton cancel
                $("#EditProductCancel").off('click').click(function (e) {
                    $('#EditProductModal').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                        //si el se tiene un id, desbloqueo el registro para liberarlo
                        if (parseInt(id) > 0) {
                            ExecuteAjax('/Catalog/UnLockProduct/', { id: id }, function (response)
                            {
                                ShowNotify(response.Header, response.Result, response.Body);
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


}


function OnImageLoaded(input)
{
    var files = input.files,
    reader = new FileReader();

    reader.onload = function (e) {
        $("#btnDropImage").removeClass("disabled");
        $("#imgPerson").attr("src", e.target.result);
    }
    reader.readAsDataURL(files[0]);
}

function RemoveImage() {
    $("#DropImage").val(true);
    $("#btnDropImage").addClass("disabled");

    $("#fileUpl").val("");
    $("#imgPerson").attr("src", "../Images/sinimagen.jpg");
}
