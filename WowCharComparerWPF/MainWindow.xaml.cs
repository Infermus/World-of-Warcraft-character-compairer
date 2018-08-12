using System;
using System.Collections.Generic;
using System.Windows;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using WowCharComparerLib.Models;

namespace WowCharComparerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeStartingControlsData();
        }

        private void InitializeStartingControlsData()
        {
            //var result = await BlizzardAPIManager.GetCharacterDataAsJson("Selectus", BlizzardRealms.GetWrappedBlizzardRealm(BlizzardRealms.BlizzardRealmEnum.BurningLegion), BlizzardLocales.en_GB);
            //BlizzardAPIManager.DeserialiseJson<RealmsStatus>(**raw json datas**);

            //RealmComboBox.ItemsSource = Enum.GetValues(typeof(BlizzardRealms.BlizzardRealmEnum));
            //LocateComboBox.ItemsSource = Enum.GetValues(typeof(BlizzardLocales));
        }

        #region Events

        private async void GetApiDataButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await BlizzardAPIManager.GetAPIDataAsJsonAsync(BlizzardAPIProfiles.Character,
                                                                        new Realm("burning-legion", "en_GB"),
                                                                        "Selectus",
                                                                        new List<CharacterFields>()
                                                                        {
                                                                            CharacterFields.PVP,
                                                                            CharacterFields.Quests,
                                                                            CharacterFields.Guild,
                                                                        });

            apiresponsetextbox_temp.Text = result.Data;
            CharacterModel model = BlizzardAPIManager.DeserializeJsonData<CharacterModel>(result.Data);
        }

        #endregion
    }
}
