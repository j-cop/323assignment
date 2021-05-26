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
            listBoxCars.Items.Clear();
            String make = comboBoxMake.SelectedItem.ToString();
            String model = comboBoxModel.SelectedItem.ToString();
            if (make != "" && model != "")
            {
                try
                {
                    OracleDataReader dr = Database.Query("Select prod_year, make, model, colour, transmission, body_style, engine_size, doors, fuel_type, fuel_rating from car Where make = '" + make + "' and model = '" + model+"'");
                    listBoxCars.Items.Add("YEAR".PadRight(6)+ "MAKE".PadRight(10)+ "MODEL".PadRight(15)+ "COLOUR".PadRight(10) + "TRANSMISSION".PadRight(20) + "BODY".PadRight(15) + "ENGINE SIZE".PadRight(20) + "DOORS".PadRight(10) + "FUEL TYPE".PadRight(15) +"FUEL RATING".PadRight(10));
                    while (dr.Read())
                    {
                        listBoxCars.Items.Add(dr.GetString(0).PadRight(6) + dr.GetString(1).PadRight(10) + dr.GetString(2).PadRight(15) + dr.GetString(3).PadRight(10) + dr.GetString(4).PadRight(20) + dr.GetString(5).PadRight(15) + dr.GetString(6).PadRight(20) + dr.GetString(7).PadRight(10) + dr.GetString(8).PadRight(15) + dr.GetString(9).PadRight(10));
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
