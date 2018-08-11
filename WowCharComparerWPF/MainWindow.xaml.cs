using System;
using System.Windows;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.Enums;

namespace WowCharComparerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RealmComboBox.ItemsSource = Enum.GetValues(typeof(BlizzardRealms.BlizzardRealmEnum));
            EULocateComboBox.ItemsSource = Enum.GetValues(typeof(BlizzardLocales));
        }

        private async void GetApiDataButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await BlizzardAPIManager.GetCharacterDataAsJsonAsync("Selectus", BlizzardRealms.GetWrappedBlizzardRealm(BlizzardRealms.BlizzardRealmEnum.BurningLegion), BlizzardLocales.en_GB);
            apiresponsetextbox_temp.Text = result.Data;
        }
    }
}
