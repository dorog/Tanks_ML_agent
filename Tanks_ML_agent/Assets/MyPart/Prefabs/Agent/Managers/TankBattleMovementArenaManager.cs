using System.Collections.Generic;
using UnityEngine;

public class TankBattleMovementArenaManager : TankBattleArenaManager
{
    public TankAgent TankPrefab;

    public GameObject TankWithTwoBrain;

    public override List<float> GetTargetData(GameObject tank)
    {
        throw new System.NotImplementedException();
    }

    protected override GameObject InstantiateTank(Transform transform, Material material)
    {
        GameObject AgentObj = Instantiate(TankWithTwoBrain, transform);

        tanks.Add(AgentObj);

        TankMovementAgent tankMovementAgent = AgentObj.GetComponent<TankMovementAgent>();
        tankMovementAgent.battleArenaManager = this;

        TankShooterAgent tankShooterAgent = AgentObj.GetComponent<TankShooterAgent>();
        tankShooterAgent.battleArenaManager = this;

        Destroyer destroyer = AgentObj.GetComponent<Destroyer>();
        destroyer.tankBattleArenaManager = this;

        ColorSetter colorSetter = AgentObj.GetComponent<ColorSetter>();
        colorSetter.SetColor(material);

        return AgentObj;
    }
}
