using Oracle.ManagedDataAccess.Client;
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
    public partial class Main : Form
    {
        OracleDB Database;
        public Main()
        {
            InitializeComponent();
            Database = new OracleDB();
        }

        private void LoadMakes()
        {
            OracleDataReader dr = Database.Query("Select Name from Make");
            comboBoxMake.Items.Add(dr.GetString(0));
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            
            if (Database.Connect()){
                MessageBox.Show("Success");

            }
            else
            {
                MessageBox.Show("Failure");
            }
        }

        private void ComboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            String make = comboBoxMake.SelectedItem.ToString();
            OracleDataReader dr = Database.Query("Select model_name from model where make_name = " + make);
            comboBoxModel.Items.Add(dr.GetString(0));
        }
    }
}
