using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

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

        public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
        {
            base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
            affectedAgent.AgentVisuals.SetContourColor(null);
            if (affectedAgent.HasMount && affectedAgent.MountAgent != null)
                affectedAgent.MountAgent.AgentVisuals?.SetContourColor(null);
        }

        public override void OnMissionTick(float dt)
        {
            _durations -= dt;
            if (_durations < 0f && !_hasFadOut && _agent != null && Mission.Current != null)
            {
                foreach (Agent agent in Mission.Current.Agents)
                {
                    if (agent == _agent)
                    {
                        _hasFadOut = true;
                        _agent.FadeOut(false, true);
                        break;
                    }
                }
            }
        }
      
        private float _durations;
        private readonly Agent _agent;
        private bool _hasFadOut;
    }
}
