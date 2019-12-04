using MLAgents;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBattleArenaManager : MonoBehaviour
{
    public Material[] Colors;

    public Transform[] Places;

    protected List<GameObject> tanks = new List<GameObject>();

    private void Start()
    {
        StartNewRound();
    }

    protected virtual void StartNewRound()
    {
        tanks.Clear();

        int place = UnityEngine.Random.Range(0, Places.Length);
        CreateTank(Places[place], Colors[place]);
    }

    protected void CreateTank(Transform transform, Material material)
    {
        GameObject tank = InstantiateTank(transform, material);
        tanks.Add(tank);
    }

    protected abstract GameObject InstantiateTank(Transform transform, Material material);

    public List<GameObject> GetTanks()
    {
        return tanks;
    }

    public virtual void Done(GameObject gameObject)
    {
        RemoveTank(gameObject);
        if (tanks.Count == 1)
        {
            MakeTankDone(tanks[0]);
            StartNewRound();
        }
        else if (tanks.Count == 0)
        {
            StartNewRound();
        }
    }

    public void RemoveTank(GameObject gameObject)
    {
        tanks.Remove(gameObject);
    }

    protected void MakeTankDone(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        Agent[] agents = gameObject.GetComponents<Agent>();
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].Done();
        }

        Agent[] childrenAgents = gameObject.GetComponentsInChildren<Agent>();
        for (int i = 0; i < childrenAgents.Length; i++)
        {
            childrenAgents[i].Done();
        }
    }

    public List<float> GetDataForShoot(GameObject tank, GameObject target)
    {
        List<float> data = new List<float>();

        try
        {
            Vector3 vectorToTank = target.transform.position - tank.transform.position;

            float distance = vectorToTank.magnitude;
            data.Add(distance);

            float angle = Vector3.SignedAngle(vectorToTank, tank.transform.forward, Vector3.up);
            data.Add(angle);

            TankMovement targetTankMovement = target.GetComponent<TankMovement>();
            Vector3 targetVelocity = targetTankMovement.GetVelocity();
            data.Add(targetVelocity.x);
            data.Add(targetVelocity.z);

            //Movement speed change it to ask
            data.Add(12f);
        }
        catch (Exception)
        {
            data.Add(0);
            data.Add(0);
            data.Add(0);
            data.Add(0);
            data.Add(0);
        }

        return data;
    }

    public List<float> GetDataForMove(GameObject tank, GameObject target)
    {
        List<float> data = new List<float>();

        try
        {
            data.Add(GetDistance(tank, target));

            data.Add(GetAngle(tank, target));
        }
        catch (Exception)
        {
            data.Add(0);
            data.Add(0);
        }

        return data;
    }

    public float GetAngle(GameObject tank, GameObject target)
    {
        try
        {
            Vector3 vectorToTank = target.transform.position - tank.transform.position;

            float angle = Vector3.SignedAngle(vectorToTank, tank.transform.forward, Vector3.up);

            return angle;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public float GetDistance(GameObject tank, GameObject target)
    {
        try
        {
            Vector3 vectorToTank = target.transform.position - tank.transform.position;

            float distance = vectorToTank.magnitude;

            return distance;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public abstract void TargetTankDied();
}
