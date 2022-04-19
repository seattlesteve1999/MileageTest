using MileageManagerForms.Database;
using System;
using Xamarin.Forms;

namespace MileageManagerFoems
{
    public class ManagerClass : ContentPage
    {
        public ListView ProcessData()
        {
            Title = "Mileage Manager";

            StackLayout layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 20
            };

            Grid header = new Grid()
            {
                ColumnSpacing = 10,
            };

            for (int i = 0; i < 4; i++)
            {
                header.ColumnDefinitions.Add(new ColumnDefinition()
                { Width = new GridLength(1, GridUnitType.Star) });
            }

            header.Children.Add(new Label { Text = "Date", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), }, 0, 0); // Left, First element
            header.Children.Add(new Label { Text = "Miles", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), }, 1, 0); // Right, First element
            header.Children.Add(new Label { Text = "Gas", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), }, 2, 0); // Left, Second element
            header.Children.Add(new Label { Text = "MPG", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), }, 3, 0); // Right, Second element

            Grid grid = new Grid
            {
                RowSpacing = 20
            };

            for (int i = 0; i < 4; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }


            MileageTableDefination data = new MileageTableDefination
            {
                Date = Convert.ToDateTime("12/23/1990"),
                Gas = Convert.ToDecimal(8.45),
                Miles = Convert.ToDecimal(223),
                MPG = Convert.ToDecimal(44.69)
            };

            //MileageItemRepository repository = new MileageItemRepository();
            //await repository.GetAllMileageData();
            //var stuff = await repository.GetAllMileageDisplayData();
            ListView listView = new ListView();

            //foreach (var item in stuff)
            //{
            //    listView.ItemsSource = item.Date.ToString();
            //}

            return listView;
        }
    }
}
