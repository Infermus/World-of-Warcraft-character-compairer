using System;
using System.Windows;
using WowCharComparerLib.Enums;
using WowCharComparerWPF.Enums;

namespace WowCharComparerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RealmComboBox.ItemsSource = Enum.GetValues(typeof(BlizzardRealms));
            EULocateComboBox.ItemsSource = Enum.GetValues(typeof(EULocate));

        }
    }
}
