using MLAgents;
using UnityEngine;

public class AgentTankHealth : AbstractTankHealth
{
    public Destroyer destroyer;
    public TankMovementAgent agent;
    public TankBattleArenaManager tankBattleArenaManager;

    protected override void LastWish()
    {
        agent.Done();
        //tankBattleArenaManager.Done(gameObject);
        //destroyer.Done();
    }

    protected override void Extra()
    {
        base.Extra();
        agent.AddReward(-1f);
    }
}
