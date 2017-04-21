<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Home page</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-language" content="" />
    <link rel="Stylesheet" type="text/css" href="Assets/css/style.css" />
    <style>
        body {
            padding: 10px;
            font: 14px/18px Calibri;
        }

        .bold {
            font-weight: bold;
        }

        td {
            padding: 5px;
            border: 1px solid #999;
        }

        p, output {
            margin: 10px 0 0 0;
        }

        #drop_zone {
            margin: 10px 0;
            width: 100%;
            min-height: 150px;
            text-align: center;
            text-transform: uppercase;
            font-weight: bold;
            border: 8px dashed #898;
            height: 160px;
        }
    </style>
    <title></title>
    <script>
        var files;
        function handleDragOver(event) {
            event.stopPropagation();
            event.preventDefault();
            var dropZone = document.getElementById('drop_zone');
            dropZone.innerHTML = "Drop now";
        }

        function handleDnDFileSelect(event) {
            event.stopPropagation();
            event.preventDefault();

            /* Read the list of all the selected files. */
            files = event.dataTransfer.files;

            /* Consolidate the output element. */
            var form = document.getElementById('form1');
            var data = new FormData(form);

            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200 && xhr.responseText) {
                    // alert("upload done!");
                    document.getElementById("SucessMessage").innerHTML = "File Uploaded Successfully!";
                } else {
                    //alert("upload failed!");
                }
            };

            xhr.open('POST', "Default.aspx");
            // xhr.setRequestHeader("Content-type", "multipart/form-data");
            xhr.send(data);

        }

        function NoDoFileUpload() {
            document.getElementById("hdnDoUpload").value = "0";
        }
    </script>
</head>
<body>
    <div id="mainDiv">
        <div id="divHeader">
            <div id="divHeader2">
                <div id="divLogo">
                    <h1>ASP.Net Zip Builder
                    </h1>
                </div>
                <div id="divMenu">
                </div>
            </div>
        </div>
        <div id="divMain">
            <div id="divMain2">
                <div id="divContent">
                    <div id="SucessMessage" class="success" runat="server" clientidmode="Static"></div>
                    <form id="form1" runat="server" enctype="multipart/form-data">
                        <br />
                        <div id="drop_zone">Drop files here</div>
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="hdnDoUpload" runat="server" ClientIDMode="Static" />
                        <asp:Button ID="btnGenerateZip" runat="server" Text="Generate Zip File" CssClass="btn" OnClientClick="NoDoFileUpload();" OnClick="btnGenerateZip_Click" />
                        <asp:Button ID="btnExtractZip" runat="server" Text="Extract Zip File" CssClass="btn" OnClientClick="NoDoFileUpload();" OnClick="btnExtractZip_Click" />
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
<script>
    if (window.File && window.FileList && window.FileReader) {
        /************************************ 
         * All the File APIs are supported. * 
         * Entire code goes here.           *
         ************************************/


        /* Setup the Drag-n-Drop listeners. */
        var dropZone = document.getElementById('drop_zone');
        dropZone.addEventListener('dragover', handleDragOver, false);
        dropZone.addEventListener('drop', handleDnDFileSelect, false);

    }
    else {
        alert('Sorry! this browser does not support HTML5 File APIs.');
    }
</script>
</html>
