"use strict"


//var unspentData;
//Gets Unspent data
function GetSessionData() {

    //unspentData = '<%= Session["json"] %>';

    if (unspentData != null && unspentData != "") {
        unspentData = JSON.parse(unspentData);
    }
};



//Get Expense payment data
//var sessionExpenses = [];
function GetExpenseData() {

    //Get Expenses
    //sessionExpenses = '<%= Session["Expenses"] %>';

    if (sessionExpenses != null && sessionExpenses != "") {
        sessionExpenses = JSON.parse(sessionExpenses);
    }
};



//Gets Chronological labels
//var xLabelData = [];
/*
function GetxLabelData() {

    //xLabelData = '<%= Session["xLabels"] %>';

    if (xLabelData != null && xLabelData != "") {
        xLabelData = JSON.parse(xLabelData);
    }
};
*/


//Gets Expense data
const exLines = [];
function GetSessionExpenses() {

    //Unspent Data
    const unspentLine = {
        label: 'Unspent',
        data: unspentData,
        borderColor: "lightgreen",
        backgroundColor: "green"
    };
    exLines.push(unspentLine);


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
            overBudget: sessionExpenses[expenseIndex].overBudget,
            borderColor: colorPalete[expenseIndex],
            backgroundColor: colorPalete[expenseIndex],
            Payments: sessionExpenses[expenseIndex].Payments
        };

        thisExpense.data = thisExpense.Payments;
        exLines.push(thisExpense);
    }

};



//window.myChart = new Chart();



function AjaxGetAndGraph() {

    GetSessionData();
    GetExpenseData();
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

    sessionStorage.clear();
    
};





