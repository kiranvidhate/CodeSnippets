<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Home page</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-language" content="" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
     <script src="Scripts/angular.min.js"></script>
    <script src="Scripts/Script.js"></script>

    <style>
        .button
        {
            border: 1px solid #006;
            background: #ccc;
            padding:2px;
        }
        .button:hover
        {
            border: 1px solid #f00;
            background: #eef;
        }
    </style>
</head>
<body style="font-family: Verdana; font-size: 12px;"  ng-app="myModule">
    <form id="form1" runat="server">
    <div id="mainDiv">
        <div id="divHeader">
            <div id="divLogo">
                <h1>
                  Understanding Filters in Angular JS
                </h1>
            </div>
        </div>
        <div id="divMain">
            <div id="divMain2">
                <div id="divContent"><br/><br/>
                    <div ng-controller="myController">
                        Rows to display : <input type="number" step="1"
                                                 ng-model="rowCount" max="10" min="0" />
                        <br /><br />
                        <lable>{{ scope.data}} </lable>
                        <table>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Date of Birth</th>
                                    <th>Gender</th>
                                    <th>Salary (number filter)</th>
                                    <th>Salary (currency filter)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="employee in employees | limitTo:rowCount">
                                    <td> {{ employee.name | uppercase }} </td>
                                    <td> {{ employee.dateOfBirth | date:"dd/MM/yyyy" }} </td>
                                    <td> {{ employee.gender }} </td>
                                    <td> {{ employee.salary | number:2 }} </td>
                                    <td> {{ employee.salary | currency : "&pound;" : 1 }} </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>