using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AddBook : Form
    {

        HttpClient client;
        private readonly string url = "https://localhost:44361";
        public AddBook()
        {
            InitializeComponent();

            client = new HttpClient();
        }

        private void AddBook_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book = new Book()
            {
                title = textBox1.Text,
                ddc = textBox2.Text,
                isbn = textBox3.Text,
                created = dateTimePicker1.Value
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            client.PostAsync($"{url}/calc/addbook", httpContent);

        }
    }
}
