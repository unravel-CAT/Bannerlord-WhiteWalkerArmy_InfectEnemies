using System;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Library;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    internal class Infect
    {
        public static void SpawnTroop(Agent affectorAgent, Agent affectedAgent, int durations, bool enableMount)
        {
            string finalInfectedTroopId = affectedAgent.Character.StringId;
            PartyBase affectorParty = GetaffectorParty(affectorAgent);
            PartyBase affectedParty = GetaffectorParty(affectedAgent);

            if (!InfectPatch.infectedTroopCanInfectOthers && affectorParty.Id.Contains("infected_troop")) return;
            if (!InfectPatch.infectedTroopCanBeInfectedAgain && affectedParty.Id.Contains("infected_troop")) return;

            if (affectorParty != null && affectedParty != null && Mission.Current.Agents.Count <= 1500)
            {
                if (affectorAgent.Character.IsPlayerCharacter || affectorAgent.Name.Contains("女神"))
                {
                    string[] playerInfectedTroopId = {
                        "Apocalypse_Palace_Guards",
                        "Apocalypse_Hawkeye_Ranger",
                        "Apocalypse_Armored_Cavalry",
                        "Apocalypse_Royal_Archers"
                    };
                    int index = random.Next(playerInfectedTroopId.Length);
                    finalInfectedTroopId = playerInfectedTroopId[index];
                }
                else if (affectorAgent.Team.IsPlayerTeam && affectorParty.MobileParty.IsMainParty && affectorAgent.IsHero && !affectorAgent.Character.IsPlayerCharacter)
                {
                    finalInfectedTroopId = "Apocalypse_Believers";
                }

                if (affectedAgent.Character.StringId.Contains("Apocalypse"))
                {
                    string[] InfectedTroopId = {
                        "aserai_vanguard_faris",
                        "battanian_fian_champion",
                        "imperial_elite_cataphract",
                        "khuzait_khans_guard",
                        "druzhinnik_champion",
                        "vlandian_banner_knight"
                    };
                    int index = random.Next(InfectedTroopId.Length);
                    finalInfectedTroopId = InfectedTroopId[index];
                }
                CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>(finalInfectedTroopId);

                // Party
                try
                {
                    if (affectorParty.MapEventSide == MobileParty.MainParty.Party.MapEventSide)
                    {
                        isPlayerSide = true;
                        if (infectedPartyHasCreated_PlayerSide == false)
                        {
                            infectedPartyHasCreated_PlayerSide = true;
                            infectedMobileParty_PlayerSide = new InfectedParty(affectorParty)._party;
                            if (affectedParty.IsMobile)
                                infectedPartyBase_PlayerSide = new PartyBase(infectedMobileParty_PlayerSide);
                            else
                                infectedPartyBase_PlayerSide = new PartyBase(infectedMobileParty_PlayerSide.HomeSettlement);
                        }
                        else
                        {
                            infectedPartyBase_PlayerSide.AddElementToMemberRoster(@object, 1, true);
                        }
                        //infectedPartyBase_PlayerSide.MapEventSide = MobileParty.MainParty.Party.MapEventSide;
                        infectedPartyBase_Now = infectedPartyBase_PlayerSide;
                    }
                    else
                    {
                        isPlayerSide = false;
                        if (infectedPartyHasCreated_OtherSide == false)
                        {
                            infectedPartyHasCreated_OtherSide = true;
                            infectedMobileParty_OtherSide = new InfectedParty(affectorParty)._party;
                            if (affectedParty.IsMobile)
                                infectedPartyBase_OtherSide = new PartyBase(infectedMobileParty_OtherSide);
                            else
                                infectedPartyBase_OtherSide = new PartyBase(infectedMobileParty_OtherSide.HomeSettlement);
                        }
                        else
                        {
                            infectedPartyBase_OtherSide.AddElementToMemberRoster(@object, 1, true);
                        }
                        //infectedPartyBase_OtherSide.MapEventSide = MobileParty.MainParty.Party.MapEventSide.OtherSide;
                        infectedPartyBase_Now = infectedPartyBase_OtherSide;
                    }
                }
                catch
                {

                }

                // Agent
                PartyAgentOrigin troopOrigin = new PartyAgentOrigin(infectedPartyBase_Now, @object, -1, default(UniqueTroopDescriptor), false);
                Agent agent = Mission.Current.SpawnTroop(troopOrigin, isPlayerSide, false, enableMount && @object.HasMount(), false, 1, 1, true, true, false, null, null, null, null, FormationClass.NumberOfAllFormations, false);
                agent.TeleportToPosition(affectedAgent.Position);
                agent.Character.IsFemale = true;
                agent.Formation?.SetMovementOrder(MovementOrder.MovementOrderCharge);

                // Apperance
                if (isPlayerSide && InfectPatch.playerSideInfectedTroopContourLineMarking != "None")
                {
                    uint color = 0;
                    switch (InfectPatch.playerSideInfectedTroopContourLineMarking)
                    {
                        case "Black": color = Colors.Black.ToUnsignedInteger(); break;
                        case "White": color = Colors.White.ToUnsignedInteger(); break;
                        case "Red": color = Colors.Red.ToUnsignedInteger(); break;
                        case "Yellow": color = Colors.Yellow.ToUnsignedInteger(); break;
                        case "Green": color = Colors.Green.ToUnsignedInteger(); break;
                        case "Blue": color = Colors.Blue.ToUnsignedInteger(); break;
                        case "Cyan": color = Colors.Cyan.ToUnsignedInteger(); break;
                        case "Magenta": color = Colors.Magenta.ToUnsignedInteger(); break;
                        case "Gray": color = Colors.Gray.ToUnsignedInteger(); break;
                    }
                    MBAgentVisuals agentVisuals = agent.AgentVisuals;
                    agentVisuals?.SetContourColor(new uint?(color), true);
                    Agent mountAgent = agent.MountAgent;
                    if (mountAgent != null)
                    {
                        MBAgentVisuals mountAgentVisuals = mountAgent.AgentVisuals;
                        mountAgentVisuals?.SetContourColor(new uint?(color), true);
                    }
                }

                if (!isPlayerSide && InfectPatch.enemySideInfectedTroopContourLineMarking != "None")
                {
                    uint color = 0;
                    switch (InfectPatch.enemySideInfectedTroopContourLineMarking)
                    {
                        case "Black": color = Colors.Black.ToUnsignedInteger(); break;
                        case "White": color = Colors.White.ToUnsignedInteger(); break;
                        case "Red": color = Colors.Red.ToUnsignedInteger(); break;
                        case "Yellow": color = Colors.Yellow.ToUnsignedInteger(); break;
                        case "Green": color = Colors.Green.ToUnsignedInteger(); break;
                        case "Blue": color = Colors.Blue.ToUnsignedInteger(); break;
                        case "Cyan": color = Colors.Cyan.ToUnsignedInteger(); break;
                        case "Magenta": color = Colors.Magenta.ToUnsignedInteger(); break;
                        case "Gray": color = Colors.Gray.ToUnsignedInteger(); break;
                    }
                    MBAgentVisuals agentVisuals = agent.AgentVisuals;
                    agentVisuals?.SetContourColor(new uint?(color), true);
                    Agent mountAgent = agent.MountAgent;
                    if (mountAgent != null)
                    {
                        MBAgentVisuals mountAgentVisuals = mountAgent.AgentVisuals;
                        mountAgentVisuals?.SetContourColor(new uint?(color), true);
                    }
                }

                // AgentFadOut
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
        private static readonly Random random = new Random();
    }
}
