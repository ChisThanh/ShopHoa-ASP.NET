var cartList = JSON.parse(localStorage.getItem('cart')) || [];

var cart = {
    setItem: function (id, name, image, price, quantity) {
        let item = {
            id: id,
            name: name,
            image: image,
            price: price,
            quantity: quantity,
        };
        var foundIndex = cartList.findIndex(item => item.id === id);
        if (foundIndex === -1) {
            cartList.push(item);
        } else {
            this.updateItem(id, quantity, 'up');
            return; 
        }
        localStorage.setItem('cart', JSON.stringify(cartList));
    },
    updateItem: function (id, newQuantity, type) {
        cartList = this.getList();
        var foundIndex = cartList.findIndex(item => item.id === id);
        if (foundIndex !== -1) {
            var q = parseFloat(newQuantity);
            let quantity = parseFloat(cartList[foundIndex].quantity);
            if (type === 'desc') {
                cartList[foundIndex].quantity = quantity - q;
                if (cartList[foundIndex].quantity < 0) {
                    cartList[foundIndex].quantity = 0;
                }
            } else if (type === 'inc') {
                cartList[foundIndex].quantity = quantity + q;
            } else if (type === 'up') {
                cartList[foundIndex].quantity = q;
            }
        }
        localStorage.setItem('cart', JSON.stringify(cartList));
    },
    getList: function () {
        let retrievedCartList = JSON.parse(localStorage.getItem('cart'));
        return retrievedCartList || [];
    },
    getQuantity: function () {
        return this.getList().length;
    },
    getTotal: function () {
        cartList = this.getList();
        return cartList.reduce((total, item) => total + (item.price * item.quantity), 0);
    },
    removeItem: function (id) {
        cartList = this.getList();
        cartList = cartList.filter(item => item.id !== id);
        localStorage.setItem('cart', JSON.stringify(cartList));
    },
    clear: function () {
        localStorage.removeItem('cart');
    },
    loadCartMini: function () {
        $("#miniCart").html("");
        if (cart.getList().length === 0) {
            $("#miniCart").append('<div>Chưa có sản phẩm trong giỏ hàng</div>');
        } else {
            $.each(cart.getList(), function (i, v) {
                var itemDiv = $("<div>").addClass("single-cart-item");
                itemDiv.append(`
                    <div class="cart-img">
                        <a href="##"><img src="${v.image}" alt=""></a>
                    </div>
                    <div class="cart-text">
                        <h5 class="title"><a href="cart.html">${v.name}</a></h5>
                        <div class="cart-text-btn">
                            <div class="cart-qty">
                                <span>${v.quantity}×</span>
                                <span class="cart-price">${v.price} VNĐ</span>
                            </div>
                            <button type="button"><i class="ion-trash-b"></i></button>
                        </div>
                    </div>
                    `);
                $("#miniCart").append(itemDiv);
            });
        }

        $('.cart-item_count').html(`${cart.getQuantity()}`);
        $('#txtTotal').html(`${cart.getTotal()} VNĐ`);
    }
};




cart.loadCartMini();