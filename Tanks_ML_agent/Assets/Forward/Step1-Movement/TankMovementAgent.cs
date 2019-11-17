using Complete;
using MLAgents;
using UnityEngine;

namespace Test {
[RequireComponent(typeof(Rigidbody))]
public class TankMovementAgent : Agent
{
    public float nostrilWidth = 2f;

    public GameObject otherTank;

    public TankMovementReset tankMovementReset;
    public TankMovement tankMovement;
    public RayPerception3D rayPerception;
    public Rigidbody agentRigidbody;

    public override void CollectObservations()
    {
        Debug.Log(IsDone());
        float rayDistance = 20f;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "Finish", "wall" };
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));

        AddVectorObs(GetNostrilStereo());

        Vector3 localVelocity = transform.InverseTransformDirection(agentRigidbody.velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveTank(vectorAction[0]);
        RotateTank(vectorAction[1]);

        if (GetCumulativeReward() <= -5f)
        {
            Done();
        }
        else
        {
            AddReward(-.001f);
        }
    }

    public override void AgentReset()
    {
        agentRigidbody.velocity = Vector3.zero;
        tankMovementReset.Reset();
    }

    private Vector2 GetNostrilStereo()
    {
        float leftNostril = 0;
        Vector3 leftNostrilPosition = transform.position - nostrilWidth / 2.0f * transform.right;
        float rightNostril = 0;
        Vector3 rightNostrilPosition = transform.position + nostrilWidth / 2.0f * transform.right;

        leftNostril += .8f - .5f * Mathf.Log10(Vector3.Distance(otherTank.transform.position, leftNostrilPosition));
        rightNostril += .8f - .5f * Mathf.Log10(Vector3.Distance(otherTank.transform.position, rightNostrilPosition));

        return new Vector2(leftNostril, rightNostril);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-.01f);
        }
    }

    private void CollectTruffle()
    {
        AddReward(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            AddReward(1);
            Done();
        }
    }

    private void MoveTank(float moveAction)
    {
        int index = Mathf.FloorToInt(moveAction);

        if (index == 0)
        {
            tankMovement.AddMovement(-1);
        }
        else if (index == 1)
        {
            tankMovement.AddMovement(1);
        }
    }

    private void RotateTank(float rotateAction)
    {
        int index = Mathf.FloorToInt(rotateAction);

        if (index == 0)
        {
            tankMovement.AddRotation(-1);

        }
        else if (index == 1)
        {
            tankMovement.AddRotation(1);
        }
    }
}
}
