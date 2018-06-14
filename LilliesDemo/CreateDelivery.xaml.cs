using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LilliesDemo.ViewModels;

namespace LilliesDemo
{
    /// <summary>
    /// Interaction logic for CreateDelivery.xaml
    /// </summary>
    public partial class CreateDelivery : Window
    {
        public int CustomerID { get; set; }

        public CreateDelivery(int customerID)
        {
            InitializeComponent();
            CustomerID = customerID;
        }

        private async void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            BusinessLogicLayer controller = new BusinessLogicLayer();
            DeliveryViewModel newDelivery = new DeliveryViewModel
            {
                CustomerID = this.CustomerID,
                DeliveryDate = pckDeliveryDate.SelectedDate,
                LilliesDelivered = txtLilliesDelivered.Text,
                Note = txtNote.Text
            };

            if (await controller.AddDelivery(newDelivery))
            {
                MainWindow window = (MainWindow)Application.Current.MainWindow;
                window.lstDeliveries.ItemsSource = await controller.PopulateDeliveryBox(CustomerID);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter an lilly delivery quantity greater than or equal to 0");
            }
        }
    }
}
