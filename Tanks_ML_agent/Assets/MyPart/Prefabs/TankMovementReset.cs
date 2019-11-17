using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementReset : MonoBehaviour
{
    public GameObject tank;
    public GameObject finish;

    public float range = 30f;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        Vector2 tankPosition = Random.insideUnitCircle * range;
        Vector2 finishPosition = Random.insideUnitCircle * range;

        tank.transform.localPosition = new Vector3(tankPosition.x, 0.5f, tankPosition.y);
        finish.transform.localPosition = new Vector3(finishPosition.x, 0.5f, finishPosition.y);
    }
}
