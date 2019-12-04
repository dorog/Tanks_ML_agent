using UnityEngine;

public class PictureLaneManager : MonoBehaviour
{
    public GameObject target;
    public int maxZ = 4;
    public int minZ = -4;

    public int maxX = 4;
    public int minX = -4;

    public GameObject targetGO = null;
    public GameObject agent;

    private Vector3 originalAgentPosition;

    public void Start()
    {
        Spawn();
        originalAgentPosition = agent.transform.position;
    }

    private void Spawn()
    {
        GameObject targetObj = Instantiate(target, transform);

        Vector3 position = GetPosition();

        targetObj.transform.localPosition = position;

        targetGO = targetObj;
    }

    public void Relocate()
    {
        Vector3 position = GetPosition();

        targetGO.transform.localPosition = position;

        agent.transform.position = originalAgentPosition;
    }

    private Vector3 GetPosition()
    {
        int x = Random.Range(minX, maxX);
        int z = Random.Range(minZ, maxZ);

        if(x == 0 && z == 0)
        {
            return GetPosition();
        }
        else
        {
            return new Vector3(x, 1, z);
        }
    }
}
