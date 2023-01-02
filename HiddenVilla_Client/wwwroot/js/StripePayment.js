window.redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51MHFZ8Cp12DN8y3ba6DZsvO6b11sc48OPbPNJZDKfdRJLunsYztvDzVYxhBQ9RaDJVfbfzFgNKFy2eL9in0rEKLx00eOFCGR3I');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};