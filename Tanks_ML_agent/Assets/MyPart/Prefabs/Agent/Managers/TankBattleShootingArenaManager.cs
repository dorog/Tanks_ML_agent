using System.Collections.Generic;
using UnityEngine;

public class TankBattleShootingArenaManager : TankBattleArenaManager
{
    public GameObject target;
    public GameObject shooterTank;

    public Transform[] targetTankLocations;

    private GameObject targetTank = null;

    protected override void StartNewRound()
    {
        base.StartNewRound();

        if(targetTank != null)
        {
            Destroy(targetTank);
        }
        CreatePractiseTank();
    }

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        GameObject AgentObj = Instantiate(shooterTank, Places[0]);

        TankShooterAgent tankShooterAgent = AgentObj.GetComponent<TankShooterAgent>();
        tankShooterAgent.battleArenaManager = this;

        Destroyer destroyer = AgentObj.GetComponent<Destroyer>();
        destroyer.tankBattleArenaManager = this;

        return AgentObj;
    }

    private void CreatePractiseTank()
    {
        int place = Random.Range(0, targetTankLocations.Length);
        GameObject practiseTank = Instantiate(target, targetTankLocations[place]);

        PractiseTankController practiseTankController = practiseTank.GetComponent<PractiseTankController>();
        practiseTankController.amount = Random.Range(10f, 15f);

        PractiseTankhealth practiseTankhealth = practiseTank.GetComponent<PractiseTankhealth>();
        practiseTankhealth.TankBattleShootingArenaManager = this;

        targetTank = practiseTank;
    }

    public override List<float> GetTargetData(GameObject tank)
    {
        List<float> targetData = new List<float>();

        /*Debug.Log("----------------------");
        foreach(float item in GetDataForShoot(tank, targetTank))
        {
            Debug.Log(item);
        }
        Debug.Log("----------------------");*/
        targetData.AddRange(GetDataForShoot(tank, targetTank));

        return targetData;
    }

    public void TargetTankDied()
    {
        targetTank = null;

        CreatePractiseTank();
    }
}
