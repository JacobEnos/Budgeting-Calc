using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Loans_Web
{
    [Serializable]
    public class Expense : IEquatable<Expense> {
        public bool Equals(Expense other) => this.Name == other.Name;



        public class monthArgs : EventArgs {

            public string x { get; set; }
            private double Y;
            public double y {
                get {return Y;}
                set { Y = Math.Truncate(value);}
            }

            public override string ToString() => x + ":" + y.ToString("C0");

            //Set y with a string
            private void SetY(string value) { 
                double z = -1;
                Double.TryParse(value, out z);
                if (0 < z) y = z;
            }

            public monthArgs() {
                this.x = null;
                y = -1;
            }

            public monthArgs(string date, double remaining) {
                this.x = date;
                y = remaining;
            }

            public monthArgs(string date, string remaining) {
                this.x = date;
                SetY(remaining);
            }

            public monthArgs(string data) {
                string[] input = data.Split(':');
                this.x = input[0];
                SetY(input[1]);
            }
        }




        public string Name;
        public double Amount;
        public double CurrentAmount;
        public double ToExpense;
        public bool recurring;
        public bool overBudget = false;
        private double _interest = 0;
        public double Interest {
            get => (_interest > 0) ? _interest : 0;
            set => _interest = (0 < value && value < 1) ? value : -1;
        }
        public int[] Time;
        public DateTime StartDate;
        public DateTime? EndDate;
        public List<monthArgs> Payments;

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
            this.Interest = copy.Interest;
            StartDate = copy.StartDate;
            EndDate = copy.EndDate;
            Payments = copy.Payments;
        }


        public Expense(){
            this.Name = null;
            this.Amount = 0;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
            StartDate = DateTime.Today;
            EndDate = null;
            Payments = new List<monthArgs>();
        }

        public Expense(string Name){
            this.Name = Name;
            this.Amount = 0;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
        }

        public Expense(double Amount){
            this.Name = "";
            this.Amount = Amount;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
        }

        public Expense(string Name, double Amount){
            this.Name = Name;
            this.Amount = Amount;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
        }

        public Expense(string Name, double Amount, double Percent){
            this.Name = Name;
            this.Amount = Amount;
            this.ToExpense = Percent/100;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
        }

        public Expense(double Amount, double Percent){
            this.Name = "";
            this.Amount = Amount;
            this.ToExpense = Percent/100;
            this.recurring = false;
            this.Time = new int[2];
            this.Interest = 0;
        }
        

        public void Recur(){
            this.recurring = true;
        }


        public double PaymentAmount(double monthlyIncome) => (this.recurring) ? this.Amount : (monthlyIncome * this.ToExpense);


        public double ExpenseAmount(double MonthlyIncome, DateTime today){

            if (this.recurring){
                return this.Amount;
            }
            else if (StartDate < today  &&  today < EndDate){
                return MonthlyIncome * this.ToExpense;
            }
            else{
                return -1;
            }
        }
        

        public double IncrementMonth(double MonthlyIncome, DateTime today) {

            AddMonth();

            //if the expense is recurring
            if (recurring) {

                Payments.Add(new monthArgs(today.ToString("MM/yyyy"), Amount));
                return Amount;
            }

            double toPay = ToExpense * MonthlyIncome;

            if (CurrentAmount < toPay) {
                toPay = CurrentAmount;
            }

            CurrentAmount -= toPay;
            Payments.Add(new monthArgs(today.ToString("MM/yyyy"), toPay));

            //Add Interest
            if (Interest > 0) CurrentAmount *= (1 + Interest/12);

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
            this.Payments.Clear();
        }
    }
}
