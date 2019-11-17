using MLAgents;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankAgent : Agent
{
    public float nostrilWidth = 2f;

    public TankBattleArenaManager tankBattleArenaManager;
    public TankMovement tankMovement;
    public TankShooting tankShooting;
    public RayPerception3D movementRayPerception;
    public RayPerception3D turretRayPerception;
    public Rigidbody agentRigidbody;

    private readonly float giveUpPoint = -5f;

    public override void CollectObservations()
    {
        AddVectorObs(tankShooting.CanFire());

        float rayDistance = 20f;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "wall", "Agent" };
        AddVectorObs(movementRayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));

        AddVectorObs(turretRayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));

        AddNostrilStereo();

        Vector3 localVelocity = transform.InverseTransformDirection(agentRigidbody.velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
    }

    private void AddNostrilStereo()
    {
        List<GameObject> tanks = tankBattleArenaManager.GetTanks();

        float leftNostril = 0;
        Vector3 leftNostrilPosition = transform.position - nostrilWidth / 2.0f * transform.right;
        float rightNostril = 0;
        Vector3 rightNostrilPosition = transform.position + nostrilWidth / 2.0f * transform.right;

        foreach (GameObject tank in tanks)
        {
            if (tank != null)
            {
                leftNostril += .8f - .5f * Mathf.Log10(Vector3.Distance(tank.transform.position, leftNostrilPosition));
                rightNostril += .8f - .5f * Mathf.Log10(Vector3.Distance(tank.transform.position, rightNostrilPosition));
            }
        }

        AddVectorObs(new Vector2(leftNostril, rightNostril));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveTank(vectorAction[0]);
        RotateTank(vectorAction[1]);
        HandleTurret(vectorAction[2]);

        if (GetCumulativeReward() <= giveUpPoint)
        {
            Done();
        }
        else
        {
            AddReward(-.001f);
        }
    }

    /*private void AddLocations(List<Transform> locations)
    {
        int myTransform = -1;
        for(int i = 0; i < locations.Count; i++)
        {
            if(locations[i] != transform)
            {
                AddVectorObs(locations[i]);
            }
            else
            {
                myTransform = i;
            }
        }
        if(myTransform != -1)
        {
            AddVectorObs(locations[myTransform]);
        }
        else
        {
            Debug.LogError("Not found my transform!");
        }
    }*/


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

    private void HandleTurret(float turretAction)
    {
        int index = Mathf.FloorToInt(turretAction);

        switch (index)
        {
            case 0:
                tankShooting.AddTurn(-1);
                break;
            case 1:
                tankShooting.AddTurn(1);
                break;
            case 2:
                tankShooting.Fire(true, this);
                break;
        }
    }

    public override void AgentOnDone()
    {
        tankBattleArenaManager.Done(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-.01f);
        }
    }
}
