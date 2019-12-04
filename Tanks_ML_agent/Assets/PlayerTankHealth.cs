
public class PlayerTankHealth : AbstractTankHealth
{
    public AIvsPlayerBattleManager aiVSplayerBattleManager;

    protected override void LastWish()
    {
        aiVSplayerBattleManager.Done(gameObject);
        Destroy(gameObject);
    }
}
