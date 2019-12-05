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

    public TanksUImanager uiManager;

    public Transform[] defaultCameraTransforms;

    private bool roundEnd = false;
    private bool playerLose = false;

    private void Start(){}

    private void OnEnable()
    {
        StartNewRound();
    }

    public void StartRound()
    {
        StartNewRound();
    }

    protected override void StartNewRound()
    {
        roundEnd = false;
        if(player != null)
        {
            Destroy(player);
        }
        if (playerLose)
        {
            Destroy(tanks[0]);
            playerLose = false;
        }

        supportManager.ResetMap();
        base.StartNewRound();

        uiManager.Count(3);
    }

    public void StartBattle()
    {
        Transform[] transforms = new Transform[tanks.Count];
        for (int i = 0; i < tanks.Count; i++)
        {
            TankShooting tankShooting = tanks[i].GetComponent<TankShooting>();
            TankMovement tankMovement = tanks[i].GetComponent<TankMovement>();

            tankShooting.enabled = true;
            tankShooting.canFire = true;
            tankMovement.enabled = true;

            transforms[i] = tanks[i].transform;
        }

        cameraControl.m_Targets = transforms;
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

        SetAttributes(redTank, spawn2Mat, "Blue", player);

        tanks.Add(redTank);
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

    public override void Done(GameObject gameObject)
    {
        if (!roundEnd)
        {
            roundEnd = true;
            cameraControl.m_Targets = defaultCameraTransforms;

            if (gameObject == player)
            {
                playerLose = true;
                player = null;

                DisableAi();

                RemoveTank(gameObject);
                uiManager.PlayerLose();
            }
            else
            {
                DisablePlayer();
                uiManager.PlayerWin();
            }
        }
    }

    private void DisableAi()
    {
        TankMovementAgent tankMovementAgent = tanks[0].GetComponent<TankMovementAgent>();
        tankMovementAgent.enabled = false;
        TankShooterAgent tankShooterAgent = tanks[0].GetComponentInChildren<TankShooterAgent>();
        tankShooterAgent.enabled = false;
    }

    private void DisablePlayer()
    {
        PlayerMovementController playerMovementController = player.GetComponent<PlayerMovementController>();
        playerMovementController.enabled = false;
        PlayerTurretController playerTurretController = player.GetComponent<PlayerTurretController>();
        playerTurretController.enabled = false;
    }
}
