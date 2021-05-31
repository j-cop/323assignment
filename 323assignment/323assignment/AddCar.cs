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
    public partial class AddCar : Form
    {
        public AddCar()
        {
            InitializeComponent();
        }

        private void buttonAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                string VIN = textBoxVIN.Text;
                int engineSize = int.Parse(textBoxEngineSize.Text);
                string transmission = comboBoxTransmission.ToString();
                string colour = textBoxColour.Text;
                int prodYear = int.Parse(textBoxProdYear.Text);
                string fuelType = comboBoxFuelType.SelectedItem.ToString();
                string bodyStyle = comboBoxBody.SelectedItem.ToString();
                string make = comboBoxMake.SelectedItem.ToString();
                string model = comboBoxModel.SelectedItem.ToString();
                string dealership = comboBoxDealership.SelectedItem.ToString();
                if(String.IsNullOrEmpty(VIN) && String.IsNullOrEmpty())
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex + "check your entries and try again.");
            }

            
        }
    }
}
