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
using StartFinance.Models;
using Windows.UI.Popups;
using SQLite.Net;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalPage : Page
    {
        Personal selectedPersonal = null;
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");
        public PersonalPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            /// Initializing a database
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            // Creating table
            Results();
        }
        public void Results()
        {
            conn.CreateTable<Personal>();
            var query1 = conn.Table<Personal>();
            PersonalView.ItemsSource = query1.ToList();
        }

        public async void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FirstName.Text.ToString() == "" || LastName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("First and Last name required", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    
                    conn.CreateTable<Personal>();
                    conn.Insert(new Personal
                    {
                        FirstName = FirstName.Text.ToString(),
                        LastName = LastName.Text.ToString(),
                        DOB = DOB.Date.Date,
                        Gender = Gender.SelectionBoxItem.ToString(),
                        Email = Email.Text.ToString(),
                        Phone = int.Parse(Phone.Text.ToString()),
                    });
                    // Creating table
                    Results();
                }
            }
            catch(Exception ex)
            {
                if(ex is FormatException)
                {
                    MessageDialog dialog = new MessageDialog("Phone number must be numeric digits", "Oops..!");
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("Gender must be selected", "Oops..!");
                    await dialog.ShowAsync();
                }
                /// no idea
            }
        }
        public async void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            if (PersonalView.SelectedItem == null)
            {
                MessageDialog dialog = new MessageDialog("No Item Selected", "Oops..!");
                await dialog.ShowAsync();
            }
            else
            {

                if (FirstName.Text.ToString() == "" || LastName.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("Can't Leave First Name and Last Name Blank", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    int personID = ((Personal)PersonalView.SelectedItem).ID;
                    conn.CreateTable<Personal>();
                    conn.Update(new Personal
                    {
                        ID = personID,
                        FirstName = FirstName.Text.ToString(),
                        LastName = LastName.Text.ToString(),
                        DOB = DOB.Date.Date,
                        Gender = Gender.SelectionBoxItem.ToString(),
                        Email = Email.Text.ToString(),
                        Phone = int.Parse(Phone.Text.ToString())
                    });
                    // Creating table
                    Results();
                }
            }
        }

        public async void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string AccSelection1 = ((Personal)PersonalView.SelectedItem).FirstName;
            //    string AccSelection2 = ((Personal)PersonalView.SelectedItem).LastName;
            //    if (AccSelection1 == "" || AccSelection2 == "")
            //    {
            //        MessageDialog dialog = new MessageDialog("Not selected the Person", "Oops..!");
            //        await dialog.ShowAsync();
            //    }
            //    else
            //    {
            //        conn.CreateTable<Personal>();
            //        var query1 = conn.Table<Personal>();
            //        var query3 = conn.Query<Personal>("DELETE FROM WishList WHERE First Name ='" + AccSelection1 + "Last Name ='" + AccSelection2 + "'");
            //        PersonalView.ItemsSource = query1.ToList();
            //    }
            //}
            //catch (NullReferenceException)
            //{
            //    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
            //    await dialog.ShowAsync();
            //}
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }
        private void PersonalView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedPersonal = (Personal)PersonalView.SelectedValue;
            DataContext = selectedPersonal;
        }
    }
}
