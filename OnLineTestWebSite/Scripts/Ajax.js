//异步从服务器获得数据
//参数datas:利用post方法提交的数据，如："name":"lucky","age":13
//参数page:要访问的远程页面，如："./ashx/GetCurrentUser.ashx"
//参数callback,是执行成功以后执行的方法，会附上返回的data
//参数 obj,调用此方法的对象
//返回值有两种形式：失败：null,成功：服务器返回的json数据
//Uajax({data:"data"}, "../ashx/a.ashx", callback_success, callback_error, obj) 
function Uajax(datas, page, callback_success, callback_error, obj) {
    var result;
    $.ajax({
        type: "post",
        url: page,
        data: datas,
        dataType: "json",
        async: true,
        beforeSend: function () {
            result = false;
        },
        success: function (data) {
            callback_success(data, obj);

        },
        error: function () {
            callback_error(obj);
        }
    });
}