<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateExpense.aspx.cs" Inherits="Loans_Web.CreateExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Create Expense
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-lg mx-auto my-5 bg-dark p-5 m-5">

        <div class="text-white">
            <h2>Create an Expense</h2>
        </div>

        <asp:Label ID="lblError" runat="server"/>

        <!-- Name -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <span class="input-group-text">Name</span>
            </div>

            <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>

        <!-- Amount -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <span class="input-group-text"><asp:Label ID="lblAmount" runat="server"/></span>
            </div>

            <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <!-- To Expense -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <span class="input-group-text">Salary to Expense (%)</span>
            </div>

            <asp:TextBox ID="txtToExpense" tooltip="The % of your monthly free-income (after taxes and loans) devoted to this expense" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <div class="text-white my-3">
            <label>Recurring:</label>
            <asp:CheckBox ID="chbRecurring" runat="server" OnCheckedChanged="chbRecurring_CheckedChanged" AutoPostBack="true" tooltip="Set monthly dollar amount for fixed-cost or pre-existing expenses"/>

            <br />

            <label>Schedule:</label>
            <asp:CheckBox ID="chbScheduled" class="my-auto" runat="server" OnCheckedChanged="chbScheduled_CheckedChanged" AutoPostBack="true" />
        </div>


        <!-- Date Pickers -->
        <div class="row justify-content-evenly" id="divDatePickers" runat="server">


            <div id="divStart" class="col-xl-5 bg-white m-1 p-1" style="border-radius: 8px" runat="server">

                <div class="d-flex justify-content-center h3">Start Date</div>
                <asp:Calendar ID="cdrStart" CssClass="mx-auto mb-2" runat="server" OnSelectionChanged="cdrStart_SelectionChanged" AutoPostBack="true"></asp:Calendar>
            </div>

            <div id="divEnd" class="col-xl-5 bg-white p-1 m-1" style="border-radius: 8px" runat="server">
                
                <div class="d-flex justify-content-center h3">End Date</div>
                <asp:Calendar ID="cdrEnd" CssClass="mx-auto mb-2" runat="server" OnSelectionChanged="cdrEnd_SelectionChanged" AutoPostBack="true"></asp:Calendar>
            </div>

        </div>



        <div class="col-12 mt-5 d-flex justify-content-around">

            <asp:Button ID="btnCancel" class="btn btn-danger d-inline" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
            <asp:Button ID="btnCreateExpense" class="btn btn-success" Text="Create" runat="server" OnClick="btnCreateExpense_Click" />
        </div>
    </div>



</asp:Content>



