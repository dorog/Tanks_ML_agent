using MLAgents;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementAgent : Agent
{
    public RayPerception3D movementRayPerception;
    public TankBattleArenaManager battleArenaManager;
    public TankMovement tankMovement;
    public float range;
    public LayerMask mask;

    public Destroyer destroyer;

    public override void CollectObservations()
    {
        AddVectorObs(battleArenaManager.GetDataForShoot(null, gameObject));

        float rayDistance = 20f;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "wall", "Agent" };
        AddVectorObs(movementRayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveTank(vectorAction[0]);
        RotateTank(vectorAction[1]);

        Reward();
    }


    private void MoveTank(float moveAction)
    {
        float amount = Mathf.Clamp(moveAction, -1, 1);

        tankMovement.AddMovement(amount);
    }

    private void RotateTank(float rotateAction)
    {
        float amount = Mathf.Clamp(rotateAction, -1, 1);

        tankMovement.AddRotation(amount);
    }

    private void Reward()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
        if (colliders.Length == 0)
        {
            AddReward(-.001f);
        }
        else
        {
            AddReward(.001f);
        }
    }

    public override void AgentOnDone()
    {
        destroyer.Done();
    }
}
