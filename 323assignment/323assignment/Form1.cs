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
            LoadMakes();
        }

        private void LoadMakes()
        {
            try
            {
                OracleDataReader dr = Database.Query("Select name from make");
                while (dr.Read())
                {
                    comboBoxMake.Items.Add(dr.GetString(0));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error has occured:" + ex);
            }

        }

        private void ComboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxModel.Text = "";
            comboBoxModel.Items.Clear();
            String make = this.comboBoxMake.GetItemText(this.comboBoxMake.SelectedItem);
            try
            {
                OracleDataReader dr = Database.Query("Select name from model where make = '" + make+"'");
                while (dr.Read())
                {
                    comboBoxModel.Items.Add(dr.GetString(0));
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has occured:" + ex);
            }

        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            String make = comboBoxMake.SelectedItem.ToString();
            String model = comboBoxModel.SelectedItem.ToString();
            if (make != "" && model != "")
            {
                try
                {
                    OracleDataReader dr = Database.Query("Select * from car Where make = '" + make + "' and model = '" + model+"'");
                    while (dr.Read())
                    {
                        listBoxCars.Items.Add(dr.GetString(0));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error has occured:" + ex);
                }
            }
        }
    }
}
