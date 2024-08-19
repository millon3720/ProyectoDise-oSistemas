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
    }

    // Maneja la eliminación de productos desde la tabla
    $('#productosTabla').on('click', '.delete-checkbox', function () {
        $(this).closest('tr').remove();
        updateSelectedProducts();
    });

    // Actualiza los productos seleccionados antes de enviar el formulario
    $('form').on('submit', function () {
        updateSelectedProducts();
    });

    // Carga los productos preexistentes
    function loadExistingProducts() {
        var existingProducts = [];
        $('#productosTabla tbody tr').each(function () {
            var productId = $(this).data('id');
            if (productId) {
                existingProducts.push(productId);
            }
        });
        $('#selectedProducts').val(JSON.stringify(existingProducts));
    }

    // Llama a loadExistingProducts en el inicio para llenar el campo oculto
    loadExistingProducts();
});