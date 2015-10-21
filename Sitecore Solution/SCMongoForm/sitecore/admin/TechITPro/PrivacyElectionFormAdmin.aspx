<%@ page language="C#" autoeventwireup="true" codebehind="PrivacyElectionFormAdmin.aspx.cs" inherits="SCMongoForm.sitecore.admin.TechITPro.PrivacyElectionFormAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Content/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.9.1/themes/smoothness/jquery-ui.css">
    <script src="../../../Scripts/jquery-1.9.1.min.js"></script>
    <script src=" http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script>
        $(function () {
            $("#startDate, #toDate").datepicker({
                dateFormat: "mm-dd-yy"
            });
        });
    </script>
</head>
<body>
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
                <form id="form1" runat="server">
                    <div class="form-group">
                        <label for="startDate">Start Date</label>
                        <input class="form-control" type="text" id="startDate" runat="server" size="10" maxlength="10" />
                        <asp:requiredfieldvalidator forecolor="Red" controltovalidate="startDate" id="rfldvalStartDate" runat="server" errormessage="Start Date is required"></asp:requiredfieldvalidator>

                    </div>
                    <div class="form-group">
                        <label for="toDate">End Date</label>
                        <input class="form-control" type="text" id="toDate" runat="server" size="10" maxlength="10" />
                        <asp:requiredfieldvalidator forecolor="Red" controltovalidate="toDate" id="rfldvalEndDate" runat="server" errormessage="End Date is required"></asp:requiredfieldvalidator>
                    </div>
                    <div class="form-group">
                        <asp:button id="btnView" class="btn btn-default" runat="server" text="View" onclick="btnView_Click" />
                    </div>

                    <asp:comparevalidator id="compVldDates" forecolor="Red" runat="server" controltovalidate="startDate" controltocompare="toDate" operator="LessThanEqual" type="Date" errormessage="Start date must be less than End date."></asp:comparevalidator>
                </form>
                <div id="FormError" runat="server">
                    Error Processing Request. Please try again later.
                </div>
                <div id="FormResults" runat="server">
                    <asp:repeater id="rptPForms" runat="server">
                        <HeaderTemplate>
                            <table class="table">
                                    <thead>
                                        <tr>
                                           
                                            <th>Name</th>
                                            <th>Address</th>
                                            <th>City</th>
                                            <th>State</th>
                                            <th>ZipCode</th>
                                            <th>Phone</th>
                                            <th>SourceCd</th>
                                            <th>AuditInsertDateUTC</th>
                                           
                                        </tr>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    
                                    <td><%# Eval("FirstName")%> <%# Eval("LastName")%></td>
                                    <td><%# Eval("Address")%></td>
                                    <td><%# Eval("City")%></td>
                                    <td><%# Eval("State")%></td>
                                    <td><%# Eval("ZipCode")%></td>
                                    <td><%# Eval("Phone")%></td>
                                    <td><%# Eval("SourceCd")%></td>
                                    <td><%# Eval("AuditInsertDateUTC")%></td>
                                    
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                 </table>
                            </FooterTemplate>
                        </asp:repeater>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
