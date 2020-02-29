﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MyClientApp
{
    public partial class Form1 : Form
    {

        localhost.WebService1 proxy = new localhost.WebService1();
        HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void WebServicesSettings()
        {
            client.BaseAddress = new Uri("https://localhost:44320/WebService1.asmx/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string countriesJson = proxy.Countries();
            //DataTable dtCountries = JsonConvert.DeserializeObject<DataTable>(countriesJson);
            //dataGridView1.DataSource = dtCountries;

            WebServicesSettings();
        }

        private DataTable stringSplit(string userJson)
        {
            string[] json = userJson.Split('>');
            string[] finalJson = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJson[0]);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpResponseMessage message = client.GetAsync("dataTableForUsers?id=" + textBox1.Text + "").Result;
            string userJson = message.Content.ReadAsStringAsync().Result;
            MessageBox.Show(userJson);
            dataGridView1.DataSource = stringSplit(userJson);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
