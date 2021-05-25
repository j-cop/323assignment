using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _323assignment
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {


        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            OracleDB Database = new OracleDB();
            if (Database.Connect()){
                MessageBox.Show("Success");

            }
            else
            {
                MessageBox.Show("Failure");
            }
        }
    }
}
