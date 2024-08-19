$(document).ready(function () {
    // Variables iniciales para provincia, cantón y distrito
    var initialProvinciaId = parseInt($('#Prov').val(), 10) || null;
    var initialCantonId = parseInt($('#Can').val(), 10) || null;
    var initialDistritoId = parseInt($('#Dis').val(), 10) || null;

    // Cargar provincias al inicio
    loadProvincias();

    // Función para cargar provincias
    function loadProvincias() {
        $.ajax({
            url: '/CargarCombos/GetProvincias',
            type: 'GET',
            success: function (data) {
                var provinciasSelect = $('#provincias');
                provinciasSelect.empty();
                provinciasSelect.append('<option value="">Seleccione una provincia</option>');

                data.sort((a, b) => a.nombre.localeCompare(b.nombre));

                data.forEach(provincia => {
                    provinciasSelect.append(`<option value="${provincia.idProvincia}">${provincia.nombre}</option>`);
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

    // Cambio en la selección de provincia
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

                    data.sort((a, b) => a.nombre.localeCompare(b.nombre));

                    data.forEach(canton => {
                        cantonesSelect.append(`<option value="${canton.idCanton}">${canton.nombre}</option>`);
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

    // Cambio en la selección de cantón
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

                    data.sort((a, b) => a.nombre.localeCompare(b.nombre));

                    data.forEach(distrito => {
                        distritosSelect.append(`<option value="${distrito.idDistrito}">${distrito.nombre}</option>`);
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

});