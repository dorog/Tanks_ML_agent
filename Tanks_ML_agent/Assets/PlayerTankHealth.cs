using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankHealth : AbstractTankHealth
{
    public AIvsPlayerBattleManager aiVSplayerBattleManager;

    protected override void LastWish()
    {
        aiVSplayerBattleManager.Done(gameObject);
    }
}
