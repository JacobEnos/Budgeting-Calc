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
        public bool Equals(Expense other) => this.Name == other.Name;



        public class monthArgs : EventArgs {

            public string x { get; set; }
            public double y { get; set; }

            public override string ToString() => x + ":" + y.ToString();


            private void SetY(string value) { 
                double z = -1;
                Double.TryParse(value, out z);
                if (z > -1) this.y = z;
            }


            public monthArgs() {
                this.x = null;
                this.y = -1;
            }


            public monthArgs(string date, double remaining) {

                this.x = date;
                this.y = remaining;
            }

            public monthArgs(string date, string remaining) {

                this.x = date;

                double z = -1;
                Double.TryParse(remaining, out z);
                if(z > -1)
                    this.y = z;
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
            StartDate = copy.StartDate;
            EndDate = copy.EndDate;
            Payments = copy.Payments;
        }


        public Expense()
        {
            this.Name = null;
            this.Amount = 0;
            this.ToExpense = 0;
            this.recurring = false;
            this.Time = new int[2];
            StartDate = DateTime.Today;
            EndDate = null;
            Payments = new List<monthArgs>();
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
        

        public void Recur(){
            this.recurring = true;
        }



        //Construct a single Expense from the string
        public string FromString(string inputs) {

            //Separate parameters
            string[] storageString = inputs.Split(',');

            try {

                //Store params
                this.Name = storageString[0];
                this.Amount = double.Parse(storageString[1]);       //double
                this.ToExpense = double.Parse(storageString[2]);    //double
                this.recurring = bool.Parse(storageString[3]);
                this.overBudget = bool.Parse(storageString[4]);
                this.StartDate = DateTime.Parse(storageString[5]);
                this.EndDate = DateTime.Parse(storageString[6]);

                //Remove params from array
                List<string> temp = new List<string>(storageString.ToList());
                temp.RemoveRange(0, 7);
                storageString = temp.ToArray();
            }
            catch (IndexOutOfRangeException ex) {
                //Improperly formed csv   
            }
            
            
            //Re-build Payments in string
            List<monthArgs> copiedArgs = new List<monthArgs>();
            for(int i=0; i<storageString.Length-1; i+=2) {

                monthArgs mA = new monthArgs(storageString[i]);
                copiedArgs.Add(mA);
            }
            
            this.Payments = copiedArgs;

            return this.ToString();
        }



        public double ExpenseAmount(double MonthlyIncome, DateTime today)
        {
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



        public double PaymentAmount(double MonthlyIncome)
        {
            if (this.recurring){
                return this.Amount;
            }
            else{
                return MonthlyIncome * this.ToExpense;
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
