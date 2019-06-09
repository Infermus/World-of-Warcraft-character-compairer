using WowCharComparerWebApp.Models.CharacterProfile.Items.Others;
using WowCharComparerWebApp.Models.CharacterProfile.ItemsModels.Others;

namespace WowCharComparerWebApp.Models.Interfaces
{
    public interface IItem
    {
        int Id { get; set; }

        string Name { get; set; }

        string Icon { get; set; }

        int Quality { get; set; }

        int ItemLevel { get; set; }

        ToolTipParams ToolTipParams { get; set; }

        Stats[] Stats { get; set; }

        int Armor { get; set; }

        string Context { get; set; }

        int[] BonusLists { get; set; }

        int ArtifactId { get; set; }

        int DisplayInfoId { get; set; }

        int ArtifactAppearanceId { get; set; }

        ArtifactTriats ArtifactTriats { get; set; }

        Relics[] Relics { get; set; }

        CharacterProfile.Appearance Appearance { get; set; }

        AzeriteItem AzeriteItem { get; set; }

        AzeriteEmpoweredItem AzeriteEmpoweredItem { get; set; }
    }
}
