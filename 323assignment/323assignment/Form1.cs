using MongoDB.Bson;
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
        Mongo mongoDB;
        bool isOracle;
        public Main()
        {
            InitializeComponent();
            isOracle = true;
            Database = new OracleDB();
            mongoDB = new Mongo();
            OracleConnnect();

        }
        private void OracleConnnect()
        {
            if (!Database.Connect())
            {
                MessageBox.Show("Failed to connect to the Database");
            }
            else
            {
                OracleLoadMakes();
            }
            
        }

        private void MongoConnect()
        {
            if (mongoDB.Connect())
            {
                MongoLoadMakes();
            }
            else
            {
                MessageBox.Show("Failed to connect to the Database");
            }

        }

        
        private void OracleLoadMakes()
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

        private void MongoLoadMakes()
        {
            List<BsonDocument> makes = mongoDB.Read("make");
            foreach(BsonDocument item in makes)
            {
                comboBoxMake.Items.Add(item.GetElement(1).Value.ToString());

            }
        }

        private void ComboBoxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxModel.Text = "";
            comboBoxModel.Items.Clear();
            String make = this.comboBoxMake.GetItemText(this.comboBoxMake.SelectedItem);
            if(isOracle)
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
                try
                {
                    List<BsonDocument> model = mongoDB.Read("model", new BsonDocument("make", make));
                    foreach (BsonDocument item in model)
                    {
                        comboBoxModel.Items.Add(item.GetValue("name"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error has occured:" + ex);
                }

            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            listBoxCars.Items.Clear();
            String make = comboBoxMake.SelectedItem.ToString();
            String model = comboBoxModel.SelectedItem.ToString();
            if (make != "" && model != "")
            {
                if (isOracle)
                {
                    try
                    {
                        OracleDataReader dr = Database.Query("Select prod_year, make, model, colour, transmission, body_style, engine_size, doors, fuel_type, fuel_rating from car Where make = '" + make + "' and model = '" + model + "'");
                        listBoxCars.Items.Add("YEAR".PadRight(6) + "MAKE".PadRight(10) + "MODEL".PadRight(15) + "COLOUR".PadRight(10) + "TRANSMISSION".PadRight(20) + "BODY".PadRight(15) + "ENGINE SIZE".PadRight(20) + "DOORS".PadRight(10) + "FUEL TYPE".PadRight(15) + "FUEL RATING".PadRight(10));
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
                else
                {
                    listBoxCars.Items.Add("YEAR".PadRight(6) + "MAKE".PadRight(10) + "MODEL".PadRight(15) + "COLOUR".PadRight(10) + "TRANSMISSION".PadRight(20) + "BODY".PadRight(15) + "ENGINE SIZE".PadRight(20) + "DOORS".PadRight(10) + "FUEL TYPE".PadRight(15) + "FUEL RATING".PadRight(10));
                    List<BsonDocument> car = mongoDB.Read("car", new BsonDocument("model", model));
                    foreach (BsonDocument item in car)
                    {

                        listBoxCars.Items.Add(item.GetValue("prod_year").ToString().PadRight(6) + item.GetValue("make").ToString().PadRight(10) + item.GetValue("model").ToString().PadRight(15) + item.GetValue("colour").ToString().PadRight(10) + item.GetValue("transmission").ToString().PadRight(20) + item.GetValue("body_style").ToString().PadRight(15) + item.GetValue("engine_size").ToString().PadRight(20) + item.GetValue("doors").ToString().PadRight(10) + item.GetValue("fuel_type").ToString().PadRight(15) + item.GetValue("fuel_rating").ToString().PadRight(10));

                    }
                }
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCar addCarWindow = new AddCar(isOracle);
            addCarWindow.Show();

        }

        private void RadioButtonOracle_CheckedChanged(object sender, EventArgs e)
        {
            isOracle = !isOracle;
            listBoxCars.Items.Clear();
            comboBoxModel.Text = "";
            comboBoxMake.Text = "";
            comboBoxMake.Items.Clear();
            comboBoxModel.Items.Clear();
            if (isOracle)
            {
                OracleConnnect();
            }
            else
            {
                MongoConnect();
            }

        }
    }
}
