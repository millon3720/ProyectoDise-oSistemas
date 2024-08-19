$(document).ready(function () {
    // Manejo de cambio en el producto
    $('#Productos').change(function () {
        var selectedOption = $(this).find('option:selected');
        var productoId = selectedOption.val();
        if (productoId) {
            var row = `
            <tr data-id="${productoId}">
                <td>${productoId}</td>
                <td>${selectedOption.data('product-name')}</td>
                <td>${selectedOption.data('product-description')}</td>
                <td>${selectedOption.data('product-presentation')}</td>
                <td>${selectedOption.data('product-quantity')}</td>
                <td>${selectedOption.data('product-package-type')}</td>
                <td>${selectedOption.data('product-package-quantity')}</td>
                <td>${selectedOption.data('product-price')}</td>
                <td><input type="checkbox" class="delete-checkbox" checked /></td>
            </tr>`;

            $('#productosTabla tbody').append(row);
            $(this).val('');
            updateSelectedProducts();
        }
    });
    $('#productosTabla').on('click', '.delete-checkbox', function () {
        $(this).closest('tr').remove();
        updateSelectedProducts();
    });
    // Actualiza los productos seleccionados
    function updateSelectedProducts() {
        var selectedProducts = [];
        $('#productosTabla tbody tr').each(function () {
            var productId = $(this).data('id');
            if (productId) {
                selectedProducts.push(productId);
            }
        });
        $('#selectedProducts').val(JSON.stringify(selectedProducts));
        console.log('Selected Products:', selectedProducts); // Depuración
    }

    // Asegúrate de que el campo oculto se actualice antes de enviar el formulario
    $('form').on('submit', function () {
        updateSelectedProducts();
        console.log('Form Submitted. Selected Products:', $('#selectedProducts').val()); // Depuración
    });


});