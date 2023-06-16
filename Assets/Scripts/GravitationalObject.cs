using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalObject : MonoBehaviour
{
    UniverseManager universeManager;
    public float mass;
    public float size;
    public Vector3 velocity;
    public Vector3 newScale;

    public ParticleSystem destructionParticles;

    public void Awake()
    {
        universeManager = FindObjectOfType<UniverseManager>();
    }

    public float SchwarzschildRadius()
    {
        float numerator = 2 * universeManager.G * mass;
        return numerator / (universeManager.C * universeManager.C);
    }

    public void Start()
    {
        size = transform.localScale.x;
        UpdateSize();
    }

    public void UpdateAcceleration(Vector3 acceleration)
    {
        velocity += acceleration;
        transform.position += velocity ;
    }

    private void FixedUpdate()
    {
        if (size != transform.localScale.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, 2.0f * Time.deltaTime);
        }
        
    }

    private void UpdateSize()
    {
        newScale = new Vector3(size, size, size);

    }

    public void AddSize(float otherSize)
    {
        size += otherSize;
        UpdateSize();
    }

    public void AddMass(float otherMass)
    {
        mass += otherMass;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
