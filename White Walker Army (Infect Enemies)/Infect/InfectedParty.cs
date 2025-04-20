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

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    public class InfectedParty
    {
        public InfectedParty(PartyBase affectorParty)
        {
            if (affectorParty.MobileParty.IsBandit)
            {
                CreateBanditParty(affectorParty);
            }
            else if (affectorParty.MobileParty.IsCaravan)
            {
                CreateCaravanParty(affectorParty);
            }
            else if (affectorParty.MobileParty.IsMilitia)
            {
                CreateMilitiaParty(affectorParty);
            }
            else if (affectorParty.MobileParty.IsVillager)
            {
                CreateVillagerParty(affectorParty);
            }
            else if (affectorParty.MobileParty.IsGarrison)
            {
                CreateGarrisonParty(affectorParty);
            }
            else if (affectorParty.MobileParty.IsLordParty)
            {
                CreateLordParty(affectorParty);
            }
        }

        public void InitParty()
        {
            _party.SetCustomName(new TextObject("{=WWA_Infected_Troop}Infected Troop", null));
            _party.SetPartyObjective(MobileParty.PartyObjective.Aggressive);
            _party.SetPartyUsedByQuest(true);
            _party.Party.SetVisualAsDirty();
            _party.Aggressiveness = 100f;
        }

        public void CreateLordParty(PartyBase affectorParty)
        {
            PartyComponent.OnPartyComponentCreatedDelegate initParty = delegate (MobileParty party)
            {
                party.ChangePartyLeader(affectorParty.MobileParty.LeaderHero);
                party.ActualClan = affectorParty.MobileParty.ActualClan;
                party.SetPartyObjective(MobileParty.PartyObjective.Aggressive);
                party.SetPartyUsedByQuest(true);
            };
            _party = MobileParty.CreateParty("infected_troop", new CustomPartyComponent(), initParty);
            InitParty();
        }

        public void CreateBanditParty(PartyBase affectorParty)
        {
            BanditPartyComponent banditPartyComponent = affectorParty.MobileParty.BanditPartyComponent;
            _party = BanditPartyComponent.CreateBanditParty("infected_party", banditPartyComponent.Clan, banditPartyComponent.Hideout, banditPartyComponent.IsBossParty);
            InitParty();
        }

        public void CreateCaravanParty(PartyBase affectorParty)
        {
            CaravanPartyComponent caravanPartyComponent = affectorParty.MobileParty.CaravanPartyComponent;
            _party = CaravanPartyComponent.CreateCaravanParty(caravanPartyComponent.Owner, caravanPartyComponent.Settlement);
            InitParty();
        }

        public void CreateMilitiaParty(PartyBase affectorParty)
        {
            _party = MilitiaPartyComponent.CreateMilitiaParty("infected_troop", affectorParty.MobileParty.HomeSettlement);
            InitParty();
        }

        public void CreateVillagerParty(PartyBase affectorParty)
        {
            _party = VillagerPartyComponent.CreateVillagerParty("infected_troop", affectorParty.MobileParty.HomeSettlement.Village, 1);
            InitParty();
        }

        public void CreateGarrisonParty(PartyBase affectorParty)
        {
            _party = GarrisonPartyComponent.CreateGarrisonParty("infected_troop", affectorParty.MobileParty.HomeSettlement, false);
            InitParty();
        }

        public MobileParty _party;
    }
}
