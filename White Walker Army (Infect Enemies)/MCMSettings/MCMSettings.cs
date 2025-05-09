using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MCM;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using TaleWorlds.Localization;

namespace White_Walker_Army__Infect_Enemies_.MCMSettings
{
    public class MCMSettings : AttributeGlobalSettings<MCMSettings>
    {
        public override string Id
        {
            get
            {
                return "White_Walker_Army";
            }
        }

        public override string DisplayName
        {
            get
            {
                return new TextObject("{=WWA_DisplayName}White Walker Army (Infect Enemies)").ToString();
            }
        }

        public override string FolderName
        {
            get
            {
                return "WWA_IE";
            }
        }

        public override string FormatType
        {
            get
            {
                return "json2";
            }
        }

        // Character
        [SettingPropertyBool( "{=WWA_EP}Enable Player", HintText = "{=WWA_EP_Hint}Enable Player Infect ability[ Default: ON ]", Order = 1, RequireRestart = true)]
        [SettingPropertyGroup("Character", GroupOrder = 1)]
        public bool EnablePlayer { get; set; } = true;

        [SettingPropertyBool("{=WWA_EPTH}Enable Player Team Hero", HintText = "{=WWA_EPTH_Hint}Enable Player Team Hero Infect ability[ Default: OFF ]", Order = 2, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnablePlayerTeamHero { get; set; } = false;

        [SettingPropertyBool("{=WWA_EPTS}Enable Player Team Solider", HintText = "{=WWA_EPTS_Hint}Enable Player Team Solider Infect ability[ Default: OFF ]", Order = 3, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnablePlayerTeamSolider { get; set; } = false;

        [SettingPropertyBool("{=WWA_EATH}Enable Ally Team Hero", HintText = "{=WWA_EATH_Hint}Enable Ally Team Hero Infect ability[ Default: OFF ]", Order = 4, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnableAllyTeamHero { get; set; } = false;

        [SettingPropertyBool("{=WWA_EATS}Enable Ally Team Solider", HintText = "{=WWA_EATS_Hint}Enable Ally Team Solider Infect ability[ Default: OFF ]", Order = 5, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnableAllyTeamSolider { get; set; } = false;

        [SettingPropertyBool("{=WWA_EETH}Enable Enemy Team Hero", HintText = "{=WWA_EETH_Hint}Enable Enemy Team Hero Infect ability[ Default: OFF ]", Order = 6, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnableEnemyTeamHero { get; set; } = false;

        [SettingPropertyBool("{=WWA_EETS}Enable Enemy Team Solider", HintText = "{=WWA_EETS_Hint}Enable Enemy Team Solider Infect ability[ Default: OFF ]", Order = 7, RequireRestart = true)]
        [SettingPropertyGroup("Character")]
        public bool EnableEnemyTeamSolider { get; set; } = false;

        // BattleType
        [SettingPropertyBool("{=WWA_EIF}Enable In FieldBattle", HintText = "{=WWA_EIF_Hint}Enable Infect ability in FieldBattle[ Default: ON ].", Order = 1, RequireRestart = true)]
        [SettingPropertyGroup("BattleType", GroupOrder = 2)]
        public bool EnableInFieldBattle { get; set; } = true;

        [SettingPropertyBool("{=WWA_EIS}Enable In Siege", HintText = "{=WWA_EIS_Hint}Enable Infect ability in SiegeBattle[ Default: OFF ].", Order = 2, RequireRestart = true)]
        [SettingPropertyGroup("BattleType")]
        public bool EnableInSiegeBattle { get; set; } = false;

        [SettingPropertyBool("{=WWA_EISO}Enable In SallyOut", HintText = "{=WWA_EISO_Hint}Enable Infect ability in SallyOutBattle[ Default: OFF ].", Order = 3, RequireRestart = true)]
        [SettingPropertyGroup("BattleType")]
        public bool EnableInSallyOutBattle { get; set; } = false;

        [SettingPropertyBool("{=WWA_EISOside}Enable In SiegeOutside", HintText = "{=WWA_EISOside_Hint}Enable Infect ability in SiegeOutsideBattle[ Default: OFF ].", Order = 4, RequireRestart = true)]
        [SettingPropertyGroup("BattleType")]
        public bool EnableInSiegeOutsideBattle { get; set; } = false;

        [SettingPropertyBool("{=WWA_EIH}Enable In HideOut", HintText = "{=WWA_EIH_Hint}Enable Infect ability in HideOutBattle[ Default: OFF ].", Order = 5, RequireRestart = true)]
        [SettingPropertyGroup("BattleType")]
        public bool EnableInHideoutBattle { get; set; } = false;

        // Settings
        [SettingPropertyInteger("{=WWA_D}Durations(Seconds)", 0, 300, HintText = "{=WWA_D_Hint}The duration of the infected character.Setting it to 0 means infinite[ Default: 30 ].", Order = 1, RequireRestart = true)]
        [SettingPropertyGroup("Settings", GroupOrder = 3)]
        public int Durations { get; set; } = 30; 

        [SettingPropertyBool("{=WWA_ITCIO}Infected Troop Can Infect Others", HintText = "{=WWA_ITCIO_Hint}Be careful, turning it on may cause a snowball effect[ Default: OFF ].", Order = 2, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public bool InfectTroopCanInfectOthers { get; set; } = false;

        [SettingPropertyBool("{=WWA_ITCBIA}Infected Troop Can Be Infected Again", HintText = "{=WWA_ITCBIA_Hint}Be careful, turning it on may cause a snowball effect[ Default: OFF ].", Order = 3, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public bool InfectedTroopCanbeInfectedAgain { get; set; } = false;

        [SettingPropertyDropdown("{=WWA_PITCLM}PlayerSide Infected Troop Contour Line Marking", HintText = "{=WWA_PITCLM_Hint}PlayerSide Infected units will be marked with an outline of the selected color.", Order = 4, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public Dropdown<String> PlayerSideInfectedTroopContourLineMarking { get; set; } = new Dropdown<string>(new string[]
        {"None", "Black", "White", "Red", "Yellow", "Green", "Blue", "Cyan", "Magenta", "Gray",}, selectedIndex: 6);

        [SettingPropertyDropdown("{=WWA_EITCLM}EnemySide Infected Troop Contour Line Marking", HintText = "{=WWA_EITCLM_Hint}EnemySide Infected units will be marked with an outline of the selected color.", Order = 5, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public Dropdown<String> EnemySideInfectedTroopContourLineMarking { get; set; } = new Dropdown<string>(new string[]
        {"None", "Black", "White", "Red", "Yellow", "Green", "Blue", "Cyan", "Magenta", "Gray",}, selectedIndex: 6);

        [SettingPropertyInteger("{=WWA_H}Health", 1, 10000, HintText = "{=WWA_H_Hint}Initial health of infected units[ Default: 100 ].", Order = 6, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public int Health { get; set; } = 100;

        [SettingPropertyFloatingInteger("{=WWA_DM}DamageMultiplier", 0.01f, 100.0f, HintText = "{=WWA_DM_Hint}Damage multiplier for infected units[ Default: 1.0 ].", Order = 8, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public float DamageMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=WWA_DTM}DamageTakenMultiplier", 0.01f, 100.0f, HintText = "{=WWA_DTM_Hint}Damage taken multiplier for infected units[ Default: 1.0 ].", Order = 9, RequireRestart = true)]
        [SettingPropertyGroup("Settings")]
        public float DamageTakenMultiplier { get; set; } = 1.0f;
    }
}
