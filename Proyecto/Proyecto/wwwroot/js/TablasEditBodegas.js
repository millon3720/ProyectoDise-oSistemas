$(document).ready(function () {
    function getCurrentDate() {
        var today = new Date();
        var day = ("0" + today.getDate()).slice(-2);
        var month = ("0" + (today.getMonth() + 1)).slice(-2);
        var year = today.getFullYear();
        return day + "-" + month + "-" + year; // YYYY-MM-DD
    }

    $('#AgregarProductoSelect').change(function () {
        var selectedOption = $(this).find('option:selected');
        var productoId = selectedOption.val();
        if (productoId) {
            var todayDate = getCurrentDate();
            if ($('#productosTabla tbody tr[data-id="' + productoId + '"]').length === 0) {
                var row = `
            <tr data-id="${productoId}">
                <td>${productoId}</td>
                <td>${selectedOption.data('product-name')}</td>
                <td>${selectedOption.data('product-description')}</td>
                <td>${selectedOption.data('product-presentation')}</td>
                <td>${selectedOption.data('product-quantity')}</td>
                <td>${selectedOption.data('product-package-type')}</td>
                <td>${selectedOption.data('product-package-quantity')}</td>
                <td><input type="date" name="products[${productoId}].FechaIngreso" value="${todayDate}" /></td>
                <td><input type="date" name="products[${productoId}].FechaVencimiento" value="${todayDate}" /></td>
                <td>${selectedOption.data('product-price')}</td>
                <td><input type="checkbox" class="delete-checkbox" checked /></td>
            </tr>`;
                $('#productosTabla tbody').append(row);
                $(this).val(''); // Limpiar selección
                updateProductsData(); // Actualizar datos de productos
            }
        }
    });

    $('#productosTabla').on('change', '.delete-checkbox', function () {
        $(this).closest('tr').remove();
        updateProductsData(); // Actualizar datos de productos
    });

    function updateProductsData() {
        var productsData = [];
        $('#productosTabla tbody tr').each(function () {
            var row = $(this);
            var product = {
                IdProducto: row.data('id'),
                FechaIngreso: row.find('input[name*="FechaIngreso"]').val(),
                FechaVencimiento: row.find('input[name*="FechaVencimiento"]').val()
            };
            productsData.push(product);

        });
        $('#productosData').val(JSON.stringify(productsData));
    }

    $('form').on('submit', function () {
        updateProductsData();
        console.log('Form Submitted. Products Data:', $('#productosData').val()); // Depuración
    });
});
