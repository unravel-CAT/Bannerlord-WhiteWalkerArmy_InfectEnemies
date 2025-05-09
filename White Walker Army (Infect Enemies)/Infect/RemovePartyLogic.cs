using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    public class RemovePartyLogic : MissionLogic
    {
        public RemovePartyLogic() { }
        public override void OnBattleEnded()
        {
            base.OnBattleEnded();
            for (int i = 0; i < Campaign.Current.MobileParties.Count; ++i)
            {
                MobileParty mobileParty = Campaign.Current.MobileParties[i];
                if (mobileParty.Party.Id.ToString().Contains("infected"))
                {
                    Campaign.Current.MobileParties.Remove(mobileParty);
                }
            }
        }
    }
}
