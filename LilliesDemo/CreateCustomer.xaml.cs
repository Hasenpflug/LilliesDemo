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
using LilliesDemo.Models;
using LilliesDemo.ViewModels;

namespace LilliesDemo
{
    /// <summary>
    /// Interaction logic for CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        public CreateCustomer()
        {
            InitializeComponent();
        }

        private async void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            BusinessLogicLayer controller = new BusinessLogicLayer();
            CustomerViewModel newCustomer = new CustomerViewModel
            {
                Address = txtAddress.Text,
                Name = txtName.Text,
                PhoneNumber = txtPhoneNumber.Text
            };

            if (await controller.AddCustomer(newCustomer))
            {
                MainWindow window = (MainWindow)Application.Current.MainWindow;
                window.lstCustomers.ItemsSource = await controller.PopulateCustomerBox();
                this.Close();
            }
            else
            {
                MessageBox.Show(String.Format("The customer name '{0}' already exists, try again.", newCustomer.Name));
            }
        }        
    }
}
