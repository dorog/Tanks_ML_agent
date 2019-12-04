using UnityEngine;

public class PractiseTankhealth : AbstractTankHealth
{
    public TankBattleArenaManager battleArenaManager;

    protected override void LastWish()
    {
        battleArenaManager.TargetTankDied();

        Destroy(gameObject);
    }
}
