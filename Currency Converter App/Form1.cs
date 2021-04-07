using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace Currency_Converter_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindCurrency();
        }

        private double getRate(string fromCurrency, string toCurrency)
        {
            var json = "";
            double rate;
            try
            {
                string url = string.Format("https://free.currconv.com/api/v7/convert?q={0}_{1}&compact=ultra&apiKey=cd0304f3d4c6b7e923d3", fromCurrency.ToUpper(), toCurrency.ToUpper());
                string key = string.Format("{0}_{1}", fromCurrency.ToUpper(), toCurrency.ToUpper());

                json = new WebClient().DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(json);
                rate = stuff[key];
            }
            catch
            {
                rate = 0;
            }
            return rate;
        }

        private void BindCurrency()
        {
            DataTable dtCurr = new DataTable();
            dtCurr.Columns.Add("Text");

            dtCurr.Rows.Add("--SELECT--");
            dtCurr.Rows.Add("IDR");
            dtCurr.Rows.Add("INR");
            dtCurr.Rows.Add("USD");
            dtCurr.Rows.Add("EUR");
            dtCurr.Rows.Add("SAR");
            dtCurr.Rows.Add("BAM");
            dtCurr.Rows.Add("PYG");
            dtCurr.Rows.Add("TRY");
            dtCurr.Rows.Add("JPY");

            fromCurr.DataSource = dtCurr.DefaultView;
            fromCurr.DisplayMember = "Text";
            fromCurr.SelectedIndex = 0;

            toCurr.DataSource = dtCurr.DefaultView;
            toCurr.DisplayMember = "Text";
            toCurr.SelectedIndex = 0;
        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            double ConvertedValue;

            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Currency");
                txtCurrency.Focus();
                return;
            }

            if (fromCurr.Text == toCurr.Text)
            {
                ConvertedValue = double.Parse(txtCurrency.Text);
                this.result.Text = ConvertedValue.ToString();
            }

            else
            {
                double rate = getRate(fromCurr.Text, toCurr.Text);
                ConvertedValue = (rate * double.Parse(txtCurrency.Text));
                this.result.Text = ConvertedValue.ToString();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            txtCurrency.Clear();
            result.Text = "";
            toCurr.SelectedIndex = 0;
            fromCurr.SelectedIndex = 0;
        }
    }
    
}
