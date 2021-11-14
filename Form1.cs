using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Method_of_calculation_5
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }
        double a = 0, b = Math.PI / 4;
        double c, d;
        int n = 2;
        MyPoint[] arr;
        double h;
        double[] x;
        class MyPoint
        {
            public double y, u, nu;
        }

        private void Result_Button_Click(object sender, EventArgs e)
        {
            c = Math.Sin(a) + Math.Cos(a);
            d = Math.Sin(b) + Math.Cos(b);

            while (n <= 16384)
            {
                x = new double[n + 1];
                h = (b - a) / n;
                for (int i = 1; i < x.Length; i++)
                {
                    x[i] = i * h;
                }
                Calculate();
                n *= 2;
            }
        }
        void Calculate()
        {
            arr = new MyPoint[n + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new MyPoint();
            }

            arr[0].nu = -(1 / (1 * h - 1));
            arr[0].u = (h * c) / (1 * h - 1);

            for (int i = 1; i < arr.Length; i++)
            {
                double p = 1 / (x[i] * x[i] - 1);
                double q = 1 / Math.Sqrt(1 - x[i] * x[i]);
                double f = -Math.Sin(x[i]) + p * Math.Cos(x[i]) + q * Math.Sin(x[i]);
                double alpha, beta, fi;
                if (i == n)
                {
                    alpha = -1;
                    beta = 1 * h + 1;
                    fi = h * d;
                }
                else
                {
                    alpha = 1 - ((p * h) / 2);
                    beta = -2 + q * h * h;
                    fi = h * h * f;
                }

                double gamma = 1 + ((p * h) / 2);
                arr[i].u = (fi - alpha * arr[i - 1].u) / (beta + alpha * arr[i - 1].nu);
                arr[i].nu = -gamma / (beta + alpha * arr[i - 1].nu);
            }
            arr[n].y = arr[n].u;

            for (int i = n - 1; i >= 0; i--)
            {
                arr[i].y = arr[i].u + arr[i].nu * arr[i + 1].y;
            }

            dataGridView1.Rows.Add(n, Math.Sin(0) - arr[0].y, Math.Sin(Math.PI / 8) - arr[arr.Length / 2].y, Math.Sin(Math.PI / 4) - arr[n].y);
        }
    }
}
