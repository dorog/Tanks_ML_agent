using System;
using UnityEngine;

public class PictureLaneController : MonoBehaviour
{
    public int maxZ = 4;
    public int minZ = -4;
    public int maxX = 4;
    public int minX = -4;

    private Vector3 originalPosition;
    public PictureLaneManager pictureLaneManager;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AgentAction(1f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AgentAction(3f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AgentAction(2f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AgentAction(4f);
        }
    }

    public void AgentAction(float vectorAction)
    {
        Debug.Log("-0.01f");
        var action = Mathf.FloorToInt(vectorAction);

        switch (action)
        {
            case 0:
                // do nothing
                break;
            case 1:
                Move(Convert.ToInt32(transform.position.x), Convert.ToInt32(originalPosition.x) + maxX, 1, 0);
                break;
            case 2:
                Move(Convert.ToInt32(transform.position.x), Convert.ToInt32(originalPosition.x) + minX, -1, 0);
                break;
            case 3:
                Move(Convert.ToInt32(transform.position.z), Convert.ToInt32(originalPosition.z) + maxZ, 0, 1);
                break;
            case 4:
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
            Debug.Log("-1f");
        }
        else
        {
            transform.position = transform.position + new Vector3(x, 0, z);
            int transX = Convert.ToInt32(transform.position.x);
            int transZ = Convert.ToInt32(transform.position.z);

            Vector3 targetPos = pictureLaneManager.targetGO.transform.position;
            int targetX = Convert.ToInt32(targetPos.x);
            int targetZ = Convert.ToInt32(targetPos.z);

            if (transX == targetX && targetZ == transZ)
            {
                Debug.Log(1);
                pictureLaneManager.Relocate();
            }
        }
    }
}
