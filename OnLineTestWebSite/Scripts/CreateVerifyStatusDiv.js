//生成用户审核状态层,
//使用此方法，请添加 css/CreateVerifyStatusDiv.CSS样式表配合使用
//使用此方法，请添加 Scripts/common.js
//data中可能有多个状态数据，都要生成
//其中一个的数据结构要求如下所示
//VerifyStatusId      463
//QuestionId          268
//VerifyTimes         1
//IsPass              true
//VerifyTime          "/Date(1442647567043)/"
//UserId              1
//UserName            "yxshu"
//UserChineseName     "余项树"
//UserImageName       "default.jpg"
//UserEmail           "yxshu@qq.com"
//IsValidate          true
//Tel                 "15872367795"
//UserScore           154
//UserChineseName1    "余项树"
//UserGroupId         1
//UserGroupName       "超级管理员"
//UserGroupRemark     "超级管理员"
//UserRankId          7
//UserRankName        "管事"
//MinScore            141
//MaxScore            200
//
//
//
//
//
function CreateVerifyStatusDiv(data) {
    if (data == null || data == undefined)
        return;
    var VerifyStatusDiv = $("<div id='verifystatusdiv'></div>");
    for (var i = 0; i < data.length; i++) {//因为data可能包含多条数据
        var div = $("<div class='varifystatus'></div>");//每一个审核状态最外面的框
        var verifyuser = $("<div class='verifyuser'></div>");
        var userimage = $("<div class='userimage'><img class='img-userimage' alt='用户图像' src='../UserImages/" + data[i]["UserImageName"] + "'/><img class='mast' alt='' src='../Images/40mast.png'/></div>");//用户图像
        var usergroupandrank = $("<div class='usergroupandrank'></div>");//用户组别和等级
        var usergroup = $("<div class='usergroup'>组别：" + data[i]["UserGroupName"] + "</div>");
        var userrank = $("<div class='userrank'>等级：" + data[i]["UserRankName"] + "</div>");
        var username = $("<div class='username'>" + data[i]["UserChineseName"] + "</div>");//用户中文名
        var sendmessage = $("<div class='sendmessage'><a href='#'>站内信</a></div>");//给用户发送站内信
        var verifycontent = $("<div class='verifycontent'></div>");//审核内容
        var contenttime = $("<div class='verifytime'>" + JsonToDate(data[i]["VerifyTime"]).Format("yyyy-MM-dd hh:mm:ss") + "</div>");//内容-时间
        var contenttimes = $("<div class='verifytimes'>第" + data[i]["VerifyTimes"] + "次审核</div>");//内容-第几次审核
        var contentheader = $("<div class='verifyheader'></div>");//内容头
        var content = $("<div class='ispass'>" + a(data[i]["IsPass"]) + "</div>");//内容
        if (data[i]["IsPass"]) {
            $(content).addClass("contentispass");
        } else {
            $(content).addClass("contentnopass");
        }
        var contentfooter = $("<div class='verifyfooter'></div>");//内容尾
        VerifyStatusDiv.append(div.append(verifyuser.append(userimage, usergroupandrank.append(usergroup, userrank), username, sendmessage), verifycontent.append(contentheader.append(contenttime, contenttimes), content, contentfooter.append(contenttime.clone(), contenttimes.clone()))));
    }
    return VerifyStatusDiv;//将生成的结构返回
}
function a(b) {
    return b ? "审核通过" : "审核未通过";
}
