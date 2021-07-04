using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_Web
{

    public class State {
        public string Name;
        public List<TaxLadder.TaxBracket> taxes;
        public double[] Salaries;
        public double[] Taxes;

        public State() {
        }

        public State(string name) {
            Name = name;
        }

        public State(string name, double[] salaries) {
            Name = name;
            Salaries = salaries;
        }

        public State(string name, double[] salaries, double[] taxes) {
            Name = name;
            Salaries = salaries;
            Taxes = taxes;
        }
        /*
        public double getTax(double salary) {
            //If the State is populated
            if (Salaries != null) {

                int i = 0;
                //Find tax bracket
                foreach (int s in Salaries) {

                    //If this is your tax bracket
                    if (salary <= s) {

                        //return tax rate
                        return Taxes[i] / 100;
                    }
                    i++;
                }

                //and no tax bracket was found
                //Return the highest tax 
                return Taxes[Taxes.Length - 1] / 100;
            }
            //If state is not populated
            else {
                return -1;
            }
        }

        /*
        public double this[int i] {

            get {
                foreach (TaxBracket tb in taxes) {
                    if (i <= tb.maxSalary)
                        return tb.Tax;
                }

                //If no tax bracket was found, return the highest tax 
                return -1;
            }
        }
        */






    }



    public class TaxLadder {

        List<TaxBracket> taxBrackets;

        public double this[double i] {
            get {
                foreach (TaxBracket tb in taxBrackets) {
                    if (i <= tb.maxSalary)
                        return tb.Tax;
                }

                //If no tax bracket was found, return the highest tax 
                return taxBrackets[taxBrackets.Count()-1].Tax;
            }
        }

        public TaxLadder() => taxBrackets = new List<TaxBracket>();


        public TaxLadder(double[] salaries, double[] taxes) {

            List<TaxBracket> toSet = new List<TaxBracket>();
            for (int i = 0; i < salaries.Length; i++) {
                toSet.Add(new TaxBracket(salaries[i], taxes[i]));
            }

            this.taxBrackets = toSet;
        }






        public class TaxBracket {

            public double maxSalary;
            private double taxRate;
            public double Tax {
                get => taxRate;
                set => taxRate = value / 100;
            }

            public TaxBracket() {
                maxSalary = -1;
                Tax = -1;
            }

            public TaxBracket(double maxSalary, double taxPercent) {
                this.maxSalary = maxSalary;
                this.Tax = taxPercent;
            }


        }





    }




}
