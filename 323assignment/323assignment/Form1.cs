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
            if (!Database.Connect())
            {
                MessageBox.Show("Failed to connect to the Database");
            }
        }

        private void LoadMakes()
        {
            OracleDataReader dr = Database.Query("Select Name from Make");
            comboBoxMake.Items.Add(dr.GetString(0));
        }

        private void ComboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMake.Text = "";
            String make = comboBoxMake.SelectedItem.ToString();
            OracleDataReader dr = Database.Query("Select model_name from model where make_name = " + make);
            comboBoxModel.Items.Add(dr.GetString(0));
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            String make = comboBoxMake.SelectedItem.ToString();
            String model = comboBoxModel.SelectedItem.ToString();
            if (make != "" && model != "")
            {
                OracleDataReader dr = Database.Query("Select * from car Where make = " + make + " and model = " + model);
                listBoxCars.Items.Add(dr.GetString(0));
            }
        }
    }
}
