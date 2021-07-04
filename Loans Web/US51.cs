using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loans_Web {
    public static class US51 {

        public static readonly Dictionary<string, TaxLadder> stateDict = new Dictionary<string, TaxLadder> {

            {"AL", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"AK", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"AZ", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"AR", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"CA", new TaxLadder(new double[]{8809, 20883, 32960, 45753, 57824, 295373, 354445, 590742, 1000000}, new double[]{1, 2, 4, 6, 8, 9.3, 10.3, 11.3, 12.3, 13.3})},
            {"CO", new TaxLadder(new double[]{8809}, new double[]{1})},
            {"CT", new TaxLadder(new double[]{10000, 50000, 100000, 200000, 250000, 500000}, new double[]{3, 5, 5.5, 6, 6.5, 6.9, 6.99})},
            {"DC", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"DE", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"FL", new TaxLadder(new double[]{0}, new double[]{0})},
            {"GA", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"HI", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"ID", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"IL", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"IN", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"IA", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"KS", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"KY", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"LA", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"ME", new TaxLadder(new double[]{22200, 52600}, new double[]{5.8, 6.75, 7.15})},
            {"MD", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"MA", new TaxLadder(new double[]{0}, new double[]{5})},
            {"MI", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"MN", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"MS", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"MO", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"MT", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NE", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NV", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NH", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NJ", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NM", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"NY", new TaxLadder(new double[]{8500, 11700, 13900, 21400, 80650, 215400, 1077550}, new double[]{4, 4.5, 5.25, 5.9, 6.21, 6.49, 6.85, 8.82})},
            {"NC", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"ND", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"OH", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"OK", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"OR", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"PA", new TaxLadder(new double[]{0}, new double[]{3.7})},
            {"RI", new TaxLadder(new double[]{65250, 148350}, new double[]{3.75, 4.75, 5.99})},
            {"SC", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"SD", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"TN", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"TX", new TaxLadder(new double[]{0}, new double[]{0})},
            {"UT", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"VT", new TaxLadder(new double[]{39600, 96000, 200200}, new double[]{3.35, 6.6, 7.6, 8.75})},
            {"VA", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"WA", new TaxLadder(new double[]{0}, new double[]{0})},
            {"WV", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"WI", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"WY", new TaxLadder(new double[]{0}, new double[]{-1})},
            {"AL", new TaxLadder(new double[]{0}, new double[]{-1})},
        };

        public static TaxLadder GetState(string stateAbv) => stateDict[stateAbv];
    }
}