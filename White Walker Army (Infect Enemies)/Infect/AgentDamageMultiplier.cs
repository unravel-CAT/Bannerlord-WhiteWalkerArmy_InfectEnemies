using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SandBox.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    [HarmonyPatch(typeof(SandboxAgentApplyDamageModel), "CalculateDamage")]
    internal class AgentDamageMultiplier
    {
        [HarmonyPostfix]
        public static void CalculateDamage(AttackInformation attackInformation, AttackCollisionData collisionData, WeaponComponentData weapon, ref float __result)
        {
            PartyBase attackerParty = Infect.GetaffectorParty(attackInformation.AttackerAgent);
            PartyBase victimParty = Infect.GetaffectorParty(attackInformation.VictimAgent);
            if (attackerParty != null && !attackInformation.IsFriendlyFire && attackerParty.Id.Contains("infected"))
            {
                __result *= InfectPatch.DamageMultiplier;
            }

            if (victimParty != null && !attackInformation.IsFriendlyFire && victimParty.Id.Contains("infected"))
            {
                __result *= InfectPatch.DamageTakenMultiplier;
            }
        }
    }
}
