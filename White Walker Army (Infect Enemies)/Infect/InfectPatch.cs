using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using White_Walker_Army__Infect_Enemies_.MCMSettings;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    [HarmonyPatch(typeof(Mission), "OnAgentRemoved")]
    public class InfectPatch
    {
        // character
        public static bool enablePlayer = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnablePlayer;
        public static bool enablePlayerTeamHero = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnablePlayerTeamHero;
        public static bool enablePlayerTeamSolider = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnablePlayerTeamSolider;
        public static bool enableAllyTeamHero = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableAllyTeamHero;
        public static bool enableAllyTeamSolider = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableAllyTeamSolider;
        public static bool enableEnemyTeamHero = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableEnemyTeamHero;
        public static bool enableEnemyTeamSolider = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableEnemyTeamSolider;

        // BattleType
        public static bool enableInField = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableInFieldBattle;
        public static bool enableInSiege = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableInSiegeBattle;
        public static bool enableInSallyOut = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableInSallyOutBattle;
        public static bool enableInSiegeOutside = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableInSiegeOutsideBattle;
        public static bool enableInHideout = GlobalSettings<MCMSettings.MCMSettings>.Instance.EnableInHideoutBattle;

        // Settings
        public static int durations = GlobalSettings<MCMSettings.MCMSettings>.Instance.Durations;
        public static bool infectedTroopCanInfectOthers = GlobalSettings<MCMSettings.MCMSettings>.Instance.InfectTroopCanInfectOthers;
        public static bool infectedTroopCanBeInfectedAgain = GlobalSettings<MCMSettings.MCMSettings>.Instance.InfectedTroopCanbeInfectedAgain;
        //public static bool canInfectHero = GlobalSettings<MCMSettings.MCMSettings>.Instance.CanInfectHero;

        public static void Postfix(Agent affectedAgent, Agent affectorAgent)
        {
            if (affectorAgent != null && affectedAgent != null && affectorAgent.Character != null && affectedAgent.Character != null && affectedAgent.IsHuman && !affectedAgent.IsHero)
            {
                if (affectorAgent.Team.IsValid && affectedAgent.Team.IsValid && affectedAgent.Team.IsEnemyOf(affectorAgent.Team) && (affectedAgent.State == AgentState.Killed || affectedAgent.State == AgentState.Unconscious))
                {
                    if (CheckCharacter(affectorAgent) && CheckBattleType(MobileParty.MainParty.MapEvent.EventType))
                    {
                        if (CampaignMission.Current.Mode == MissionMode.Tournament || Mission.Current.IsSiegeBattle || MobileParty.MainParty.MapEvent.EventType == MapEvent.BattleTypes.Hideout)
                        {
                            Infect.SpawnTroop(affectorAgent, affectedAgent, durations, false);
                        }
                        else
                        {
                            Infect.SpawnTroop(affectorAgent, affectedAgent, durations, true);
                        }
                    }
                }
            }
        }

        public static bool CheckCharacter(Agent affectorAgent)
        {
            PartyBase affectorParty = Infect.GetaffectorParty(affectorAgent);
            // Player
            if (enablePlayer && affectorAgent.IsMainAgent)
                return true;
            // PlayerTeamHero
            if (enablePlayerTeamHero && affectorAgent.IsHero && !affectorAgent.IsMainAgent && affectorAgent.Team.IsPlayerTeam && affectorParty.MobileParty.IsMainParty)
                return true;
            // PlayerTeamSolider
            if (enablePlayerTeamSolider && !affectorAgent.IsHero && affectorAgent.Team.IsPlayerTeam && affectorParty.MobileParty.IsMainParty)
                return true;
            //AllyTeamHero
            if (enableAllyTeamHero && affectorAgent.IsHero && affectorAgent.Team.IsPlayerAlly && !affectorParty.MobileParty.IsMainParty)
                return true;
            // AllyTeamSolider
            if (enableAllyTeamSolider && !affectorAgent.IsHero && affectorAgent.Team.IsPlayerAlly && !affectorParty.MobileParty.IsMainParty)
                return true;
            // EnemyTeamHero
            if (enableEnemyTeamHero && affectorAgent.IsHero && affectorParty.MapEventSide == MobileParty.MainParty.Party.MapEventSide.OtherSide)
                return true;
            // EnemyTeamSolider
            if (enableEnemyTeamSolider && !affectorAgent.IsHero && affectorParty.MapEventSide == MobileParty.MainParty.Party.MapEventSide.OtherSide)
                return true;
            return false;
        }

        public static bool CheckBattleType(MapEvent.BattleTypes mapEvent)
        {
            bool result;
            switch (mapEvent)
            {
                case MapEvent.BattleTypes.FieldBattle:
                    result = enableInField;
                    break;
                case MapEvent.BattleTypes.Siege:
                    result = enableInSiege;
                    break;
                case MapEvent.BattleTypes.Hideout:
                    result = enableInHideout;
                    break;
                case MapEvent.BattleTypes.SallyOut:
                    result = enableInSallyOut;
                    break;
                case MapEvent.BattleTypes.SiegeOutside:
                    result = enableInSiegeOutside;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
