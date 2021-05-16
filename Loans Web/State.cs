using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loans_Web
{
    public class State
    {
        public string Name;
        public double[] Salaries;
        public double[] Taxes;

        public State()
        {
           
        }

        public State(string name){
            Name = name;
        }

        public State(string name, double[] salaries)
        {
            Name = name;
            Salaries = salaries;
        }

        public State(string name, double[] salaries, double[] taxes)
        {
            Name = name;
            Salaries = salaries;
            Taxes = taxes;
        }

        public double getTax(double salary)
        {
            //If the State is populated
            if(Salaries != null){
                
                int i = 0;
                //Find tax bracket
                foreach (int s in Salaries){
                    
                    //If this is your tax bracket
                    if (salary <= s){
                        
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
            else{
                return -1;
            }
            
        }


    }
}
