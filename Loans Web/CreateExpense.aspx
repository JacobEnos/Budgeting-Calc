<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateExpense.aspx.cs" Inherits="Loans_Web.CreateExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Create Expense
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mx-auto my-5 bg-dark p-5" style="width:40%">

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
                <span class="input-group-text">Amount</span>
            </div>

            <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <!-- To Expense -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <span class="input-group-text">Salary to Expense(%)</span>
            </div>

            <asp:TextBox ID="txtToExpense" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <div class="text-white my-3">
            <label>Recurring:</label>
            <asp:CheckBox ID="chbRecurring" runat="server" OnCheckedChanged="chbRecurring_CheckedChanged" AutoPostBack="true" />

            <br />

            <label>Schedule:</label>
            <asp:CheckBox ID="chbScheduled" class="my-auto" runat="server" OnCheckedChanged="chbScheduled_CheckedChanged" AutoPostBack="true" />
        </div>


        <!-- Date Pickers -->
        <div class="d-flex justify-content-around" id="divDatePickers" runat="server">


            <div class="col-6 bg-white p-1 m-1" style="border-radius: 8px">

                <div class="d-flex justify-content-center h3">Start Date</div>
                <asp:Calendar ID="cdrStart" CssClass="mx-2 px-2" runat="server" OnSelectionChanged="cdrStart_SelectionChanged" AutoPostBack="true"></asp:Calendar>
            </div>

            <div class="col-6 bg-white p-1 m-1 justify-content-center" style="border-radius: 8px">
                
                <div class="d-flex justify-content-center h3">End Date</div>
                <asp:Calendar ID="cdrEnd" CssClass="mx-2" runat="server" OnSelectionChanged="cdrEnd_SelectionChanged" AutoPostBack="true"></asp:Calendar>
            </div>

        </div>



        <div class="col-12 mt-5 d-flex justify-content-around">

            <asp:Button ID="btnCancel" class="btn btn-danger d-inline" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
            <asp:Button ID="btnCreateExpense" class="btn btn-success" Text="Create" runat="server" OnClick="btnCreateExpense_Click" />
        </div>
    </div>



</asp:Content>



