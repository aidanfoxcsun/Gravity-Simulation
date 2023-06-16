using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UniverseManager : MonoBehaviour
{
    public float G = 0.00000067f;
    public long C = 299792458;
    public float velocity;
    public GravitationalObject[] gravObj;
    public GameObject gravitationalObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Collide(GravitationalObject o1, GravitationalObject o2)
    {
        if(o1.mass < o2.mass)
        {
            o2.AddMass(o1.mass);
            o2.AddSize(o1.size);
            Instantiate(o1.destructionParticles, o1.transform.position, Quaternion.LookRotation(o2.transform.position));
            o1.Destroy();
        }
        else
        {
            o1.AddMass(o2.mass);
            o1.AddSize(o2.size);
            Instantiate(o2.destructionParticles, o2.transform.position, Quaternion.LookRotation(o1.transform.position));
            o2.Destroy();
        }
    }

    public Vector3 Accelerate(float distance, GravitationalObject o1, GravitationalObject o2)
    {
        
        if (distance < ((o1.size + o2.size)/2.0f))
        {
            Collide(o1, o2);
        }
        
        float m1 = o1.mass;
        float m2 = o2.mass;
        float force = (G * m1 * m2) / (distance * distance);
        Vector3 normalizedDirectionalVector = (o2.transform.position - o1.transform.position).normalized;
        float acceleration = force / m1;
        
        return normalizedDirectionalVector*acceleration;

    }

    public float SchwarzschildRadius(GravitationalObject o)
    {
        float numerator = 2 * G * o.mass;
        return numerator / (C * C);
    }

    void GravCalculation()
    {
        gravObj = (GravitationalObject[])Object.FindObjectsOfType<GravitationalObject>();
        for (int i = 0; i < gravObj.Length-1; i++)
        {
            for(int j = 1; j < gravObj.Length; j++)
            {
                if (gravObj[i] == gravObj[j])
                {
                    continue;
                }
                float distance = Vector3.Distance(gravObj[i].transform.position, gravObj[j].transform.position);
                Debug.Log(distance + " " + gravObj[i] + ", " + gravObj[j]);
                Vector3 iAccel = Accelerate(distance, gravObj[i], gravObj[j]);
                Vector3 jAccel = Accelerate(distance, gravObj[j], gravObj[i]);

                gravObj[i].UpdateAcceleration(iAccel);
                gravObj[j].UpdateAcceleration(jAccel);
            }
        }
    }

    public void SpawnPlanet(Transform pos, Vector3 direction)
    {
        GameObject obj = Instantiate(gravitationalObject, pos);
        obj.transform.SetParent(null);
        GravitationalObject grav = obj.GetComponent<GravitationalObject>();
        grav.velocity = direction*velocity;
    }

    public void SetVelocity(float v)
    {
        velocity = v;
    }

    // Update is called once per frame
    void Update()
    {
        GravCalculation();
    }
}
