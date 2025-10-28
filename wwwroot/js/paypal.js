paypal.Buttons({
    createOrder: function(data, actions) {
        const totalElement = document.getElementById('basket-total');
        const total = totalElement ? totalElement.dataset.total : "0.00";

        return actions.order.create({
            purchase_units: [{
                amount: {
                    value: total
                }
            }]
        });

    },
    onApprove: function(data, actions) {
        return actions.order.capture().then(function(details) {
            window.location.href = '/Confirmation';
        });
    }
}).render('#paypal-button-container');

