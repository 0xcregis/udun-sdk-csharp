using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Udun.Core.Entity;
using Udun.Core.Http;

namespace Udun.FormDemo.Api
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RequestControllerBase requestController = new RequestControllerBase();

        private void button1_Click(object sender, EventArgs e)
        {
            Address address = requestController.CreateCoinAddress(Convert.ToInt32(this.textBox1.Text), this.textBox2.Text, this.textBox3.Text, this.textBox4.Text);
            this.richTextBox1.Text = Newtonsoft.Json.JsonConvert.SerializeObject(address);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResponseMessage<string> msg = requestController.Transfer(this.textBox5.Text, this.textBox6.Text, this.textBox7.Text, this.textBox8.Text, this.textBox9.Text, this.textBox10.Text);
            this.richTextBox2.Text = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResponseMessage<string> msg = requestController.AutoTransfer(this.textBox5.Text, this.textBox6.Text, this.textBox7.Text, this.textBox8.Text, this.textBox9.Text, this.textBox10.Text);
            this.richTextBox2.Text = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Trade reqTrade = new Trade()
            {
                address = "地址A",
                amount = "1000000",
                businessId = "b1021123",
                coinType = "0",
                mainCoinType = "0",
                decimals = 8,
                fee = "5640",
                memo = "测试memo",
                status = 1,
                tradeId = "t1232132131",
                tradeType = 1,
                txId = "0xa34dq22sddadwqr243qrd32543hucdaeh83"
            };
            this.richTextBox3.Text = Newtonsoft.Json.JsonConvert.SerializeObject(reqTrade);
            string msg = requestController.CallBack(reqTrade,false);
            this.richTextBox4.Text = msg;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Trade reqTrade = new Trade()
            {
                address = "地址A",
                amount = "1000000",
                businessId = "b1021123",
                coinType = "0",
                mainCoinType = "0",
                decimals = 8,
                fee = "5640",
                memo = "测试memo",
                status = 1,
                tradeId = "t1232132131",
                tradeType = 2,
                txId = "0xa34dq22sddadwqr243qrd32543hucdaeh83"
            };
            this.richTextBox3.Text = Newtonsoft.Json.JsonConvert.SerializeObject(reqTrade);
            string msg = requestController.CallBack(reqTrade, false);
            this.richTextBox4.Text = msg;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Trade reqTrade = new Trade()
            {
                address = "地址A",
                amount = "1000000",
                businessId = "b1021123",
                coinType = "0",
                mainCoinType = "0",
                decimals = 8,
                fee = "5640",
                memo = "测试memo",
                status = 1,
                tradeId = "t1232132131",
                tradeType = 2,
                txId = "0xa34dq22sddadwqr243qrd32543hucdaeh83"
            };
            this.richTextBox3.Text = Newtonsoft.Json.JsonConvert.SerializeObject(reqTrade);
            string msg = requestController.CallBack(reqTrade, true);
            this.richTextBox4.Text = msg;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.textBox11.Text) && !string.IsNullOrEmpty(this.textBox12.Text))
            {
                this.richTextBox5.Text = requestController.CheckAddress(this.textBox12.Text.Trim(), this.textBox11.Text.Trim()).ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.richTextBox6.Text = Newtonsoft.Json.JsonConvert.SerializeObject(requestController.GetSupportCoin());
        }
    }
}
