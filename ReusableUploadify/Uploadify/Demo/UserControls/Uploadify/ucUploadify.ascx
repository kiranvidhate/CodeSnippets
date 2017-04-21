<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadify.ascx.cs" Inherits="UserControls_Uploadify_WebUserControl" %>


<link rel="Stylesheet" type="text/css" href="UserControls/Uploadify/lib/uploadify.css" />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript" src="UserControls/Uploadify/lib/jquery.uploadify.js"></script>
<div id="divUploadify" style="background-color:#fff;padding:10px;height:100%;">
    <a href="javascript:$('#spnMessage').hide();$('#<%=fileUploadify.ClientID%>').uploadify('upload','*');">Start Upload</a>
    &nbsp;|&nbsp;
    <a href="javascript:$('#spnMessage').hide();$('#<%=fileUploadify.ClientID%>').uploadify('cancel', '*');">Clear</a>
    <div style="padding-top: 15px; height: 50px;">
        <div id="spnMessage" style="padding-top: 10px; padding-bottom: 10px;"></div>
        <asp:FileUpload ID="fileUploadify" runat="server" />
    </div>
</div>
<script type="text/javascript">
    var uploadFolder = "<%= this.UploadFolder %>";
    var allowedFileExtensions = "<%= this.AllowedFileExtensions %>";
    var multiUpload = "<%= this.MultiUpload %>" == "False" ? false : true;
    var maxFileSize = "<%= this.MaxFileSize %>";
    var maxQueueSize = "<%= this.MaxQueueSize %>";

    $("#spnMessage").hide();
    $(window).load(
        function () {
            $('#<%=fileUploadify.ClientID%>').uploadify({
                'swf': 'UserControls/Uploadify/lib/uploadify.swf',
                'uploader': 'UserControls/Uploadify/Upload.ashx',
                'cancelImg': 'UserControls/Uploadify/lib/cancel.png',
                'auto': false,
                'multi': multiUpload,
                'fileDesc': 'Image Files',
                'fileTypeExts': allowedFileExtensions,
                'queueSizeLimit': maxQueueSize,
                'fileSizeLimit': maxFileSize,
                'buttonText': 'Choose Images',
                'formData': { 'UploadFolder': uploadFolder },
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    $("#spnMessage").show();
                    $("#spnMessage").html('Error: <br/>' + file.name + ' could not be uploaded.<br/>' + errorString);
                    $("#spnMessage").attr("class", "error");
                },
                'onUploadSuccess': function (file, data, response) {
                    $("#spnMessage").show();
                    $("#spnMessage").html('The file(s) uploaded successfully.');
                    $("#spnMessage").attr("class", "success");
                }
            });
        }
    );
</script>
