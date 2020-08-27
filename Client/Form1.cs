using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Client
{
    [Serializable]
    public class Book
    {
        public string title { get; set; }
        public string isbn { get; set; }
        public string ddc { get; set; }
        public DateTime created { get; set; }
    }
    public partial class Form1 : Form
    {
        HttpClient client;
        private readonly string url = "https://localhost:44361";
        public Form1()
        {
            InitializeComponent();

            client = new HttpClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var responseMessage = client.GetAsync($"{url}/calc/allbooks").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content;
                var books = JsonConvert.DeserializeObject<List<Book>>(result.ReadAsStringAsync().Result);
                foreach (var item in books)
                {
                    listBox1.Items.Add(item.title);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
