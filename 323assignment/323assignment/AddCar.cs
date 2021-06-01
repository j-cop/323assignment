using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace _323assignment
{
    public partial class AddCar : Form
    {
        OracleDB Database;
        bool oracle;
        public AddCar(bool isOracle)
        {
            InitializeComponent();
            oracle = isOracle;
            if (oracle)
            {
                Database = new OracleDB();
                if (!Database.Connect())
                {
                    MessageBox.Show("Failed to connect to the Database");
                    this.Close();
                }
                OracleLoadItems();
            }

        }

        private void OracleLoadItems()
        {
            try
            {
                //Transmission
                OracleDataReader dr = Database.Query("Select * from transmission");
                while (dr.Read())
                {
                    comboBoxTransmission.Items.Add(dr.GetString(0));
                }
                //Fuel Type
                dr = Database.Query("Select * from fuel_type");
                while (dr.Read())
                {
                    comboBoxFuelType.Items.Add(dr.GetString(0));
                }
                //Body Style
                dr = Database.Query("Select * from body_style");
                while (dr.Read())
                {
                    comboBoxBody.Items.Add(dr.GetString(0));
                }
                //make
                dr = Database.Query("Select name from make");
                while (dr.Read())
                {
                    comboBoxMake.Items.Add(dr.GetString(0));
                }
                //dealership
                dr = Database.Query("Select name from dealership");
                while (dr.Read())
                {
                    comboBoxMake.Items.Add(dr.GetString(0));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has occured:" + ex);
            }
            

        }

        private void buttonAddCar_Click(object sender, EventArgs e)
        {
            if (oracle)
            {
                try
                {
                    string VIN = textBoxVIN.Text;
                    string transmission = comboBoxTransmission.ToString();
                    string colour = textBoxColour.Text;
                    string fuelType = comboBoxFuelType.SelectedItem.ToString();
                    string bodyStyle = comboBoxBody.SelectedItem.ToString();
                    string make = comboBoxMake.SelectedItem.ToString();
                    string model = comboBoxModel.SelectedItem.ToString();
                    string dealership = comboBoxDealership.SelectedItem.ToString();
                    int prodYear = (int)numericUpDownProdYear.Value;
                    int engineSize = (int)numericUpDownEngineSize.Value;
                    int doors = (int)numericUpDownDoors.Value;
                    int fuelRating = (int)numericUpDownFuelRating.Value;
                    int seats = (int)numericUpDownSeats.Value;
                    if (String.IsNullOrEmpty(VIN) || String.IsNullOrEmpty(transmission) || String.IsNullOrEmpty(colour) || String.IsNullOrEmpty(fuelType) || String.IsNullOrEmpty(bodyStyle) || String.IsNullOrEmpty(make) || String.IsNullOrEmpty(model) || String.IsNullOrEmpty(dealership))
                    {
                        MessageBox.Show("Check Details and try again");
                    }
                    else
                    {
                        Database.Query("insert into Car (VIN, engine_size, doors, prod_year, fuel_rating, colour, seats, make, model, fuel_type, body_style, dealership, transmission) values ('" + VIN + "', " + engineSize + ", " + doors + ", " + prodYear + ", " + fuelRating + ", '" + colour + "', " + seats + ", '" + make + "', '" + model + "', '" + fuelType + "', '" + bodyStyle + "', " + dealership + ", '" + transmission + "')");
                        MessageBox.Show("Car added!");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "check your entries and try again.");
                }
            }

            
        }

        private void ComboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxModel.Text = "";
            comboBoxModel.Items.Clear();
            String make = this.comboBoxMake.GetItemText(this.comboBoxMake.SelectedItem);
            if (oracle)
            {
                try
                {
                    OracleDataReader dr = Database.Query("Select name from model where make = '" + make + "'");
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
        }
    }
}
