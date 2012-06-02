using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumServer.DataLayer;

namespace InitDataBase
{
    public partial class Form1 : Form
    {
        DataManager dm;

        public Form1()
        {
            InitializeComponent();
            dm = new DataManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dm.CleanForumData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dm.InitForumData();
        }

    }
}
