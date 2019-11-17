using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterClass : MonoBehaviour
{
    [Header("In Range Test Parameters")]
    public float range = 20f;
    public LayerMask mask;

    [Header("Ray test")]
    public RayPerception ray;

    public void Update()
    {
        Debug.Log(Hit());
    }

    private bool Hit()
    {
        float rayDistance = 20f;
        float[] rayAngles = { 90f };
        string[] detectableObjects = { "Tank" };

        List<float> result = ray.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f);

        return result[0] == 0 ? false : true;
    }

    private void ColCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
        //if(colliders != null)
        //{
        if (colliders.Length > 0)
        {
            Debug.Log("At least one!");
            Debug.Log(colliders[0].gameObject.name);
        }
        //}
    }
}
