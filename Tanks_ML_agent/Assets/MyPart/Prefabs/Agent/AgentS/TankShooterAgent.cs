using MLAgents;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TankShooterAgent : Agent
{
    public TankShooting tankShooting;
    public RayPerception3D turretRayPerception;
    public TankBattleArenaManager battleArenaManager;
    public Destroyer destroyer;

    public GameObject turret;
    public float range = 30f;

    public bool ShowObservations = false;

    public static float bestReward = -10f;

    public override void CollectObservations()
    {
        if (ShowObservations)
        {
            ShowObservations = false;
           
            Debug.Log(tankShooting.CanFire());
            foreach (var item in battleArenaManager.GetTargetData(turret))
            {
                Debug.Log(item);
            }
        }
        AddVectorObs(tankShooting.CanFire());

        AddVectorObs(battleArenaManager.GetTargetData(turret));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        RotateTurret(vectorAction[0]);
        Fire(vectorAction[1]);
        AddReward(-.001f);
    }

    private void RotateTurret(float amount)
    {
        float rotation = Mathf.Clamp(amount, -1, 1);
        tankShooting.AddTurn(rotation);
    }

    private void Fire(float chance)
    {
        float fire = Mathf.Clamp(chance, -1, 1);
        if (fire > 0)
        {
            tankShooting.Fire(true, this);

            /*if (Hit())
            {
                AddReward(1f);
            }*/
        }
    }

    private bool Hit()
    {
        float rayDistance = range;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "Tank" };

        List<float> result = turretRayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);

        return result[0] == 0 ? false : true;
    }

    public override void AgentOnDone()
    {
        destroyer.Done();
        /*if(bestReward < GetCumulativeReward())
        {
            bestReward = GetCumulativeReward();
            Debug.Log(bestReward + " " + DateTime.Now);
        }*/
    }
}
