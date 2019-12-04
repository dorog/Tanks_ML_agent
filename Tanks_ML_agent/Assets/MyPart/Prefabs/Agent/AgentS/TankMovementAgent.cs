using MLAgents;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementAgent : Agent
{
    public RayPerception3D turretRay;
    public RayPerception3D ray;
    public TankBattleArenaManager battleArenaManager;
    public GameObject target;
    public TankMovement tankMovement;
    public Destroyer destroyer;

    private string search = "Tank";
    private string EnemyTankTag;
    private string EnemyBlockTag;

    public LayerMask layerMask;

    public string Search {
        get
        {
            return search;
        }
        set
        {
            EnemyTankTag = value + "Tank";
            EnemyBlockTag = value + "Location";
        } 
    }

    private const int nothing = 0;
    private const int up = 1;
    private const int down = 2;
    private const int left = 3;
    private const int right = 4;

    public float range = 30f;

    [Header("Angles Settings")]
    public float start = 0;
    public float end = 180;
    public float step = 15;
    private float[] rayAngles;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        List<float> angles = new List<float>();
        for(float i = start; i < end; i += step)
        {
            angles.Add(i);
        }

        angles.Add(270f);

        rayAngles = angles.ToArray();
    }

    public override void CollectObservations()
    {
        const float rayDistance = 60f;

        string[] detectableObjects = { EnemyTankTag, EnemyBlockTag , "Environment", "Good", "Bad"};
        AddVectorObs(ray.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(12f);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var action = Mathf.FloorToInt(vectorAction[0]);

        switch (action)
        {
            case nothing:
                // do nothing
                break;
            case right:
                RotateTank(1);
                break;
            case left:
                RotateTank(-1);
                break;
            case up:
                MoveTank(1);
                break;
            case down:
                MoveTank(-1);
                break;
            default:
                throw new ArgumentException("Invalid action value");
        }

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
        /*float angle = battleArenaManager.GetAngle(gameObject, target) + 90f;
        string[] detect = { EnemyTankTag };
        var result = turretRay.Perceive(30, new float[] { angle }, detect, 0f, 0f);*/
        /*if(gameObject.tag == "RedTank")
        {
            result.ForEach(x => Debug.Log(x));
        }*/

        float angle = battleArenaManager.GetAngle(gameObject, target) + 90f;
        //Debug.Log(angle);
        if (start <= angle && end >= angle)
        {
            Vector3 startPos = transform.position + (target.transform.position - transform.position).normalized * 3f;
            Vector3 direction = (target.transform.position - transform.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(startPos, direction, out hit, 25f, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

                if (hit.collider.gameObject.tag == EnemyTankTag)
                {
                    //AddReward(1f / agentParameters.maxStep);
                    //Debug.Log("InRange");
                    AddReward(1f);
                    //Done();
                }
            }
        }
        else
        {
            AddReward(-1f / agentParameters.maxStep);
        }
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new float[] { right };
        }
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { up };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { left };
        }
        if (Input.GetKey(KeyCode.S))
        {
            return new float[] { down };
        }
        return new float[] { nothing };
    }

    public void EnteredGood()
    {
        /*inGoodBlocks++;
        inBadBlocks++;*/
    }

    public void ExitGood()
    {
        /*inGoodBlocks--;
        if (inGoodBlocks == 1 && inBadBlocks == 0)
        {
            AddReward(1f);
        }*/
    }

    public void EnteredBad()
    {
        SetReward(-1f);
        Done();
    }

    public void ExitBad()
    {
        //inBadBlocks--;
        AddReward(1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            AddReward(-1f);
        }

        if (collision.gameObject.CompareTag(EnemyTankTag))
        {
            AddReward(-.1f);
        }
    }


    public override void AgentOnDone()
    {
        destroyer.Done();
    }
}
