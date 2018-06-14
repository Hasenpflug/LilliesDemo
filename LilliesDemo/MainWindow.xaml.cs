using System;
using System.Threading.Tasks;
using System.Windows;
using LilliesDemo.ViewModels;

namespace LilliesDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SelectedCustomerID { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void lstCustomers_Initialized(object sender, EventArgs e)
        {
            BusinessLogicLayer controller = new BusinessLogicLayer();
            lstCustomers.ItemsSource = await controller.PopulateCustomerBox();
        }       

        private void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            CreateCustomer customerWindow = new CreateCustomer();
            customerWindow.Show();
        }

        private void btnCreateDelivery_Click(object sender, RoutedEventArgs e)
        {
            CreateDelivery deliveryWindow = new CreateDelivery(SelectedCustomerID);
            deliveryWindow.Show();
        }

        private async void lstCustomers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BusinessLogicLayer controller = new BusinessLogicLayer();

            if (lstCustomers.SelectedValue is CustomerViewModel customer)
            {
                SelectedCustomerID = customer.CustomerID;
                txtName.Text = customer.Name;
                txtAddress.Text = customer.Address;
                txtPhoneNumber.Text = customer.PhoneNumber;

                lstDeliveries.ItemsSource = await controller.PopulateDeliveryBox(SelectedCustomerID);
                btnCreateDelivery.IsEnabled = true;
            }
        }

        private void lstDeliveries_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstDeliveries.SelectedValue is DeliveryViewModel delivery)
            {
                SelectedCustomerID = delivery.CustomerID;
                txtDeliveryDate.Text = delivery.DeliveryDate.ToString();
                txtLilliesDelivered.Text = delivery.LilliesDelivered;
                txtNote.Text = delivery.Note;
            }
        }
    }
}
