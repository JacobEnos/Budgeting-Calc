<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Loans_Web.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Budget-Calc</title>

    <!-- <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>  -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.3.2/chart.min.js" integrity="sha512-VCHVc5miKoln972iJPvkQrUYYq7XpxXzvqNfiul1H4aZDwGBGC0lq373KNleaB2LpnC2a/iNfE5zoRYmB4TRDQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous" />

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


        <div> <!-- class="twinkling" -->

            <!-- container -->
            <div id="bg" class="clouds p-5" style="z-index: 3">




                <div id="tile" class="container-xl bg-white my-auto p-5" style="border-radius: 5px; box-shadow: 0px 0px 20px 5px;">





                    <div class="row mr-4">

                        <div class="col-xl-4 my-4">

                            <!-- Salary -->
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Salary ($)</span>
                                </div>
                                <asp:TextBox ID="txtSalary" CssClass="form-control" runat="server" OnTextChanged="txtSalary_TextChanged" AutoPostBack="true" />
                            </div>


                            <!-- Salary to Loans -->
                            <div class="input-group my-2">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Salary To Loans (%)</span>
                                </div>
                                <asp:TextBox ID="txtToLoans" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
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
                                <asp:TextBox ID="txtLoans" CssClass="form-control" runat="server" />
                            </div>

                            <!-- Loan Interest Rate -->
                            <div class="input-group my-2">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Loan Interest Rate(%)</span>
                                </div>
                                <asp:TextBox ID="txtLoanInterest" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-xl-6 offset-xl-2 pr-4">
                            <asp:Panel ID="Panel1" runat="server">
                                <!-- Monthly Payment -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Monthly Payment</span>
                                    </div>
                                    <asp:TextBox ID="txtMonthlyPayment" Enabled="false" runat="server" />
                                </div>

                                <!-- Time to Pay -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Time to Pay (YY/MM)</span>
                                    </div>
                                    <asp:TextBox ID="txtTimeToPay" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <!-- Total Paid -->
                                <div class="input-group my-2 justify-content-end">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Total Paid</span>
                                    </div>
                                    <asp:TextBox ID="txtTotalPaid" Enabled="false" runat="server"></asp:TextBox>
                                </div>


                                <div class="col-1-xs offset-9">
                                    <asp:Button Text="Calculate" Style="position: relative; right: 0px" class="btn btn-light border border-secondary" runat="server" ID="btnCalc" OnClick="btnCalc_Click" />
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
                        "use strict"

                        function SaveCSV() { document.getElementById("btnSaveCSV").click(); };

                        function PickCSV() { document.getElementById("uplExpenses").click(); };
                        function ReadCSV() { document.getElementById("btnReadCSV").click(); };

                    </script>





                    <script>
                        "use strict"

                        const moneyCanvas = document.getElementById("moneyChart").getContext("2d");
                        var xLabels = [];
                        const colorPalete = ["orange", "yellow", "darkorchid", "lightcoral", "lightseagreen", "navy", "springgreen"];


                        var jsonData;
                        //Gets Unspent data
                        function GetSessionData() {

                            jsonData = '<%= Session["json"] %>';

                            if (jsonData != null && jsonData != "") {
                                jsonData = JSON.parse(jsonData);
                            }

                        };
                        //GetSessionData();


                        //Gets Loan payment data
                        var loansData;
                        function GetLoansData() {

                            loansData = '<%= Session["LoanPayments"] %>';

                            if (loansData != null && loansData != "") {
                                loansData == null;
                                loansData = JSON.parse(loansData);
                            }
                        };
                        //GetLoansData();


                        //Get Expense payment data
                        var sessionExpenses = [];
                        function GetExpenseData() {

                            //Get Expenses
                            sessionExpenses = '<%= Session["Expenses"] %>';

                            if (sessionExpenses != null && sessionExpenses != "") {
                                sessionExpenses = JSON.parse(sessionExpenses);
                            }
                        };
                        //GetExpenseData();


                        //Gets Chronological labels
                        var xLabelData = [];
                        function GetxLabelData() {

                            xLabelData = '<%= Session["xLabels"] %>';

                            if (xLabelData != null && xLabelData != "") {
                                xLabelData = JSON.parse(xLabelData);

                            }

                            console.log("These dates in order?");
                            console.log(xLabelData);
                        };
                        //GetxLabelData();




                        //Gets Expense data
                        const exLines = [];
                        function GetSessionExpenses() {

                            //Unspent Data
                            const graph1 = {
                                label: 'Unspent',
                                data: jsonData,
                                borderColor: "lightgreen",
                                backgroundColor: "green"
                            };

                            //Loans Data
                            const loansGraph = {
                                label: 'Loans',
                                data: loansData,
                                borderColor: "red",
                                backgroundColor: "red"
                            };

                            exLines.push(graph1);
                            exLines.push(loansGraph);


                            var expenseIndex = 0;
                            for (expenseIndex in sessionExpenses) {

                                const thisExpense = {
                                    label: sessionExpenses[expenseIndex].Name,
                                    start: sessionExpenses[expenseIndex].StartDate,
                                    end: sessionExpenses[expenseIndex].EndDate,
                                    amount: sessionExpenses[expenseIndex].Amount,
                                    payment: sessionExpenses[expenseIndex].Payment,
                                    data: [],
                                    recurring: sessionExpenses[expenseIndex].recurring,
                                    borderColor: colorPalete[expenseIndex],
                                    backgroundColor: colorPalete[expenseIndex],
                                    Payments: sessionExpenses[expenseIndex].Payments
                                };

                                thisExpense.data = thisExpense.Payments;
                                exLines.push(thisExpense);
                            }

                        };
                        //GetSessionExpenses();





                        window.myChart = new Chart();



                        function AjaxGetAndGraph() {

                            GetSessionData();
                            GetLoansData();
                            GetExpenseData();
                            GetxLabelData();
                            GetSessionExpenses();

                            console.log("Data received by AJAX");
                            console.log(exLines);


                            console.log("Expenses Received:");
                            console.log(sessionExpenses);

                            const graphData = {
                                labels: xLabelData,
                                datasets: exLines
                            };


                            const config = {
                                type: 'line',
                                data: graphData,

                                options: {
                                    responsive: true,
                                    plugins: {
                                        legend: {
                                            position: 'top',
                                        },
                                        title: {
                                            display: true,
                                            text: 'Budget Breakdown'
                                        }
                                    }
                                }
                            };


                            if (window.myChart != null) {
                                window.myChart.destroy();
                                console.log("destroyed");
                            }

                            myChart = new Chart(
                                moneyCanvas,
                                config
                            );

                        };


                            //myChart.defaults.global.defaultFontFamily = "Lato";
                            //myChart.defaults.global.defaultFontSize = 18;

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
