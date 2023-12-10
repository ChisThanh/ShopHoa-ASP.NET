
helper = {
    showNotification: function (from, align) {
        
        $.notify({
            icon: "pe-7s-gift",
            message: "<b>Thêm sản phẩm thành công</b> - sản phẩm đã được thêm vào giỏ hàng"

        }, {
            type: 'success',
            timer: 1000,
            placement: {
                from: from,
                align: align
            }
        });
    },
    showNotf: function () {

        $.notify({
            icon: "pe-7s-gift",
            message: "<b>Đặt hàng thành công rồi nha <3</b>"

        }, {
            type: 'success',
            timer: 1000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
    },
}
          

