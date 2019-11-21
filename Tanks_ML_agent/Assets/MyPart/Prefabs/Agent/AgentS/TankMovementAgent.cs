using MLAgents;
using UnityEngine;

public class TankMovementAgent : Agent
{
    public RayPerception3D movementRayPerception;
    public TankBattleArenaManager battleArenaManager;
    public GameObject target;
    public TankMovement tankMovement;
    public float range = 32f;

    public Destroyer destroyer;

    public override void CollectObservations()
    {
        float rayDistance = 20f;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "wall", "Agent" };
        AddVectorObs(movementRayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));

        AddVectorObs(battleArenaManager.GetDataForMove(gameObject, target));
        //AddVectorObs(tankMovement.tankMovementSettings.Speed);
        //AddVectorObs(tankMovement.tankMovementSettings.TurnSpeed);
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
        float distance = battleArenaManager.GetDistance(gameObject, target);
        if (distance > range)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-0.5f);
        }
    }
}
