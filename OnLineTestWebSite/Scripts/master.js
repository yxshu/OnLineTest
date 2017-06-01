var sidebar;
var sidebarheight;
var content;
//定义左边导航栏的显示和隐藏
$(function () {
    sidebar = $("#sidebar");//整个菜单
    sidebarheight = sidebar.height(); //取得导航栏的高度
    content = $("#content");   //右边的内容区域
    content.css({ "min-height": sidebarheight + "px" }); //设置内容区域的最小高度
    var h = sidebar.height() > content.height() ? sidebar.height() : content.height();//比较内容区和菜单区哪个更长些，取其中较长者
    $(".authority").each(function (index) { //对于每一个authority执行//每一类菜单表
        //var tipHeight = $(".authorityChilds:eq(" + index + ")").height(); //对应子菜单的高度
        var obj = $(this);//一个菜单块，包含一项和子菜单
        var paddingtop = obj.css("padding-top");
        var top = obj.offset().top;//取top位置
        var left = obj.offset().left;//取left位置
        var height = obj.css("height");//取高度
        var width = obj.css("width");//取宽度
        var min = $(".authorityChilds:eq(" + index + ")");  //取得子菜单
        var mtop = min.offset().top;
        var mHeight = min.css("height"); //取得子菜单的高度
        $(this).mouseenter(function () {
            obj.css({ "background": "#EEEEEE" });
            min.css({ "display": "block", "padding": paddingtop, "min-height": height, "border": "solid 1px #FF9924", "border-left": "none", "left": width, "background": "#EEEEEE" });

            var mnewtop = 0 - parseInt(height) - parseInt(paddingtop);
            if (mHeight > sidebar.height()) {  //子菜单的高度大于整个菜单的高度
                min.css({ "height": sidebar.height(), "overflow": "scroll" });
                mnewtop = parseInt(sidebar.offset().top) - parseInt(min.offset().top) - 2 * parseInt(paddingtop);
            } else {
                if (parseInt(min.offset().top) + parseInt(mHeight) > parseInt(sidebar.offset().top) + parseInt(sidebar.height())) { //底部超出菜单解决方案
                    mnewtop = parseInt(sidebar.offset().top) + parseInt(sidebar.height()) - parseInt(min.offset().top) - parseInt(mHeight) - 2 * parseInt(paddingtop);
                }
            }
            min.css({ "top": mnewtop }).slideDown(100);
            $(window).scroll(); //根据导航栏调整滚动条
        }).mouseleave(function () { //如果鼠标移出
            $(".authorityChilds").css({ "top": 0 }).hide(); //子菜单隐藏
            $(this).css({ "background": "#FFFFFF" }); //设置主菜单属性
            $(window).scroll(); //根据导航栏调整内容区域的最小高度
        });
    });

    //------------------------------下面是自定义函数区----------------------------------------------------
    //定义左边导航栏的位置随滚动条的变化
    var top = sidebar.offset().top;
    var clientheight = $(window).height();
    $(window).scroll(function () {
        var scrolltop = $(window).scrollTop(); //窗口的高度
        var mintop = top;
        var maxtop = top + content.height() - sidebarheight - 80;
        var starshifttop, stopshifttop; //开始滚动/停止滚动时滚动条的位置
        if (sidebarheight > clientheight)
            starshifttop = top + sidebarheight - clientheight;
        else {
            starshifttop = top;
        }
        if (sidebarheight > clientheight)
            stopshifttop = top + content.height() - sidebarheight; //停止滚动的位置
        else {
            stopshifttop = top + content.height() - clientheight;
        }
        var newtop = scrolltop - sidebarheight + clientheight;
        if (scrolltop > starshifttop && scrolltop < stopshifttop) {
            sidebar.css({ "top": newtop - 10 + "px" });
        } else if (scrolltop < starshifttop) {
            sidebar.css({ "top": mintop + "px" });
        } else if (scrolltop > stopshifttop) {
            sidebar.css({ "top": maxtop + "px" });
        }

    });


    $(".authorityChilds a").on({
        "mouseover": function () { $(this).css({ "background-color": "#FF9924" }) },
        "mouseout": function () { $(this).css({ "background-color": "#EEE" }) }
    });
})