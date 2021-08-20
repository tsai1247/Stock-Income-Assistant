using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stock_Income_Assistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculate();
        }

        private float toFloat(TextBox tb)
        {
            if (tb == null)
                throw new Exception();
            if (tb.Text == "")
                return 0;
            return float.Parse(tb.Text);
        }

        private float toFloat(string str)
        {
            return float.Parse(str);
        }

        private int toInt(TextBox tb)
        {
            if (tb == null)
                throw new Exception();
            if (tb.Text == "")
                return 0;
            return int.Parse(tb.Text);
        }
        private int toInt(string str)
        {
            return int.Parse(str);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            calculate();
        }

        private void calculate()
        {
            try
            {
                float input = toFloat(input_price) * toFloat(input_num) * toFloat(service_fee);
                if (input < toInt(min_service_fee))
                    input = toInt(min_service_fee);
                input += toFloat(input_price) * toFloat(input_num);

                float output = input;
                output += toFloat(income_num);

                float output1 = output;
                output1 /= 1 - toFloat(service_fee) - toFloat(tax_fee);

                float output2 = output;
                output2 += toFloat(min_service_fee);
                output2 /= 1 - toFloat(tax_fee);

                bool check1 = output1 * toFloat(service_fee) >= toFloat(min_service_fee);
                bool check2 = output2 * toFloat(service_fee) <= toFloat(min_service_fee);


                int outnum = toInt(input_num);
                float outprice;

                if (check1 && check2)
                {
                    outprice = MathF.Min(output1, output2) / outnum;
                }
                else if (check1)
                {
                    outprice = output1 / outnum;
                }
                else if (check2)
                {
                    outprice = output2 / outnum;
                }
                else
                {
                    outprice = float.MaxValue;
                }



                output_num.Text = outnum.ToString();
                output_price.Text = String.Format("{0:0.##}", outprice);

                totalinput.Content = String.Format("{0:0.##} -> {1:0.##}", toFloat(input_price) * toFloat(input_num), input);
                totaloutput.Content = String.Format("{0:0.##} -> {1:0.##}"
                    , outprice * outnum
                    , outprice * outnum * (1 - toFloat(tax_fee)) - MathF.Max(outprice * outnum * toFloat(service_fee), toFloat(min_service_fee)));
            }
            catch(Exception)
            {
            }
        }
    }
}
