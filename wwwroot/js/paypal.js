paypal.Buttons({
    createOrder: function (data, actions) {
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
    onApprove: function (data, actions) {
        return actions.order.capture().then(function (details) {
            return fetch('/Basket?handler=CompleteOrder', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    orderId: data.orderID,
                    payerId: data.payerID
                })
            })
                .then(response => response.json())
                .then(result => {
                    window.location = `/Confirmation?id=${result.orderId}`;
                });
        });
    }
}).render('#paypal-button-container');

