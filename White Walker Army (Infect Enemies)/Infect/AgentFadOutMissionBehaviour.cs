using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    internal class AgentFadOutMissionBehaviour : MissionBehavior
    {
        public AgentFadOutMissionBehaviour(Agent agent, float durations)
        {
            this._agent = agent;
            this._durations = durations;
            this._hasFadOut = false;
        }
        public override MissionBehaviorType BehaviorType
        {
            get
            {
                return MissionBehaviorType.Other;
            }
        }

        public override void OnMissionTick(float dt)
        {
            if (this._hasFadOut) { return; }
            this._durations -= dt;
            if (this._durations < 0f && this._agent != null && this._agent.State == AgentState.Active && !_hasFadOut)
            {
                _hasFadOut = true;
                _agent.FadeOut(false, true);
            }        
        }

        protected override void OnEndMission()
        {
            //Mission.Current.RemoveMissionBehavior(this);
        }

        private float _durations;
        private Agent _agent;
        private bool _hasFadOut;
    }
}
