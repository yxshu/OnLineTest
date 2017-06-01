<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage-logined.master" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="TestAndPractice_AddQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../CSS/UpdataQuestion.css" />
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script type="text/javascript" src="../Scripts/Ajax.js"></script>
    <script type="text/javascript" src="../Scripts/UpdataQuestion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="Server">
    <div class="row" id="remark">试题提交以后会被随机审核，通过三人及以上审核通过才会被使用和检索。</div>
    <div class="row">
        <label class="title">试题编号</label><label class="content" id="QuestionId"><%=QuestionId %></label>
    </div>
    <div class="row">
        <label class="title  QT" id="lab_questiontitle">试题题目<label style="color: red">*</label></label><textarea class="content" id="QuestionTitle" cols="20" rows="2"></textarea>
    </div>
    <div class="row">
        <label class="title QT">选项A<label style="color: red">*</label></label><textarea class="content QC" id="AnswerA" cols="20" rows="2"></textarea>
    </div>
    <div class="row">
        <label class="title QT">选项B<label style="color: red">*</label></label><textarea class="content QC" id="AnswerB" cols="20" rows="2"></textarea>
    </div>
    <div class="row">
        <label class="title QT">选项C<label style="color: red">*</label></label><textarea class="content QC" id="AnswerC" cols="20" rows="2"></textarea>
    </div>
    <div class="row">
        <label class="title QT">选项D<label style="color: red">*</label></label><textarea class="content QC" id="AnswerD" cols="20" rows="2"></textarea>
    </div>
    <div class="row">
        <label class="title QT">原试题参考图</label><img id="oldImageAddress" alt="原试题参考图" src="" />
    </div>
    <div class="row">
        <label class="title">新试题参考图</label><input id="ImageAddress" class="content" type="file" />请确保上传图像不超过1M
    </div>
    <div class="row">
        <label class="title">参考答案<label style="color: red">*</label></label><select class="content" id="CorrectAnswer"><option value="-1" selected="selected">请选择……</option>
            <option value="1">A、</option>
            <option value="2">B、</option>
            <option value="3">C、</option>
            <option value="4">D、</option>
        </select>
    </div>
    <div class="row">
        <label class="title">初始难度系数<label style="color: red">*</label></label><select class="content" id="DifficultyId"><option value="-1" selected="selected">请选择……</option>
        </select>
    </div>
    <div class="row">
        <label class="title">作者<label style="color: red">*</label></label><label class="content" id="UserName"></label>
    </div>
    <div class="row">
        <label class="title">试题类型<label style="color: red">*</label></label><select class="content" id="PaperCodeId"><option value="-1" selected="selected">请选择……</option>
        </select>
    </div>
    <%--//这里面处理类型和科目两个任务--%>
    <div class="row">
        <label class="title">参考教材</label><select class="content" id="TextBookId"><option value="-1" selected="selected">请选择……</option>
        </select>
    </div>
    <%--根据papercode来选择教材--%>
    <div class="row">
        <label class="title">参考章节</label><select class="content" id="ChapterId"><option value="-1" selected="selected">请选择……</option>
        </select>
    </div>
    <%--根据papercode来选择教材--%>
    <div class="row" id="PastExamRow">
        <label class="title">是否真题</label><select class="content" id="PastExamPaperId"><option value="1">是</option>
            <option value="0" selected="selected">否</option>
        </select>
    </div>
    <%--根据是否真题动态添加考区、期数、在真题中的号码--%>
    <div class="row" id="updata">保存</div>

</asp:Content>

