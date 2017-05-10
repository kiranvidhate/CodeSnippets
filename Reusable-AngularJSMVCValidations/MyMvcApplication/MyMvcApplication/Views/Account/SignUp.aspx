<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>SignUp</title>
    <style type="text/css">
        label {
            display: block;
        }

        input.ng-invalid.ng-dirty {
            background-color: #FA787E;
        }

        input.ng-valid.ng-dirty {
            background-color: #78FA89;
        }

        .invalid, .error {
            color: red;
            margin-top: 10px;
        }
    </style>
    <script src="../Scripts/jquery-1.8.2.min.js"></script>
    <script src="../Scripts/angular.min.js"></script>
    <script src="../Scripts/SignUpController.js"></script>
</head>
<body>
    <div data-ng-app="validation">

        <form name="mainForm" data-ng-submit="sendForm()" data-ng-controller="SignUpController" novalidate>
            <div class="error">{{message}}</div>
            <div>
                <label for="userName">User Name</label>
                <input id="userName" name="userName" type="text" data-ng-model="person.UserName" />
            </div>
            <div>
                <label for="password">Password</label>
                <input id="password" name="password" type="password" data-ng-model="person.Password" />
            </div>
            <div>
                <label for="confirmPassword">Confirm Password</label>
                <input id="confirmPassword" name="confirmPassword" type="password" data-ng-model="person.ConfirmPassword" />
            </div>
            <div>
                <input type="checkbox" data-ng-model="agreedToTerms" name="agreedToTerms" id="agreedToTerms" required />
                <label for="agreedToTerms" style="display: inline">I agree to the terms</label>
            </div>
            <div>
                <button type="submit" data-ng-disabled="mainForm.$invalid">Submit</button>
            </div>
            <ul>
                <li id="errorMessages" class="error" data-ng-repeat="error in errors">{{error}}</li>
            </ul>
        </form>
    </div>
</body>
</html>
