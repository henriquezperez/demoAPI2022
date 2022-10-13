using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetAPIdemo
{
    public partial class Form1 : Form
    {


        string url = "https://jsonplaceholder.typicode.com/posts";
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "API: " + url;
        }

        public async Task<string> GetRest()
        {
            WebRequest rest = WebRequest.Create(url);
            WebResponse response = rest.GetResponse();
            /*if(response == null)
            {
                MessageBox.Show("Error");
            }*/
            StreamReader sr = new StreamReader(response.GetResponseStream());  
            return await sr.ReadToEndAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string res = "";
            Info frm = new Info();
            frm.Show();
            //string res = await GetRest();
            try
            {
                res = await GetRest();
            }
            catch
            {
                frm.Close();
                MessageBox.Show("Error de petición");
            }
            Thread.Sleep(500);
            frm.Close();
            if(res != string.Empty)
            {
                List<PostViewModel> list = JsonConvert.DeserializeObject<List<PostViewModel>>(res);
                dataGridView1.DataSource = list;
                MessageBox.Show("Petición exitosa");
            }
            else
            {
                MessageBox.Show("Error: No hay conexión a internet o no existe el recurso..." + "\n"+"API: " + url);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }
    }
}
