using UnityEngine;

public class AIvsPlayerBattleManager : TankBattleArenaManager
{
    public GameObject playerTank;
    public GameObject aiTank;

    public Transform[] positions;
    public Material spawn2Mat;

    public SupportManager supportManager;

    public Complete.CameraControl cameraControl;

    private GameObject player;

    protected override void StartNewRound()
    {
        if(player != null)
        {
            Destroy(player);
        }

        supportManager.ResetMap();
        base.StartNewRound();
    }

    public override void TargetTankDied()
    {
        //throw new System.NotImplementedException();
    }

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        player = Instantiate(playerTank, transform);
        player.tag = "BlueTank";

        PlayerTankHealth playerTankHealth = player.GetComponent<PlayerTankHealth>();
        playerTankHealth.aiVSplayerBattleManager = this;

        ColorSetter colorSetter = player.GetComponent<ColorSetter>();
        colorSetter.SetColor(material);

        int range = Random.Range(0, positions.Length);
        GameObject redTank = Instantiate(aiTank, positions[range]);
        redTank.tag = "RedTank";

        SetAttributes(redTank, spawn2Mat, "Blue", redTank);

        tanks.Add(redTank);

        SetCameraTargets(player.transform, redTank.transform);
        return player;
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

    private void SetCameraTargets(Transform player, Transform ai)
    {
        // Create a collection of transforms the same size as the number of tanks.
        Transform[] targets = new Transform[2];

        targets[0] = ai;
        targets[1] = player;


        // These are the targets the camera should follow.
        cameraControl.m_Targets = targets;
    }
}
