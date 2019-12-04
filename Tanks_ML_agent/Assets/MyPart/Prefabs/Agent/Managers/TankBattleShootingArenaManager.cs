using UnityEngine;

public class TankBattleShootingArenaManager : TankBattleArenaManager
{
    public GameObject target;
    public GameObject shooterTank;

    public Transform[] targetTankLocations;

    private GameObject targetTank = null;

    private TankShooterAgent agent;

    protected override void StartNewRound()
    {
        base.StartNewRound();

        if (targetTank != null)
        {
            Destroy(targetTank);
        }
        CreatePractiseTank();
    }

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        GameObject AgentObj = Instantiate(shooterTank, transform);

        TankShooterAgent tankShooterAgent = AgentObj.GetComponentInChildren<TankShooterAgent>();
        tankShooterAgent.battleArenaManager = this;
        tankShooterAgent.target = targetTank;
        tankShooterAgent.search = "Red";

        Destroyer destroyer = AgentObj.GetComponent<Destroyer>();
        destroyer.tankBattleArenaManager = this;

        agent = tankShooterAgent;

        return AgentObj;
    }

    private void CreatePractiseTank()
    {
        int place = Random.Range(0, targetTankLocations.Length);
        GameObject practiseTank = Instantiate(target, targetTankLocations[place]);

        PractiseTankController practiseTankController = practiseTank.GetComponent<PractiseTankController>();
        practiseTankController.amount = Random.Range(10f, 15f);

        PractiseTankhealth practiseTankhealth = practiseTank.GetComponent<PractiseTankhealth>();
        practiseTankhealth.battleArenaManager = this;

        targetTank = practiseTank;

        agent.target = targetTank;
    }

    public override void TargetTankDied()
    {
        targetTank = null;

        //CreatePractiseTank();
        agent.AddReward(1f);
        agent.Done();
    }
}
