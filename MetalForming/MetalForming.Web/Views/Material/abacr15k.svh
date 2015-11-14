@model ProgramaModel

@{
    ViewBag.MenuActual = ConstantesWeb.Menu.Mantenimiento;
    ViewBag.MenuItemActual = ConstantesWeb.MenuItem.MantenerMaterial;
}

@section styles{
    <style type="text/css">
        .well {
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 10px;
            padding-right: 10px;
            margin-bottom: 10px;
        }

        .well-form {
            padding-top: 15px;
            padding-bottom: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
}

<header class="header-crumb">
    <div class="container">
        <h2>Mantener Material</h2>
    </div>
</header>

<main>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-xs-8">
                    <div class="table-responsive ">
                        <table id="tblMateriales" class="table table-striped table-header table-rounded">
                            <thead class="upper">
                                <tr>
                                    <th class="hide"></th>
                                    <th>
                                        <input id="txtBuscarCodigo" type="text" class="form-control buscarMateriales" placeholder="Código" />
                                    </th>
                                    <th>
                                        <input id="txtBuscarDescripcion" type="text" class="form-control buscarMateriales" placeholder="Descripción" />
                                    </th>
                                    <th>
                                        <div class="col-xs-4">
                                            <div class="row">
                                                <select id="cboBuscarCampo" class="form-control">
                                                    <option value="0"> </option>
                                                    <option value="1">Presion</option>
                                                    <option value="2">Ancho</option>
                                                    <option value="3">Largo</option>
                                                    <option value="4">Espesor</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="row">
                                                <input id="txtBuscarMin" type="text" class="form-control buscarMateriales" placeholder="Min." />
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="row">
                                                <input id="txtBuscarMax" type="text" class="form-control buscarMateriales" placeholder="Max." />
                                            </div>
                                        </div>
                                    </th>
                                    <th>
                                        <select id="cboBuscarEstado" class="form-control">
                                            <option value="1">Activo</option>
                                            <option value="0">Inactivo </option>
                                        </select>
                                    </th>
                                </tr>
                                <tr>
                                    <th class="hide"></th>
                                    <th>Código</th>
                                    <th>Descripción</th>
                                    <th>Pres/Anc/Lar/Esp</th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="well well-form">
                        <div class="form-horizontal">
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Código:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtCodigo" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Descripción:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtDescripcion" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Presión:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtPresion" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Ancho:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtAncho" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Largo:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtLargo" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Espesor:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtEspesor" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Stock Min:
                                </div>
                                <div class="col-xs-7">
                                    <input id="txtStockMinimo" class="form-control" value="0" />
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-5">
                                    Estado:
                                </div>
                                <div class="col-xs-7">
                                    <select id="cboEstado" class="form-control">
                                        <option value="Activo">Activo</option>
                                        <option value="Inactivo">Inactivo</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group form-group-sm">
                                <div class="col-xs-12 text-center">
                                    <a href="javascript:;" class="btn btn-primary-line" id="btnGuardar"> Crear</a>
                                    <a href="javascript:;" class="btn btn-primary-line" id="btnSalir"> Cancelar</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <br />
    <br />
    <br />
</main>


@section scripts{
    <script type="text/javascript">

        var urlGuardarMaterial = '@Url.Action("GuardarMaterial")';
        var urlBuscarMaterial = '@Url.Action("BuscarMaterial")';

        $(function () {

            BuscarMateriales();

            $("#cboBuscarEstado").on("change", function () {
                BuscarMateriales();
            });

            $(".buscarMateriales").on("keypress", function (e) {
                if (e.which == 13) {
                    BuscarMateriales();
                }
            });

            $("#btnGuardar").on("click", function () {
                if (Validaciones()) {
                    Guardar();
                }
            });

            $("#btnSalir").on("click", function () {
                Refrescar();
            });
        });

        function BuscarMateriales() {
            var parametros = {
                codigo: $("#txtBuscarCodigo").val(),
                descripcion: $("#txtBuscarDescripcion").val(),
                tipo: $("#cboBuscarCampo").val() == "" ? 0 : parseInt($("#cboBuscarCampo").val()),
                min: $("#txtBuscarMin").val() == "" ? -1 : parseInt($("#txtBuscarMin").val()),
                max: $("#txtBuscarMax").val() == "" ? 99999 : parseInt($("#txtBuscarMax").val()),
                estado: $("#cboBuscarEstado").val() == "" ? 0 : parseInt($("#cboBuscarEstado").val())
            };

            $.ajax({
                type: 'POST',
                url: urlBuscarMaterial,
                data: JSON.stringify(parametros),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    $("#tblMateriales tbody").html("");

                    if (response.Success == true) {
                        $.each(response.Data, function (index, item) {
                            var tr = "<tr>";
                            tr += "<td class=\"tdIdMaterial hide\">" + item.Id + "</td>";
                            tr += "<td>" + item.Codigo + "</td>";
                            tr += "<td>" + item.Descripcion + "</td>";
                            tr += "<td>" + item.Informacion + "</td>";
                            tr += "<td>" + item.Estado + "</td>";
                            tr += "</tr>";

                            $("#tblMateriales tbody").append(tr);
                        });
                    } else {
                        app.showMessageDialog(response.Message);
                    }
                },
                error: function (x, xh, xhr) {
                    console.error(xh);
                }
            });
        }

        function Validaciones() {
            var cantidadOV = parseInt($("#txtCantidadOV").val());
            if (cantidadOV == 0) {
                app.showMessageDialog("Debe seleccionar Ordenes de venta.");
                return false;
            }
            return true;
        }

        function Guardar() {
            var model = {
                Id: parseInt($("#hidIdPrograma").val()),
                Numero: $("#hidNumero").val(),
                Estado: $("#hidEstado").val(),
                FechaInicioStr: $("#txtFechaInicioPrograma").val(),
                FechaFinStr: $("#txtFechaFinPrograma").val(),
                CantidadOV: parseInt($("#txtCantidadOV").val()),
                IdPlan: parseInt($("#hidIdPlan").val()),
                OrdenesVenta: new Array()
            };
            $('#tblOrdenesVenta').find("tbody tr").each(function () {
                var seleccionado = $(this).find("td.tdSeleccionar input:enabled").is(':checked');
                if (seleccionado === true) {
                    var idOrdenVenta = parseInt($.trim($(this).find("td.tdIdOrdenVenta").text()));
                    model.OrdenesVenta.push({
                        Id: idOrdenVenta
                    });
                }
            });

            var mensajeConfirmacion = "";
            if (model.Id === 0) {
                mensajeConfirmacion = "Seguro de crear el programa.";
            } else {
                mensajeConfirmacion = "Seguro de modificar el programa vigente.";
            }

            app.showConfirmDialog(mensajeConfirmacion, function () {
                $.ajax({
                    type: 'POST',
                    url: urlGuardarPrograma,
                    data: JSON.stringify(model),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response.Success === true) {
                            var mensajeGrabadoCorrecto = "Se grabo el programa " + response.Data + ". En el periodo del " + model.FechaInicioStr + " al " + model.FechaFinStr;
                            app.showMessageDialog(mensajeGrabadoCorrecto, Refrescar);
                        } else {
                            app.showMessageDialog(response.Message);
                        }
                    },
                    error: function (x, xh, xhr) {
                        console.error(xh);
                    }
                });
            });
        }

        function Refrescar() {
            window.location.reload();
        }

    </script>
}