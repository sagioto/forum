using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ForumClientCore;

namespace ForumClientGui
{
    public partial class ClientFormUI : Form
    {
        ClientController controller;

        public ClientFormUI()
        {
            InitializeComponent();
            controller = new ClientController();
            controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer); ;
        }

        /// <summary>
        /// Will be called when controller will invoke update event
        /// </summary>
        /// <param name="text"></param>
        public void controller_OnUpdateFromServer(string text)
        {
            richTextBox1.Text = richTextBox1.Text + text + '\n';    // Update textBox with the message from server
        }

        /// <summary>
        /// When clicking button1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                thread.Start();
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public void WorkThreadFunction()
        {
            controller.AddMessage(textBox1.Text);
        }
    }
}
