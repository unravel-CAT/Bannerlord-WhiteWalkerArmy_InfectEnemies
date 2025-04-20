using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.Localization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    internal class Infect
    {
        public static void SpawnTroop(Agent affectorAgent, Agent affectedAgent, int durations, bool enableMount)
        {
            string finalInfectedTroopId = affectedAgent.Character.StringId;
            PartyBase affectorParty = GetaffectorParty(affectorAgent);
            PartyBase affectedParty = GetaffectorParty(affectedAgent);
            //InformationManager.DisplayMessage(new InformationMessage(affectorParty.Id.ToString() + ": " + affectorParty.Name.ToString()));

            if (!InfectPatch.infectedTroopCanInfectOthers && affectorParty.Id.Contains("infected_troop")) return;
            if (!InfectPatch.infectedTroopCanBeInfectedAgain && affectedParty.Id.Contains("infected_troop")) return;
            if (affectorParty != null && affectedParty != null && Mission.Current.Agents.Count <= 1500)
            {
                CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>(finalInfectedTroopId);

                // Party
                if (affectorParty.MapEventSide == MobileParty.MainParty.Party.MapEventSide)
                {
                    isPlayerSide = true;
                    if (infectedPartyHasCreated_PlayerSide == false)
                    {
                        infectedPartyHasCreated_PlayerSide = true;
                        infectedMobileParty_PlayerSide = new InfectedParty(affectorParty)._party;
                        infectedPartyBase_PlayerSide = new PartyBase(infectedMobileParty_PlayerSide);
                    }
                    else
                    {
                        infectedPartyBase_PlayerSide.AddElementToMemberRoster(@object, 1, true);
                    }
                    infectedPartyBase_PlayerSide.MapEventSide = MobileParty.MainParty.Party.MapEventSide;
                    infectedPartyBase_Now = infectedPartyBase_PlayerSide;
                }
                else
                {
                    isPlayerSide = false;
                    if (infectedPartyHasCreated_OtherSide == false)
                    {
                        infectedPartyHasCreated_OtherSide = true;
                        infectedMobileParty_OtherSide = new InfectedParty(affectorParty)._party;
                        infectedPartyBase_OtherSide = new PartyBase(infectedMobileParty_OtherSide);
                    }
                    else
                    {
                        infectedPartyBase_OtherSide.AddElementToMemberRoster(@object, 1, true);
                    }
                    infectedPartyBase_OtherSide.MapEventSide = MobileParty.MainParty.Party.MapEventSide.OtherSide;
                    infectedPartyBase_Now = infectedPartyBase_OtherSide;
                }

                // Agent
                PartyAgentOrigin troopOrigin = new PartyAgentOrigin(infectedPartyBase_Now, @object, -1, default(UniqueTroopDescriptor), false);
                Agent agent = Mission.Current.SpawnTroop(troopOrigin, isPlayerSide, false, enableMount && @object.HasMount(), false, 1, 1, true, true, false, null, null, null, null, FormationClass.NumberOfAllFormations, false);
                agent.TeleportToPosition(affectedAgent.Position);
                //agent.Character.IsFemale = true;
                agent.ChangeMorale(100f);
                if (durations > 0)
                {
                    AgentFadOutMissionBehaviour agentFadOutMissionBehaviour = new AgentFadOutMissionBehaviour(agent, durations);
                    Mission.Current.AddMissionBehavior(agentFadOutMissionBehaviour);
                }
            }
        }
        public static PartyBase GetaffectorParty(Agent affectorAgent)
        {
            PartyBase result = null;
            try
            {
                PartyAgentOrigin partyAgentOrigin = (PartyAgentOrigin)affectorAgent.Origin;
                result = partyAgentOrigin.Party;
            }
            catch
            {
            }
            try
            {
                PartyGroupAgentOrigin partyGroupAgentOrigin = (PartyGroupAgentOrigin)affectorAgent.Origin;
                result = partyGroupAgentOrigin.Party;
            }
            catch
            {
            }
            return result;
        }

        public static bool isPlayerSide = false;
        private static bool infectedPartyHasCreated_PlayerSide = false;
        private static bool infectedPartyHasCreated_OtherSide = false;

        private static MobileParty infectedMobileParty_PlayerSide = null;
        private static MobileParty infectedMobileParty_OtherSide = null;

        private static PartyBase infectedPartyBase_PlayerSide = null;
        private static PartyBase infectedPartyBase_OtherSide = null;
        private static PartyBase infectedPartyBase_Now = null;
    }
}
