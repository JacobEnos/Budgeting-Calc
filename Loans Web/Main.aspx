<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Loans_Web.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Budget Planner</title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.3.2/chart.min.js" ></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet"  />
    <script src="Charting.js"></script>
    <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
</head>
<body class="stars" onload="AjaxGetAndGraph();">

    <style>
        body {
            background-attachment: fixed;
        }


        canvas {
            display: block;
            vertical-align: bottom;
        }


        @import "susy";
        @import "compass/reset";


        .stars, .twinkling, .clouds {
            position: relative;
            display: block;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            width: 100%;
            height: 100vh;
        }


        .stars {
            z-index: 0;
            background: #000 url('https://image.ibb.co/mjnygo/stars.png') repeat top center;
            height: 100%;
            background-repeat: repeat;
        }

        .twinkling {
            z-index: 1;
            background: transparent url('https://image.ibb.co/ir1DE8/twinkling.png') repeat top center;
            animation: move-twink-back 200s linear infinite;
            height: 100%;
            background-repeat: repeat;
        }

        .clouds {
            z-index: 2;
            background: transparent url('https://image.ibb.co/bT4N7T/clouds.png') repeat top center;
            animation: move-clouds-back 200s linear infinite;
            height: 100%;
            background-repeat: repeat;
        }

        @keyframes move-twink-back {
            from {
                background-position: 0 0;
            }

            to {
                background-position: -10000px 5000px;
            }
        }

        @keyframes move-clouds-back {
            from {
                background-position: 0 0;
            }

            to {
                background-position: 10000px 0;
            }
        }

        #divSaveCSV:hover {
            background-color: lightgreen;
            transition: background-color 1s ease-out;
            box-shadow: 0px 3px 5px -2px;
            transition: box-shadow 1s ease-out;
        }


        #divReadCSV:hover {
            background-color: lightblue;
            transition: background-color 1s ease-out;
            box-shadow: 0px 3px 5px -2px;
            transition: box-shadow 1s ease-out;
        }

        #divAddExpense:hover {
            background-color: lightgray;
            transition: background-color 1s ease-out;
            box-shadow: 0px 3px 5px -2px;
            transition: box-shadow 1s ease-out;
        }
    </style>





    <form id="form1" runat="server">


        <div>
            <!-- class="twinkling" -->

            <!-- container -->
            <div id="bg" class="clouds p-5" style="z-index: 3">




                <div id="tile" class="container-xl bg-white my-auto p-5" style="border-radius: 5px; box-shadow: 0px 0px 20px 5px;">


                    <div class="flex-row px-5 py-2 border border-dark" style="background-color:lightblue; border-radius: 5px">
                        1. Hover over any input field for a brief description<br/>
                        2. Loans will be ignored if the 'Salary To Loans' field is '0'.<br/>
                        *Note: A "Loan" can represent any expense with interest.
                    </div>


                    <div class="row mr-4">

                        <div class="col-xl-4 my-4">

                            <!-- Salary -->
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Annual Salary ($)</span>
                                </div>
                                <asp:TextBox ID="txtSalary" tooltip="Your annual salary pre-tax.&#013;This is used to determine your tax bracket (if applicable).&#013;This is used to calculate dynamic expenses so only include liquid income." CssClass="form-control" runat="server" />
                            </div>


                            <!-- Salary to Loans -->
                            <div class="input-group my-2">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Salary To Loans (%)</span>
                                </div>
                                <asp:TextBox ID="txtToLoans" ToolTip="The percentage of your monthly income (after tax) devoted to your Loan payment.&#013;*Note: This is not your minimum payment! To avoid excess interest you should allocate funds based on your income" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                        </div>



                        <!-- State Info/Selection -->
                        <div class="col-xl-6 offset-xl-2 my-4">

                            <!-- State Name -->
                            <div class="input-group d-inline-flex">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">State</span>
                                </div>

                                <!-- State Selection -->
                                <asp:DropDownList ID="ddlState" tooltip="The state you will be taxed/reside in." runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                                    <asp:ListItem Value="CA">California</asp:ListItem>
                                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                                    <asp:ListItem Value="NY">New York</asp:ListItem>
                                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <!-- Taxes Info -->
                            <div class="row mr-5 pr-5">

                                <div class="col-6">

                                    <!-- State Tax -->
                                    <div class="input-group d-inline-flex my-2">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">State Tax (%)</span>
                                        </div>
                                        <asp:TextBox ID="txtStateTax" runat="server" Enabled="false" CssClass="form-control" Style="background-color: gray"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-6">

                                    <!-- Federal Tax -->
                                    <div class="input-group d-inline-flex my-2">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Federal Tax (%)</span>
                                        </div>
                                        <asp:TextBox ID="txtFederalTax" runat="server" Enabled="false" CssClass="form-control" Style="background-color: gray"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="row">

                        <!-- Loans Info -->
                        <div class="col-xl-4 my-4">

                            <!-- Loan Amount -->
                            <div class="input-group my-2 mr-4">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Loan Amount ($)</span>
                                </div>
                                <asp:TextBox ID="txtLoans" tooltip="Your current loan amount, if you have not yet begun payments this is the full amount" CssClass="form-control" runat="server" />
                            </div>

                            <!-- Loan Interest Rate -->
                            <div class="input-group my-2">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Loan Interest Rate (%)</span>
                                </div>
                                <asp:TextBox ID="txtLoanInterest" tooltip="Your Loan's interest rate (compounded monthly)." CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-xl-5 border border-dark offset-xl-3 pb-2 pr-4" style="background-color:lightgrey">
                            <asp:Panel ID="Panel1" runat="server">
                                <!-- Monthly Payment -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Monthly Payment</span>
                                    </div>
                                    <asp:TextBox ID="txtMonthlyPayment" tooltip="The dollar amount of Loans to pay monthly based on your salary, state, and loan allocation percentage" Enabled="false" runat="server" />
                                </div>

                                <!-- Time to Pay -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Time to Pay (YY/MM)</span>
                                    </div>
                                    <asp:TextBox ID="txtTimeToPay" tooltip="The amount of time until your loan is entirely paid off. Ex: (2/5 = 2 Years, 5 Months)" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <!-- Total Paid -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Total Paid</span>
                                    </div>
                                    <asp:TextBox ID="txtTotalPaid" ToolTip="The total amount paid in loans (including accrued interest)" Enabled="false" runat="server"></asp:TextBox>
                                </div>


                                <div class="col-1-xs offset-9">
                                    <asp:Button Text="Calculate" Style="position: relative; right: 0px" class="btn btn-light border border-secondary" runat="server" ID="btnCalc" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <br />


                    <div id="moneyWrapper" class="border border-dark p-2 mt-2">

                        <canvas id="moneyChart" style="z-index: 2;" width="600" height="400"></canvas>

                        <!-- CSV Buttons -->
                        <div class="row mx-1" style="height: 10vh;">

                            <div class="col-6 d-flex justify-content-center" id="divSaveCSV" title="Download Budget" onclick="SaveCSV()">

                                <svg xmlns="http://www.w3.org/2000/svg" width="80" fill="currentColor" class="bi bi-download mx-auto my-auto" viewBox="0 0 16 16">
                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z" />
                                    <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z" />
                                </svg>
                                <asp:LinkButton ID="btnSaveCSV" runat="server" OnClick="btnSaveCSV_Click" />
                            </div>


                            <div class="col-6 d-flex justify-content-center" id="divReadCSV" onclick="PickCSV()" title="Upload Budget">

                                <svg xmlns="http://www.w3.org/2000/svg" width="80" fill="currentColor" class="bi bi-upload mx-auto my-autos" viewBox="0 0 16 16">
                                    <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z" />
                                    <path d="M7.646 1.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1-.708.708L8.5 2.707V11.5a.5.5 0 0 1-1 0V2.707L5.354 4.854a.5.5 0 1 1-.708-.708l3-3z" />
                                </svg>
                            </div>

                        </div>
                    </div>



                    <script>
                        function SaveCSV() { document.getElementById("btnSaveCSV").click(); };
                        function PickCSV() { document.getElementById("uplExpenses").click(); };
                        function ReadCSV() { document.getElementById("btnReadCSV").click(); };
                    </script>

                    <script>
                        const moneyCanvas = document.getElementById("moneyChart").getContext("2d");
                        var xLabels = [];
                        const colorPalete = ["orange", "yellow", "darkorchid", "lightcoral", "lightseagreen", "navy", "springgreen"];

                        var unspentData = '<%= Session["unspentData"] %>';
                        var loansData = '<%= Session["LoanPayments"] %>';



                        var sessionExpenses = '<%= Session["Expenses"] %>';
                        var xLabelData = '<%= Session["xLabels"] %>';

                        window.myChart = new Chart();
                    </script>



                    <!-- Expenses -->
                    <div class="col-12 pt-3">

                        <div class="d-inline-flex p-3" id="divAddExpense" onclick="addExpense()" title="Create Expense" style="border-radius: 10px">
                            <!--
                            <svg xmlns="http://www.w3.org/2000/svg" width="80" fill="currentColor" class="bi bi-plus-lg" viewBox="0 0 16 16">
                                <path d="M8 0a1 1 0 0 1 1 1v6h6a1 1 0 1 1 0 2H9v6a1 1 0 1 1-2 0V9H1a1 1 0 0 1 0-2h6V1a1 1 0 0 1 1-1z" />
                            </svg> -->

                            <img src="images/plus.svg" width="80" />

                        </div>



                        <div style="overflow: hidden; height: 0px">
                            <asp:FileUpload ID="uplExpenses" runat="server" onchange="ReadCSV()" />
                            <asp:Button ID="btnReadCSV" Text="Read CSV" runat="server" OnClick="btnReadCSV_Click" />
                            <asp:Button Text="Create Expense" runat="server" ID="btnCreateExpense" OnClick="btnCreateExpense_Click" />
                        </div>

                        <script>

                            function addExpense() { document.getElementById("btnCreateExpense").click(); };

                        </script>


                        <br />

                        <div class="row">

                            <div class="col-12">
                                <asp:Repeater ID="rptExpenses" OnItemDataBound="rptExpenseBound" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <div class="my-2 p-2" style="background-color: lightgray; border-radius: 5px; box-shadow: 0px 3px 5px -4px;">

                                            <div class="row">

                                                <div class="col-1 offset-1 h2">
                                                    <%# ((Loans_Web.Expense)Container.DataItem).Name %>
                                                </div>


                                                <div class="col-2 offset-6 d-flex justify-content-end">

                                                    <asp:LinkButton ID="btnManageExpense" class="btn btn-warning border border-dark mx-1" runat="server" CommandName="Edit"
                                                        CommandArgument='<%# ((Loans_Web.Expense)Container.DataItem).Name %>'>Edit</asp:LinkButton>

                                                    <asp:LinkButton ID="btnDeleteExpense" class="btn btn-danger border border-dark mx-1" runat="server" CommandName="Delete"
                                                        CommandArgument='<%# ((Loans_Web.Expense)Container.DataItem).Name %>'>Delete</asp:LinkButton>
                                                </div>
                                            </div>


                                            <div id="divExpenseData" class="flex-row d-flex justify-content-around" runat="server">

                                                <div class="d-inline">
                                                    <asp:Label Text="Amount:" runat="server" />
                                                    <asp:Label Text='<%# ((Loans_Web.Expense)Container.DataItem).Amount.ToString("C0") %>' runat="server" />
                                                </div>

                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>




                </div>
                <!-- Close Tile -->


            </div>

        </div>
        <!-- Close background -->

    </form>



</body>
</html>
