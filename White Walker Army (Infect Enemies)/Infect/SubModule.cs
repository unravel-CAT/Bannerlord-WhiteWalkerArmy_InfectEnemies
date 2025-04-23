using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.MapEvents;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    public class SubModule : MBSubModuleBase
    {
        public static UnitUnitConfig UnitUnitConfig { get; private set; }
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            SubModule.LoadConfig();
            new Harmony("infect").PatchAll();
        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            for (int i = 0; i < Campaign.Current.MobileParties.Count; ++i)
            {
                MobileParty mobileParty = Campaign.Current.MobileParties[i];
                if (mobileParty.Party.Id.ToString().Contains("infected_troop"))
                {
                    Campaign.Current.MobileParties.Remove(mobileParty);
                }
            }
        }


        private static void LoadConfig()
        {
            if (File.Exists(SubModule.UnitUnitConfigFilePath))
            {
                try
                {
                    SubModule.UnitUnitConfig = JsonConvert.DeserializeObject<UnitUnitConfig>(File.ReadAllText(SubModule.UnitUnitConfigFilePath));
                }
                catch
                {
                    throw new Exception("Config Encounter Errors.");
                }
            }
        }

        private static readonly string UnitUnitConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "unit_unit_config.json");
    }
}
