using UnityEngine;

public class PractiseTankhealth : AbstractTankHealth
{
    public TankBattleShootingArenaManager TankBattleShootingArenaManager;

    protected override void LastWish()
    {
        TankBattleShootingArenaManager.TargetTankDied();

        Destroy(gameObject);
    }
}
