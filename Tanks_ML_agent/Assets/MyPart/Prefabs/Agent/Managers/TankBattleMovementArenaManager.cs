using UnityEngine;

public class TankBattleMovementArenaManager : TankBattleArenaManager
{
    public GameObject TankPrefab;
    public GameObject ShootingTank;
    public Transform[] positions;

    public Material spawn2Mat;

    private GameObject redTank;
    private GameObject blueTank;

    public SupportManager supportManager;

    protected override void StartNewRound()
    {
        supportManager.ResetMap();
        base.StartNewRound();
    }

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        blueTank = Instantiate(TankPrefab, transform);
        blueTank.tag = "BlueTank";

        int range = Random.Range(0, positions.Length);
        redTank = Instantiate(TankPrefab, positions[range]);
        redTank.tag = "RedTank";

        SetAttributes(blueTank, material, "Red", redTank);
        SetAttributes(redTank, spawn2Mat, "Blue", blueTank);

        tanks.Add(redTank);
        return blueTank;
    }

    private void SetAttributes(GameObject tank, Material mat, string tag, GameObject enemy)
    {
        TankMovementAgent tankMovementAgent = tank.GetComponent<TankMovementAgent>();
        tankMovementAgent.battleArenaManager = this;
        tankMovementAgent.target = enemy;
        tankMovementAgent.Search = tag;

        TankShooterAgent tankShooterAgent = tank.GetComponentInChildren<TankShooterAgent>();
        tankShooterAgent.battleArenaManager = this;
        tankShooterAgent.target = enemy;
        tankShooterAgent.search = tag;

        Destroyer destroyer = tank.GetComponent<Destroyer>();
        destroyer.tankBattleArenaManager = this;

        ColorSetter colorSetter = tank.GetComponent<ColorSetter>();
        colorSetter.SetColor(mat);
    }

    public override void TargetTankDied()
    {
        //Done(target);
    }
}
