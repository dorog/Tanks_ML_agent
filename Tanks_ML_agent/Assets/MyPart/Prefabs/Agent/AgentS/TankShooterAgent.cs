using MLAgents;
using UnityEngine;

public class TankShooterAgent : Agent
{
    public TankShooting tankShooting;
    public RayPerception3D turretRayPerception;
    public TankBattleArenaManager battleArenaManager;
    public GameObject target;
    public Destroyer destroyer;
    public string search = "Tank";

    public GameObject turret;
    public float range = 30f;

    public LayerMask tankMask;
    public LayerMask layer;
    public GameObject tankGameObject;

    public override void CollectObservations()
    {
        AddVectorObs(tankShooting.CanFire());

        //AddVectorObs(battleArenaManager.GetDataForShoot(turret, target));
        AddVectorObs(battleArenaManager.GetDataForShootV2(turret, target));

        string[] detectable = { search + "Tank", "Environment" };
        AddVectorObs(turretRayPerception.Perceive(range, new float[] { 90f }, detectable));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        RotateTurret(vectorAction[0]);
        
        Reward(Fire(vectorAction[1]));
    }

    private void Reward(bool fire)
    {
        AddReward(-.001f);

        /*string[] detectable = { search };
        List<float> results = turretRayPerception.Perceive(range, new float[] { 90f }, detectable);*/

        /*if(results[0] == 1)
        {
            if (!fire)
            {
                AddReward(-.001f);
            }
        }
        else
        {
            if (fire)
            {
                AddReward(-.001f);
            }
        }*/

        RaycastHit hit;
        if (Physics.Raycast(turret.transform.position, turret.transform.forward, out hit, 35f, layer))
        {

            Collider[] colliders = Physics.OverlapSphere(hit.transform.position, 5, tankMask);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject == tankGameObject)
                {
                    AddReward(-1f);
                }
            }
        }


    }

    private void RotateTurret(float amount)
    {
        float rotation = Mathf.Clamp(amount, -1, 1);
        tankShooting.AddTurn(rotation);
    }

    private bool Fire(float chance)
    {
        float fire = Mathf.Clamp(chance, -1, 1);
        if (fire > 0)
        {
            tankShooting.Fire(true, this);
            return true;
        }

        return false;
    }

    public override void AgentOnDone()
    {
        destroyer.Done();
    }

    public override float[] Heuristic()
    {
        float rotate = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return new float[] { rotate, 1f };
        }

        return new float[] { rotate, -1f };
    }
}
