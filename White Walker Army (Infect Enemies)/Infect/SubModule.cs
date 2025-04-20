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

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("infect").PatchAll();
        }

        //public override void OnMissionBehaviorInitialize(Mission mission)
        //{
        //    base.OnMissionBehaviorInitialize(mission);
        //    mission.AddMissionBehavior(new AgentFadOutMissionBehaviour());
        //}
    }
}
