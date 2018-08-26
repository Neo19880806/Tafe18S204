using SQLite.Net;
using StartFinance.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppointmentPage : Page
    {
        Appointment selectedAppointment = null;
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");
        public AppointmentPage()
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
            conn.CreateTable<Appointment>();
            var query1 = conn.Table<Appointment>();
            AppointmentListView.ItemsSource = query1.ToList();
        }

        private async void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EventName.Text.ToString() == "" || Location.Text.ToString()=="")
                {
                    MessageDialog dialog = new MessageDialog("No value entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<Appointment>();
                    conn.Insert(new Appointment
                    {
                        EventName = EventName.Text.ToString(),
                        Location = Location.Text.ToString(),
                        EventDate = EventDate.Date.Date,
                        StartTime = StartTime.Time.ToString(),
                        EndTime = EndTime.Time.ToString()
                    });
                    // Creating table
                    Results();
                }
            }
            catch (Exception ex)
            {
                if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("EventName Name already exist, Try Different Name", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    /// no idea
                }
            }
        }

        private async void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentListView.SelectedItem == null)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
            else
            {
                if (EventName.Text.ToString() == "" || Location.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("No value entered", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    int ID = ((Appointment)AppointmentListView.SelectedItem).AppointmentID;
                    conn.CreateTable<Appointment>();
                    conn.Update(new Appointment
                    {
                        AppointmentID = ID,
                        EventName = EventName.Text.ToString(),
                        Location = Location.Text.ToString(),
                        EventDate = EventDate.Date.Date,
                        StartTime = StartTime.Time.ToString(),
                        EndTime = StartTime.Time.ToString()
                    });
                    // Creating table
                    Results();
                }
            }
        }
        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentListView.SelectedItem == null)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
            else
            {
                int ID = ((Appointment)AppointmentListView.SelectedItem).AppointmentID;
                conn.CreateTable<Appointment>();
                var query1 = conn.Table<Appointment>();
                var query3 = conn.Query<Appointment>("DELETE FROM Appointment WHERE AppointmentID ='" + ID + "'");
                AppointmentListView.ItemsSource = query1.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Results();
        }

        private void AppointmentListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selectedAppointment = (Appointment)AppointmentListView.SelectedValue;
            DataContext = selectedAppointment;
        }
    }
}
