$(document).ready(function () {
    var initialProvinciaId = document.getElementById("Prov").value;
    var initialCantonId = document.getElementById("Can").value;
    var initialDistritoId = document.getElementById("Dis").value;

    // Convertir los valores a números si es necesario
    initialProvinciaId = initialProvinciaId ? parseInt(initialProvinciaId, 10) : null;
    initialCantonId = initialCantonId ? parseInt(initialCantonId, 10) : null;
    initialDistritoId = initialDistritoId ? parseInt(initialDistritoId, 10) : null;

    loadProvincias();

    function loadProvincias() {
        $.ajax({
            url: '/CargarCombos/GetProvincias',
            type: 'GET',
            success: function (data) {
                console.log('Datos de provincias:', data);
                var provinciasSelect = $('#provincias');
                provinciasSelect.empty();
                provinciasSelect.append('<option value="">Seleccione una provincia</option>');

                // Ordenar las provincias alfabéticamente
                data.sort(function (a, b) {
                    return a.nombre.localeCompare(b.nombre);
                });

                $.each(data, function (index, provincia) {
                    provinciasSelect.append('<option value="' + provincia.idProvincia + '">' + provincia.nombre + '</option>');
                });

                if (initialProvinciaId) {
                    provinciasSelect.val(initialProvinciaId).trigger('change');
                }
            },
            error: function () {
                alert('Error al cargar las provincias.');
            }
        });
    }

    $('#provincias').change(function () {
        var provinciaId = $(this).val();
        if (provinciaId) {
            $.ajax({
                url: '/CargarCombos/GetCantones',
                type: 'GET',
                data: { provinciaId: provinciaId },
                success: function (data) {
                    var cantonesSelect = $('#cantones');
                    cantonesSelect.empty();
                    cantonesSelect.append('<option value="">Seleccione un cantón</option>');

                    // Ordenar los cantones alfabéticamente
                    data.sort(function (a, b) {
                        return a.nombre.localeCompare(b.nombre);
                    });

                    $.each(data, function (index, canton) {
                        cantonesSelect.append('<option value="' + canton.idCanton + '">' + canton.nombre + '</option>');
                    });

                    if (initialCantonId) {
                        cantonesSelect.val(initialCantonId).trigger('change');
                    }
                },
                error: function () {
                    alert('Error al cargar los cantones.');
                }
            });
        } else {
            $('#cantones').empty().append('<option value="">Seleccione un cantón</option>');
            $('#distritos').empty().append('<option value="">Seleccione un distrito</option>');
        }
    });

    $('#cantones').change(function () {
        var cantonId = $(this).val();
        if (cantonId) {
            $.ajax({
                url: '/CargarCombos/GetDistritos',
                type: 'GET',
                data: { cantonId: cantonId },
                success: function (data) {
                    var distritosSelect = $('#distritos');
                    distritosSelect.empty();
                    distritosSelect.append('<option value="">Seleccione un distrito</option>');

                    // Ordenar los distritos alfabéticamente
                    data.sort(function (a, b) {
                        return a.nombre.localeCompare(b.nombre);
                    });

                    $.each(data, function (index, distrito) {
                        distritosSelect.append('<option value="' + distrito.idDistrito + '">' + distrito.nombre + '</option>');
                    });

                    if (initialDistritoId) {
                        distritosSelect.val(initialDistritoId);
                    }
                },
                error: function () {
                    alert('Error al cargar los distritos.');
                }
            });
        } else {
            $('#distritos').empty().append('<option value="">Seleccione un distrito</option>');
        }
    });

    $('#Productos').change(function () {
        var selectedOption = $(this).find('option:selected');
        if (selectedOption.val()) {
            var nombre = selectedOption.data('product-name');
            var descripcion = selectedOption.data('product-description');
            var presentacion = selectedOption.data('product-presentation');
            var cantidadPresentacion = selectedOption.data('product-quantity');
            var tipoEmpaque = selectedOption.data('product-package-type');
            var cantidadEmpaque = selectedOption.data('product-package-quantity');
            var precio = selectedOption.data('product-price');
            var productoId = selectedOption.val();

            var row = '<tr data-id="' + productoId + '">' +
                '<td>' + productoId + '</td>' +
                '<td>' + nombre + '</td>' +
                '<td>' + descripcion + '</td>' +
                '<td>' + presentacion + '</td>' +
                '<td>' + cantidadPresentacion + '</td>' +
                '<td>' + tipoEmpaque + '</td>' +
                '<td>' + cantidadEmpaque + '</td>' +
                '<td>' + precio + '</td>' +
                '<td><input type="checkbox" class="delete-checkbox" checked /></td>' +
                '</tr>';

            $('#productosTabla tbody').append(row);
            $('#Productos').val('');
            updateSelectedProducts();
        }
    });

    function updateSelectedProducts() {
        var selectedProducts = [];
        $('#productosTabla tbody tr').each(function () {
            var productId = $(this).data('id');
            selectedProducts.push(productId);
        });
        $('#selectedProducts').val(JSON.stringify(selectedProducts));
    }

    $('#productosTabla').on('click', '.delete-checkbox', function () {
        $(this).closest('tr').remove();
        updateSelectedProducts();
    });
});
