

//Muestra la ventada emergente de Edición /Captura de catalogos, dependiendo del 
//tipo dado en el parametro Entity (Client,Employee,Supplier, Product, etc), si se envía un disable call back
//se considerara que la ventana no esta en modo de edición
function ShowCatalogModal(OnCompleate, CloseCallBack, Entity, id, disableCallBack) {
    ShowLoading('static');

    var param = {};
    var url = "/Catalog/BeginAdd" + Entity;

    if (id > 0) {
        param = { id: id };
        url = "/Catalog/BeginUpdate" + Entity;
    }
    var form = "#" + Entity + "Form";


    var unlockUrl = "/Catalog/UnLockPerson/";

    switch (Entity) {
        case "Product":
            unlockUrl = "/Catalog/UnLockProduct/";
            break;
    }



    ExecuteAjax(url, param, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                ShowModal(response, 'static', 'lg');

                //si se recibe un id
                if (typeof (param.id) != 'undefined')
                {
                    //si se recibe un callback de deshabilitación.. entro a modo visualización
                    if (typeof (disableCallBack) != 'undefined')
                        disableCallBack();
                        //de lo contrario, entro a modo edición y bloqueo el registro
                    else
                    {
                        ShowNotify("Registro bloqueado!", "warning", "Dispones de 5 min para realizar cambios, sobre este registro");
                    }
                }

                if (Entity == "Product")
                    SubmitProduct(OnCompleate);
                else
                    SubmitPerson(OnCompleate, form);

                //evento del boton cancel
                $("#EditCancel").off('click').click(function (e) {
                    HideModal(function () {
                        //si el se tiene un id y no hay callback de deshabilitación remuevo el bloqueo
                        if (parseInt(param.id) > 0 && typeof (disableCallBack) == 'undefined') {
                            ExecuteAjax(unlockUrl, { id: param.id }, function (response) {
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

function SubmitPerson(SuccessCallBack, form) {
    $(form).off('submit').on('submit', function (e) {
        e.preventDefault();

        var $form = $(e.target),
        formData = new FormData(),
        params = $form.serializeArray(),
        files = [],
        addresses = [];


        if (!$form.valid()) {
            ShowNotify("Error de validación", "danger", "Existen errores en lo datos capturados, por favor verifica", 3500);
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

        //agrego las direcciones a borrar
        $.each(DroppedAddress, function (index, a) {
            formData.append('DroppedAddress[' + index + ']', a);
        });

        //agrego los telefonos a borrar
        $.each(DroppedPhones, function (i, phone) {
            formData.append('DroppedPhones[' + i + ']', phone);
        });

        //agrego los emails a borrar
        $.each(DroppedMails, function (i, mail) {
            formData.append('DroppedMails[' + i + ']', mail);
        });

        var addressCount = 0;
        var newAddress = 0;
        //agrego las direcciones nueva
        $("#tbAddress tbody tr").each(function (index, row) {
            var entity = $(row).find('[id="item_EntityId"]').val();

            if (entity == 0) {
                formData.append('Person.Addresses[' + newAddress + '].AddressId', $(row).find('[id="item_AddressId"]').val());
                formData.append('Person.Addresses[' + newAddress + '].TownId', $(row).find('[id="item_TownId"]').val());
                formData.append('Person.Addresses[' + newAddress + '].ZipCode', $(row).find('[id="item_ZipCode"]').val());
                formData.append('Person.Addresses[' + newAddress + '].Location', $(row).find('[id="item_Location"]').val());
                formData.append('Person.Addresses[' + newAddress + '].Street', $(row).find('[id="item_Street"]').val());
                formData.append('Person.Addresses[' + newAddress + '].AddressTypeId', $(row).find('[id="item_AddressTypeId"]').val());
                formData.append('Person.Addresses[' + newAddress + '].EntityId', $(row).find('[id="item_EntityId"]').val());

                newAddress++;
            }
            addressCount++;
        });


        var phoneCount = 0;
        var newPhone = 0;
        $("#tbPhones tbody tr").each(function (index, row) {
            var entity = $(row).find('[id="item_EntityId"]').val();

            if (isNaN(entity)) {
                formData.append('Person.PhoneNumbers[' + newPhone + '].PhoneTypeId', $(row).find('[id="item_PhoneTypeId"]').val());
                formData.append('Person.PhoneNumbers[' + newPhone + '].Phone', $(row).find('[id="item_Phone"]').val());
                formData.append('Person.PhoneNumbers[' + newPhone + '].EntityId', $(row).find('[id="item_EntityId"]').val());
                newPhone++;
            }
            phoneCount++;
        });

        var newMail = 0;
        var mailCount = 0;
        $("#tbEmails tbody tr").each(function (index, row) {
            var entity = parseInt($(row).find('[id="item_EntityId"]').val())

            if (isNaN(entity)) {
                formData.append('Person.EmailAddresses[' + newMail + '].EmailTypeId', $(row).find('[id="item_EmailTypeId"]').val());
                formData.append('Person.EmailAddresses[' + newMail + '].Email', $(row).find('[id="item_Email"]').val());
                formData.append('Person.EmailAddresses[' + newMail + '].EntityId', $(row).find('[id="item_EntityId"]').val());
                //conteo de nuevos registros
                newMail++;
            }
            //Conteo total
            mailCount++;
        });

        var allowSubmit = true;

        if (addressCount == 0) {
            ShowNotify("Dirección requerida <spans class='fa fa-street-view'/>", "warning", "Debes proporcionar al menos una dirección", 3500);
            allowSubmit = false;
        }

        if (phoneCount == 0) {
            ShowNotify("Teléfono requerido <spans class='fa fa-phone'/>", "warning", "Debes proporcionar al menos un teléfono", 3500);
            allowSubmit = false;
        }

        if (mailCount == 0) {
            ShowNotify(" Correo requerido <spans class='fa fa-envelope' />", "warning", "Debes proporcionar al menos un correo electrónico", 3500);
            allowSubmit = false;
        }

        if (!allowSubmit)
            return;


        ShowModLoading();

        $.ajax({
            url: $form.attr('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                HideModLoading();

                if ($.isPlainObject(response) && response.Code != 200) {
                    ShowNotify(response.Header, response.Result, response.Body, 3500);

                    switch (response.Code) {
                        case 401:
                            window.location = data.LogOnUrl;
                            break;
                    }
                }
                else {
                    HideModal(function () {
                        ShowNotify(response.Header, response.Result, response.Body);
                        SuccessCallBack(response.Id);
                    }, true);
                }
            },
            error: function () { HideModLoading(); }
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
            ShowNotify(e.name, "danger", m, "3500");
        });

        return false;
    }
    return true;
}


function SubmitProduct(SuccessCallBack) {
    $("#frmProducts").off('submit').on('submit', function (e) {
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
        if (needActivate) {
            $("#general").addClass("active");

            if (!$form.valid()) {
                $("#general").removeClass("active");
                ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica", 3500);
                return;
            }
        }

        if (!$form.valid()) {
            ShowNotify("Error de validación", "danger", "El formulario contiene errores, por favor verifica", false);
            return;
        }

        $("#EditContent").hide();
        $("#EditLoading").children().show();

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
        console.log("Llegue hasta aqui");
        $.ajax({
            url: $form.attr('action'),
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                console.log(response);

                if ($.isPlainObject(response) && typeof (response.Code) == 401) {
                    ShowNotify(response.Header, response.Result, response.Body, 4500);
                    window.location = response.LogOnUrl;
                }
                else {
                    HideModal(function () {
                        ShowNotify(response.Header, response.Result, response.Body);
                        SuccessCallBack(response.Id);
                    }, true);
                }
            }
        });
    });
}




function OnImageLoaded(input) {
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


function ShowAssingSupplier(OnSelected) {
    url = "/Catalog/GetSuppliers";

    ShowLoading('static');

    ExecuteAjax(url, {}, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                ShowModal(response, 'static', 'lg');

                $("#tbAssigSupplier tbody tr").each(function (index, row) {
                    $(row).find("#btnAssing").off("click").click(function () {
                        var id = $(row).find('[id="item_EntityId"]').val();
                        var name = $(row).find('[id="item_Name"]').val();

                        OnSelected(id, name);
                        HideModal(null, true);
                    })
                });
            }
        });
    });
}


function ShowAddProduct(OnSelected) {
    url = "/Catalog/GetProducts";

    ShowLoading('static');

    ExecuteAjax(url, {}, function (response) {
        HideLoading(function () {
            if (!$.isPlainObject(response)) {
                ShowModal(response, 'static', 'lg');

                $("#tbProducts tbody tr").each(function (index, row) {
                    $(row).find("#btnAssing").off("click").click(function () {

                        var product = {
                            ProductId: $(row).find('[id="item_ProductId"]').val(),
                            Code: $(row).find('[id="item_Code"]').val(),
                            Description: $(row).find('[id="item_Description"]').val(),
                            BuyPrice: $(row).find('[id="item_BuyPrice"]').val(),
                            Image: $(row).find("#item_Image").attr('src')
                        }
                        console.log(product);
                        $(row).addClass("success");
                        $(row).find("#btnAssing").addClass("disabled");

                        OnSelected(product);
                    })
                });
            }
        });
    });
}