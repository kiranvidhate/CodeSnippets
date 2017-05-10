<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!doctype html>
<html ng-app="App" >
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
    <script>document.write("<base href=\"" + document.location + "\" />");</script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.0.2/angular.js"></script>
    <script src="Assets/js/app.js"></script>
</head>
<body ng-controller="TodoCtrl">
    <div id="mainDiv">
        <div id="divHeader">
            <div id="divHeader2">
                <div id="divLogo">
                    <h1>AngularJS AJAX Demo
                    </h1>
                </div>
                <div id="divMenu">
                </div>
            </div>
        </div>
        <div id="divMain">
            <div id="divMain2">
                <div id="divContent">
                    <ul>
                        <li ng-repeat="todo in todos">{{todo.text}} - <em>{{todo.done}}</em></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
