using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loans_Web
{

    public class TaxLadder {

        List<TaxBracket> taxBrackets;

        public TaxLadder() => taxBrackets = new List<TaxBracket>();

        public double this[double i] {
            get {
                foreach (TaxBracket tb in taxBrackets) {
                    if (i <= tb.maxSalary)
                        return tb.Tax;
                }
                return taxBrackets[taxBrackets.Count()-1].Tax;
            }
        }


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
