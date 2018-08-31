using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite;
using StartFinance.Models;
using Windows.UI.Popups;
using SQLite.Net;
/*
 *Author Daniel bialous Added on 31/08/2018 on the feat_Contractdetails branch 
 */


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactDetailsPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public ContactDetailsPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            // Creating table
           // conn.CreateTable<ContactDetails>();
            Results();
        }

        public void Results()
        {
            conn.CreateTable<ContactDetails>();
            var query1 = conn.Table<ContactDetails>();
            ContactDetailsView.ItemsSource = query1.ToList();
        }

        private async void AddContactDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_firstName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Please Enter your first name");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<ContactDetails>();
                    conn.Insert(new ContactDetails
                    {
                        firstName = _firstName.Text.ToString(),
                        lastName = _lastName.Text.ToString(),
                        companyName = _companyName.Text.ToString(),
                        mobilePhone = _mobilePhone.Text.ToString(),
                        email = _email.Text.ToString()
                    });
                    // Creating table
                    Results();
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageDialog dialog = new MessageDialog("You forgot to enter the Amount or entered an invalid Amount", "Oops..!");
                    await dialog.ShowAsync();
                }
                else if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("Name already exist, Try Different Name", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    /// no idea
                }
            }// end of firstName

        } 

        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string AccSelection = ((ContactDetails)ContactDetailsView.SelectedItem).firstName;
                if (AccSelection == "")
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<ContactDetails>();
                    var query1 = conn.Table<ContactDetails>();
                    var query3 = conn.Query<ContactDetails>("DELETE FROM ContactDetails WHERE FirstName ='" + AccSelection + "'");
                    ContactDetailsView.ItemsSource = query1.ToList();
                }
            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }

    }
}

