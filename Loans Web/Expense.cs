using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Loans_Web
{
    [Serializable]
    public class Expense : IEquatable<Expense>
    {

        public bool Equals(Expense other) {

            if (this.Name == other.Name) {
                return true;
            }
            return false;
        }


        public string Name;
        public double Amount;
        public double CurrentAmount;
        public double ToExpense;
        public bool recurring;
        public int[] Time;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<string> payDates;
        public double Payment;


        public Expense this[int i]{
            get{
                return this[i];
            }
            set{
                this[i] = value;
            }
        }

        public Expense(Expense copy) {

            this.Name = copy.Name;
            this.Amount = copy.Amount;
            this.ToExpense = copy.ToExpense;
            this.recurring = copy.recurring;
            this.Time = copy.Time;
            StartDate = copy.StartDate;
            EndDate = copy.EndDate;
            payDates = new List<string>();
        }


        public Expense()
        {
            this.Name = null;
            this.Amount = 0;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            StartDate = DateTime.Today;
            EndDate = DateTime.MaxValue;
            payDates = new List<string>();
        }

        public Expense(string Name)
        {
            this.Name = Name;
            this.Amount = 0;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
        }

        public Expense(double Amount)
        {
            this.Name = "";
            this.Amount = Amount;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
        }

        public Expense(string Name, double Amount)
        {
            this.Name = Name;
            this.Amount = Amount;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
        }

        public Expense(string Name, double Amount, double Percent)
        {
            this.Name = Name;
            this.Amount = Amount;
            this.ToExpense = Percent/100;
            this.recurring = false;
            this.Time = new int[2];
        }

        public Expense(double Amount, double Percent)
        {
            this.Name = "";
            this.Amount = Amount;
            this.ToExpense = Percent/100;
            this.recurring = false;
            this.Time = new int[2];
        }

        public Expense(string[] inputs) {

            FromString(inputs);
        }

        public void Recur(){
            this.recurring = true;
        }


        public override string ToString() {

            string storageString = "";

            storageString += this.Name + ",";
            storageString += this.Amount.ToString() + ",";            //double
            //storageString += this.CurrentAmount.ToString() + ",";     //double
            storageString += this.ToExpense.ToString() + ",";         //double
            storageString += this.recurring.ToString() + ",";
            //storageString += this.Time.ToString();
            storageString += this.StartDate.ToString() + ",";
            storageString += this.EndDate.ToString();
            //storageString += this.payDates.ToString();
            //storageString += this.Payment.ToString();

            return storageString;
            //return base.ToString();
        }


        public string FromString(string[] inputs) {

            string[] storageString = inputs;

            this.Name = storageString[0];
            this.Amount = double.Parse(storageString[1]);       //double
            this.ToExpense = double.Parse(storageString[2]);    //double
            this.recurring = bool.Parse(storageString[3]);
            this.StartDate = DateTime.Parse(storageString[4]);
            this.EndDate = DateTime.Parse(storageString[5]);
            
            payDates = new List<string>();

            return storageString.ToString();
        }


        public string PrintExpense(double MonthlyDisposable)
        {
            string toReturn;

            if (recurring){
                toReturn = ("Name: " + Name + "<br/>" +
                            "Recurring Payment: " + Amount.ToString("C0") + "<br/>");
                
                //If recurring Expense has a StartDate
                if (DateTime.Today <= StartDate){

                    //Print StartDate
                    toReturn += "Start: " + StartDate.ToShortDateString() + "<br/>";
                    
                    //If the recurring Expense has an EndDate
                    if (StartDate < EndDate   &&  EndDate != DateTime.MaxValue){

                        //Print EndDate
                        toReturn += "End: " + EndDate.ToShortDateString() + "<br/>";
                    }
                }
                toReturn += "<br/>";
            }
            else{
                toReturn = ("Name: " + Name + "<br/>" +
                            "Amount: " + Amount.ToString("C0") + "<br/>" +
                            "Percent: " + ToExpense * 100 + "<br/>");

                            //If there are payments
                            if(ToExpense != 0){
                                
                                //Print the payment amount
                                toReturn += "Monthly: " + PaymentAmount(MonthlyDisposable).ToString("C0") + "<br/>";
                            }

                toReturn += "<br/>";
            }
            return toReturn;
        }


        public double ExpenseAmount(double MonthlyIncome, DateTime today)
        {
            if (this.recurring){
                Payment = this.Amount;
                return this.Amount;
            }
            else if (StartDate < today  &&  today < EndDate){
                Payment = MonthlyIncome * this.ToExpense; ;
                return MonthlyIncome * this.ToExpense;
            }
            else{
                return -1;
            }
        }

        public double PaymentAmount(double MonthlyIncome)
        {
            if (this.recurring){
                Payment = this.Amount;
                return this.Amount;
            }
            else{
                Payment = MonthlyIncome * this.ToExpense;
                return MonthlyIncome * this.ToExpense;
            }
        }


        public void IncrementExpense(double MonthlyIncome, DateTime date){
            
            //if the expense is not recurring
            if (recurring == false){
    
                //and the Expense is not paid off
                if (CurrentAmount > 0){

                    //and a payment should be made
                    if (StartDate <= date  &&  
                        (date < EndDate  ||  DateTime.MaxValue == EndDate)){

                        //Subtract todays amount from this expense
                        CurrentAmount -= ExpenseAmount(MonthlyIncome, date);

                        //Record paid month
                        payDates.Add(date.ToShortDateString());
                        AddMonth();
                    }
                }   
            }

            //If the expense is recurring
            //And is occuring
            else if(StartDate <= date  &&  date < EndDate){   

                //Record paid Month
                AddMonth();
            }
        }


        public double IncrementMonth(double MonthlyIncome) {

            AddMonth();

            //if the expense is recurring
            if (recurring) return Amount;

            double toPay = ToExpense * MonthlyIncome;
            CurrentAmount -= toPay;
            return toPay;
        }



        public void AddMonth(){

            //Count payments
            if (Time[1] >= 11){

                //convert to a year
                Time[1] = 0;
                Time[0]++;
            }
            else{
                //else add a month
                Time[1]++;
            }
        }

        public void Clear()
        {
            this.Name = null;
            this.Amount = 0;
            this.ToExpense = 0;
            this.Time = new int[2];
        }
    }
}
