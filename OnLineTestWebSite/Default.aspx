<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���߿���ϵͳ ��¼</title>
    <link href="CSS/common.css" rel="stylesheet" type="text/css" />
    <link href="CSS/default.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/Default.js" type="text/javascript"></script>

</head>
<body>
    <center>
        <form method="post" id="form1" action="ashx/Login.ashx" runat="server">
            <div id="container">
                <!--������ͷ-->
                <div id="header">
                    <%--�ߴ磺150px*1000px  ��CSS�����һ��LOGO.PNG--%>
                </div>
                <!--���������Ҫ�����ű���ͼ�ģ����100%  �߶�400px �������Сͼ��͵�¼����ͨ������λ��ʹ��λ�ڴ˱��������� -->
                <div id="Mbody">
                    <!--�����Ǳ���������Сͼ��-->
                    <div id="minico">
                        <ul>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                        </ul>
                    </div>
                </div>
                <!--���ǵ�¼�Ĵ���--->
                <div id="content">
                    <!--����--->
                    <div id="title">��¼����ϵͳ</div>
                    <!--�û���������/�û���--->
                    <div id="txtUser">
                        <img id="usernameico" class="tubiao" src="Images/username.jpg" alt="�û���" title="�û���" /><input type="text" name="txtusername" id="username" class="shurukuan" value="" />
                    </div>
                    <!--����--->
                    <div id="txtPassword">
                        <img id="passwordico" class="tubiao" src="Images/password.jpg" alt="����" title="����" /><input type="password" name="txtpassword" id="password" class="shurukuan" value="" />
                    </div>
                    <!--��֤��--->
                    <div id="txtValidCode">
                        <img id="validico" class="tubiao" src="Images/valid.jpg" alt="��֤��" title="��֤��" /><input type="text" name="txtValidCode" id="validcode" class="shurukuan" value="��֤��" /><span id="validmark"></span>
                        <!--��֤��ͼƬ--->
                        <%="<img id='codeimg1' alt='��֤��' src='ashx/HandlerValidCode.ashx?wordnum=4&height=30&id="+DateTime.Now+"' onclick='checkcode()'/>" %>
                    </div>
                    <div id="errormessage"></div>
                    <!--��¼��ť--->
                    <div id="denglu">
                        <input id="btnsubmit" type="submit" value="��  ¼" />
                    </div>
                    <!--�ײ�ע��--->
                    <div id="last"><a href="register.html" id="register">���ע��</a><span id="lianxigly"><a href="#">�˻�����</a><a href="#">��ϵ����Ա</a></span></div>
                </div>

                <!--����ҳ�Ų���--->
                <div id="footer">
                    <p>
                        ��ַ:����ʡ�人�к�ɽ����ɳ�޴��6�� �ʱ�:430065 �绰:027-88756000 ������:��ICP��06007470��
                    </p>
                    <br />
                    <p>
                        �人��ְͨҵѧԺ whtcc.edu.cn 2004-2016 &copy ��Ȩ���� ���ά��:����ѧԺ ��ϵ��ʽ:����ѧԺ �����ṩ:��������������
                    </p>
                </div>

            </div>

        </form>
    </center>
</body>
</html>
