using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using Oracle.ManagedDataAccess.Client;

namespace _323assignment
{
    public partial class AddCar : Form
    {
        OracleDB Database;
        Mongo mongoDB;
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
            else
            {
                mongoDB = new Mongo();
                if (!mongoDB.Connect())
                {
                    MessageBox.Show("Failed to connect to the Database");
                    this.Close();
                }
                MongoLoadItems();
            }

        }
        private void MongoLoadItems()
        {
            try
            {
                MongoItemLoad("transmission", comboBoxTransmission);
                MongoItemLoad("fuel_type", comboBoxFuelType);
                MongoItemLoad("body_style", comboBoxBody);
                MongoItemLoad("make", comboBoxMake);
                MongoItemLoad("Dealership", comboBoxDealership);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has occured:" + ex);
            }
        }

        private void MongoItemLoad(string collection, ComboBox combo)
        {
            List<BsonDocument> list = mongoDB.Read(collection);
            foreach (BsonDocument item in list)
            {
                combo.Items.Add(item.GetValue("name").ToString());

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
                    comboBoxDealership.Items.Add(dr.GetString(0));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has occured:" + ex);
            }
            

        }

        private void buttonAddCar_Click(object sender, EventArgs e)
        {

            string VIN = textBoxVIN.Text;
            string transmission = comboBoxTransmission.SelectedItem.ToString();
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
            if (!(String.IsNullOrEmpty(VIN) || String.IsNullOrEmpty(transmission) || String.IsNullOrEmpty(colour) || String.IsNullOrEmpty(fuelType) || String.IsNullOrEmpty(bodyStyle) || String.IsNullOrEmpty(make) || String.IsNullOrEmpty(model) || String.IsNullOrEmpty(dealership)))
            {
                if (oracle)
                {
                    try
                    {

                        OracleDataReader dr = Database.Query("Select id from dealership where name = '" + dealership + "'");
                        dr.Read();
                        int ds = int.Parse(dr.GetString(0));
                        Database.Query("insert into car (VIN, engine_size, doors, prod_year, fuel_rating, colour, seats, make, model, fuel_type, body_style, dealership, transmission) values ('" + VIN + "', " + engineSize + ", " + doors + ", " + prodYear + ", " + fuelRating + ", '" + colour + "', " + seats + ", '" + make + "', '" + model + "', '" + fuelType + "', '" + bodyStyle + "', '" + ds + "', '" + transmission + "')");

                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex + "check your entries and try again.");
                    }

                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Check Details and try again");
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
            else
            {
                List<BsonDocument> model = mongoDB.Read("model", new BsonDocument("make", make));
                foreach (BsonDocument item in model)
                {
                    comboBoxModel.Items.Add(item.GetValue("name"));
                }
            }
        }
    }
}
