using MLAgents;
using UnityEngine;

public class TankBattleMovementArenaManager : TankBattleArenaManager
{
    public GameObject TankPrefab;

    public GameObject ShootingTank;

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        GameObject AgentObj = Instantiate(TankPrefab, transform);

        TankMovementAgent tankMovementAgent = AgentObj.GetComponent<TankMovementAgent>();
        tankMovementAgent.battleArenaManager = this;
        tankMovementAgent.target = ShootingTank;

        Destroyer destroyer = AgentObj.GetComponent<Destroyer>();
        destroyer.tankBattleArenaManager = this;

        AgentTankHealth agentTankHealth = AgentObj.GetComponent<AgentTankHealth>();
        agentTankHealth.tankBattleArenaManager = this;

        TankShooterAgent tankShooterAgent = ShootingTank.GetComponent<TankShooterAgent>();
        tankShooterAgent.target = AgentObj;

        return AgentObj;
    }
}
