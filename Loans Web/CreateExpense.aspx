<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateExpense.aspx.cs" Inherits="Loans_Web.CreateExpense" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Create Expense
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-lg mx-auto my-5 bg-dark p-5 m-5">

        <div class="text-white">
            <h2>Create an Expense</h2>
        </div>

        <asp:Label ID="lblError" runat="server" />

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
                <span class="input-group-text">
                    <asp:Label ID="lblAmount" runat="server" /></span>
            </div>

            <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <!-- To Expense -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <span class="input-group-text">Salary to Expense (%)</span>
            </div>

            <asp:TextBox ID="txtToExpense" ToolTip="The % of your monthly free-income (after taxes and loans) devoted to this expense" CssClass="form-control" runat="server"></asp:TextBox>
        </div>


        <!-- Interest -->
        <div class="input-group my-2">
            <div class="input-group-prepend">
                <div class="input-group-text">
                    <asp:CheckBox ID="chbInterest" CssClass="chbBig form-control mr-2" runat="server" OnCheckedChanged="chbInterest_CheckedChanged" AutoPostBack="true" />
                    Interest Rate (APR): 
                </div>
            </div>

            <asp:TextBox ID="txtInterest" ToolTip="Interest rate of this expenses, compounded monthly" CssClass="form-control" runat="server"></asp:TextBox>
        </div>



        <div class="d-inline-block col-1 justify-content-end text-white my-3">

            <div class="d-flex flex-row">
                <label class="my-auto">Recurring:</label>
                <asp:CheckBox ID="chbRecurring" class="chbBig" runat="server" OnCheckedChanged="chbRecurring_CheckedChanged" AutoPostBack="true" ToolTip="Set monthly dollar amount for fixed-cost or pre-existing expenses" />
            </div>

            <div class="d-flex flex-row">
                <label class="my-auto">Schedule:</label>
                <asp:CheckBox ID="chbScheduled" class="chbBig my-auto" runat="server" OnCheckedChanged="chbScheduled_CheckedChanged" AutoPostBack="true" />
            </div>
        </div>


        <!-- Date Pickers -->
        <div class="d-flex justify-content-around" id="divDatePickers" runat="server">


            <div id="divStart" class="d-inline-flex bg-white m-1 p-2" style="border-radius: 8px" runat="server">

                <div class="d-inline-flex input-group my-2">
                    <div class="input-group-text">
                        <span class="h2">Start Date</span>
                    </div>
                    <asp:TextBox ID="cdrStart" CssClass="form-control" type="date" runat="server" />
                </div>
            </div>


            <div id="divEnd" class="d-inline-flex bg-white m-1 p-2" style="border-radius: 8px" runat="server">

                <div class="input-group my-2">
                    <div class="input-group-text pr-2">
                        <span class="h2">End Date</span>
                    </div>
                    <asp:TextBox ID="cdrEnd" CssClass="form-control" type="date" runat="server" />
                </div>
            </div>


        </div>



        <div class="col-12 mt-5 d-flex justify-content-around">

            <asp:Button ID="btnCancel" class="btn btn-danger d-inline" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
            <asp:Button ID="btnCreateExpense" class="btn btn-success" Text="Create" runat="server" OnClick="btnCreateExpense_Click" />
        </div>
    </div>



</asp:Content>



