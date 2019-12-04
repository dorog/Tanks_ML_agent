using MLAgents;
using System;
using UnityEngine;

public class PictureLaneAgent : Agent
{
    public int maxZ = 4;
    public int minZ = -4;
    public int maxX = 4;
    public int minX = -4;

    private const int nothing = 0;
    private const int left = 2;
    private const int right = 1;
    private const int up = 3;
    private const int down = 4;

    private Vector3 originalPosition;
    public PictureLaneManager pictureLaneManager;

    public bool writeAction = false;
    public Camera renderCamera;

    public override void CollectObservations()
    {
        SetMask();
    }

    private void SetMask()
    {
        var positionX = (int)transform.position.x;
        var originalX = (int)originalPosition.x;

        var positionZ = (int)transform.position.z;
        var originalZ = (int)originalPosition.z;

        if (positionX - originalX == minX)
        {
            SetActionMask(left);
        }

        if (positionX - originalX == maxX)
        {
            SetActionMask(right);
        }

        if (positionZ - originalZ == minZ)
        {
            SetActionMask(down);
        }

        if (positionZ - originalZ == maxZ)
        {
            SetActionMask(up);
        }
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(-0.01f);
        var action = Mathf.FloorToInt(vectorAction[0]);

        if (writeAction)
        {
            Debug.Log(action);
        }
        switch (action)
        {
            case nothing:
                // do nothing
                break;
            case right:
                Move(Convert.ToInt32(transform.position.x), Convert.ToInt32(originalPosition.x) + maxX, 1, 0);
                break;
            case left:
                Move(Convert.ToInt32(transform.position.x), Convert.ToInt32(originalPosition.x) + minX, -1, 0);
                break;
            case up:
                Move(Convert.ToInt32(transform.position.z), Convert.ToInt32(originalPosition.z) + maxZ, 0, 1);
                break;
            case down:
                Move(Convert.ToInt32(transform.position.z), Convert.ToInt32(originalPosition.z) + minZ, 0, -1);
                break;
            default:
                throw new ArgumentException("Invalid action value");
        }
    }

    private void Move(int value, int position, int x, int z)
    {
        if (value == position)
        {
            AddReward(-1f);
        }
        else
        {
            transform.position = transform.position + new Vector3(x, 0, z);
            int transX = Convert.ToInt32(transform.position.x);
            int transZ = Convert.ToInt32(transform.position.z);

            Vector3 targetPos = pictureLaneManager.targetGO.transform.position;
            int targetX = Convert.ToInt32(targetPos.x);
            int targetZ = Convert.ToInt32(targetPos.z);

            if(transX == targetX && targetZ == transZ)
            {
                AddReward(1);
                //pictureLaneManager.Relocate();
                Done();
            }
        }
    }

    public void FixedUpdate()
    {
        if(renderCamera != null)
        {
            renderCamera.Render();
        }

        RequestDecision();
    }

    public override void AgentReset()
    {
        transform.position = originalPosition;
        pictureLaneManager.Relocate();
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new float[] { 0 };
        }
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { 2 };
        }
        if (Input.GetKey(KeyCode.S))
        {
            return new float[] { 3 };
        }
        return new float[] { 4 };
    }
}
