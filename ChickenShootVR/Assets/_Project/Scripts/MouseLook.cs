using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Camera cam;
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        cam.transform.eulerAngles = (Vector2)rotation * speed;
    }
}
