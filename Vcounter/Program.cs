using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Vcounter
{
    class Program
    {
        
        public static double d;
        public static List<double> L = new List<double>();
        public static List<double> V = new List<double>();
        public static List<double> AM = new List<double>();
        public static List<double> AN = new List<double>();
        public static List<double> BM = new List<double>();
        public static List<double> BN = new List<double>();
        public static List<double> Vres = new List<double>();
      
        static void Main(string[] args)
        {
           // L = new List<float>();
           // V = new List<float>();
            getdist();
            getmns();
            calculatethisshit();
            for(int i = 0; i < AN.Count; i++)
            Console.WriteLine(V[i]);
            Console.ReadLine(); //Pause
        }

        // Reads Curve0, gets all L an V
        static void getdist()
        {
            int i = 0;
            string curveText = System.IO.File.ReadAllText("../Curve0");
            while (curveText[i] != '(') i++;
            string dist="";
            i++;
            while (curveText[i] != ',')
            {
                dist+=curveText[i];
                i++;
            }
            d = float.Parse(dist);
            while (curveText[i] != 'T')
                i++;
            i+=3;
           // Console.WriteLine(curveText[i]);
            string temp = "";
            for (int j = i; j < curveText.Length; j++)
                temp += curveText[j];
            string[] values = temp.Split('\r', '\n');
               for (int j = 0; j < values.Length; j+=2)
               {

                //Console.WriteLine(noempties.Length);
                if(values[j] != "")
                {
                    string[] noempties = values[j].Split('\t');
                    L.Add(double.Parse(noempties[0], CultureInfo.InvariantCulture));
                    V.Add(double.Parse(noempties[1], CultureInfo.InvariantCulture));
                }
            }
            }
        // Reads excel file, gets all distances fo recievers
        static void getmns()
        {
            string vals = System.IO.File.ReadAllText("../anambnbm.txt");
            string[] vstr = vals.Split('\n');
            for (int i =0; i < vstr.Length; i++)
            {
                if ( vstr[i] != "")
                {

                    string[] morn = vstr[i].Split('\t');
                    if (i % 2 == 0)
                    {
                        AM.Add(double.Parse(morn[0]));
                        BM.Add(double.Parse(morn[1]));
                    }
                    else
                    {
                        AN.Add(double.Parse(morn[0]));
                        BN.Add(double.Parse(morn[1]));
                    }

                }
            }
        }
        //Сalculations
        static void calculatethisshit()
        {
            double Van = 0, Vam = 0, Vbm = 0, Vbn = 0;
            for(int i =0; i < AN.Count; i++)
            {
                // AN
                for(int j = 1; j < L.Count; j ++)
                {
                    if (L[j] + d >= AN[i])
                    {
                        // Solve triangle
                        double AE, BC, OE, AC;
                        AE = AN[i] - L[j - 1];
                        BC = V[j] - V[j - 1];
                        AC = L[j] - L[j - 1];
                        OE = AE * BC / AC;
                        Van= OE + V[j - 1];
                    }
                }
                // BN
                for(int j = 1; j < L.Count; j ++)
                {
                    if (L[j] + d >= BN[i])
                    {
                        // Solve triangle
                        double AE, BC, OE, AC;
                        AE = AN[i] - L[j - 1];
                        BC = V[j] - V[j - 1];
                        AC = L[j] - L[j - 1];
                        OE = AE * BC / AC;
                        Vbn= OE + V[j - 1];
                    }
                }
                // AM
                for(int j = 1; j < L.Count; j ++)
                {
                    if (L[j] + d >= AM[i])
                    {
                        // Solve triangle
                        double AE, BC, OE, AC;
                        AE = AN[i] - L[j - 1];
                        BC = V[j] - V[j - 1];
                        AC = L[j] - L[j - 1];
                        OE = AE * BC / AC;
                        Vam= OE + V[j - 1];
                    }
                }
                // BM
                for(int j = 1; j < L.Count; j ++)
                {
                    if (L[j] + d >= BM[i])
                    {
                        // Solve triangle
                        double AE, BC, OE, AC;
                        AE = AN[i] - L[j - 1];
                        BC = V[j] - V[j - 1];
                        AC = L[j] - L[j - 1];
                        OE = AE * BC / AC;
                        Vbm= OE + V[j - 1];
                    }
                }

                // Sum all Vs
                double Vres = Vam - Vbm - Van + Vbn;
                V.Add(Vres);
            }
        }
    }
}
