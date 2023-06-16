using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float speed = 0.5f;
    public float sensitivity = 0.5f;

    public UniverseManager um;

    private void Awake()
    {
        um = FindObjectOfType<UniverseManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            um.SpawnPlanet(transform, transform.forward);
        }
    }
}
