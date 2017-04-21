<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register TagName="JQUploadify" TagPrefix="uc" Src="~/UserControls/Uploadify/ucUploadify.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Home page</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-language" content="" />
    <link rel="Stylesheet" type="text/css" href="Assets/css/style.css" />
</head>
<body>
    <div id="mainDiv">
        <div id="divHeader">
            <div id="divHeader2">
                <div id="divLogo">
                    <h1>ASP.Net jQuery Uploadify
                    </h1>
                </div>
                <div id="divMenu">
                </div>
            </div>
        </div>
        <div id="divMain">
            <div id="divMain2">
                <div id="divContent">
                    <form id="frmFrom" runat="server">
                        <uc:JQUploadify ID="ucUploadify" runat="server" 
                            UploadFolder="~/Uploads" 
                            MultiUpload="true" 
                            MaxFileSize="1000KB" 
                            MaxQueueSize="5"
                            AllowedFileExtensions="*.jpg;*.jpeg;*.gif;*.png"
                            />
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
