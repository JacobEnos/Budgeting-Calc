<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Loans_Web.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Budget-Calc</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous">
</head>
<body>

    <style>
        body {
            margin: 0;
            background-color: #17182f;
        }

        canvas {
            display: block;
            vertical-align: bottom;
        }


        /* ---- particles.js container ---- */

        #particles-js {
            position: absolute;
            width: 100%;
            height: 100%;
        }
    </style>

    <form id="form1" runat="server">

        <!-- particles.js container -->
        <div id="particles-js" class="mt-5" style="z-index: -1">

            <!-- particles.js lib - https://github.com/VincentGarreau/particles.js -->
            <script src="https://cdn.jsdelivr.net/particles.js/2.0.0/particles.min.js"></script>

            <!-- <div id="wrapper" class="flex-container d-flex vh-100"> -->



                <div id="tile" class="bg-white mx-auto my-auto p-5 w-50" style="border-radius: 5px; box-shadow: 0px 0px 20px 5px;">

                    <div class="row">
                        <!-- Salary -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Salary</span>
                            </div>
                            <asp:TextBox ID="txtSalary" CssClass="form-control" runat="server" />
                        </div>


                        <!-- Salary to Loans -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Salary To Loans(%)</span>
                            </div>
                            <asp:TextBox ID="txtToLoans" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>



                    <!-- States -->
                    <div class="row">

                        <!-- State Name -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">State</span>
                            </div>

                            <asp:DropDownList ID="ddlState" runat="server">
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

                        <!-- State Tax -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">State Tax(%)</span>
                            </div>
                            <asp:TextBox ID="txtStateTax" runat="server" Enabled="false" Style="background-color: gray"></asp:TextBox>
                        </div>


                        <!-- Federal Tax -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Federal Tax(%)</span>
                            </div>
                            <asp:TextBox ID="txtFederalTax" runat="server" Enabled="false" Style="background-color: gray"></asp:TextBox>
                        </div>


                    </div>


                    <br />


                    <!-- Loans Info -->
                    <div class="row my-4">

                        <!-- Loan Amount -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Loan Amount</span>
                            </div>
                            <asp:TextBox ID="txtLoans" CssClass="form-control" runat="server" />
                        </div>

                        <!-- Loan Interest Rate -->
                        <div class="input-group w-50 my-2">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Loan Interest Rate(%)</span>
                            </div>
                            <asp:TextBox ID="txtLoanInterest" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>


                    <!-- Expenses -->
                    <div class="bg-white">

                        <asp:Button Text="Create Expense" runat="server" ID="btnCreateExpense" OnClick="btnCreateExpense_Click"/>

                        <asp:Repeater ID="rptExpenses" runat="server"></asp:Repeater>

                        <asp:Label ID="lblExpenseResults" runat="server"/>


                    </div>



                    <!-- Payment Details -->
                    <div class="my-4" style="position: relative; right: 0px">

                        <!-- Monthly Payment -->
                        <div class="input-group my-2 justify-content-end">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Monthly Payment</span>
                            </div>
                            <asp:TextBox ID="txtMonthlyPayment" Enabled="false" runat="server"></asp:TextBox>
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



                    </div>


                    <div class="col-1-xs offset-10">
                        <asp:Button Text="Calculate" Style="position: relative; right: 0px" class="btn btn-light border border-secondary" runat="server" />
                    </div>

                </div>
                <!-- Close Tile -->

            <!-- </div> -->
        </div>
        <!-- Close background -->

    </form>



    <script>
        particlesJS("particles-js", {
            "particles": {
                "number": {
                    "value": 355,
                    "density": {
                        "enable": true,
                        "value_area": 789.1476416322727
                    }
                },
                "color": {
                    "value": "#ffffff"
                },
                "shape": {
                    "type": "circle",
                    "stroke": {
                        "width": 0,
                        "color": "#000000"
                    },
                    "polygon": {
                        "nb_sides": 5
                    },
                    "image": {
                        "src": "img/github.svg",
                        "width": 100,
                        "height": 100
                    }
                },
                "opacity": {
                    "value": 0.48927153781200905,
                    "random": false,
                    "anim": {
                        "enable": true,
                        "speed": 0.2,
                        "opacity_min": 0,
                        "sync": false
                    }
                },
                "size": {
                    "value": 2,
                    "random": true,
                    "anim": {
                        "enable": true,
                        "speed": 2,
                        "size_min": 0,
                        "sync": false
                    }
                },
                "line_linked": {
                    "enable": false,
                    "distance": 150,
                    "color": "#ffffff",
                    "opacity": 0.4,
                    "width": 1
                },
                "move": {
                    "enable": true,
                    "speed": 0.2,
                    "direction": "none",
                    "random": true,
                    "straight": false,
                    "out_mode": "out",
                    "bounce": false,
                    "attract": {
                        "enable": false,
                        "rotateX": 600,
                        "rotateY": 1200
                    }
                }
            },
            "interactivity": {
                "detect_on": "canvas",
                "events": {
                    "onhover": {
                        "enable": true,
                        "mode": "bubble"
                    },
                    "onclick": {
                        "enable": true,
                        "mode": "push"
                    },
                    "resize": true
                },
                "modes": {
                    "grab": {
                        "distance": 400,
                        "line_linked": {
                            "opacity": 1
                        }
                    },
                    "bubble": {
                        "distance": 83.91608391608392,
                        "size": 1,
                        "duration": 3,
                        "opacity": 1,
                        "speed": 3
                    },
                    "repulse": {
                        "distance": 200,
                        "duration": 0.4
                    },
                    "push": {
                        "particles_nb": 4
                    },
                    "remove": {
                        "particles_nb": 2
                    }
                }
            },
            "retina_detect": true
        });

    </script>




</body>
</html>
