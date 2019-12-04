
public class AgentTankHealth : AbstractTankHealth
{
    public TankMovementAgent movementAgent;
    public TankShooterAgent shoterAgent;

    protected override void LastWish()
    {
        movementAgent.Done();
        shoterAgent.Done();
    }

    protected override void Extra()
    {
        base.Extra();
        movementAgent.SetReward(-1f);
    }
}
