using UnityEngine;
using System.Collections;

public class EdgePan : MonoBehaviour {

    public float boundary = 100f;
    public float panSpeed = 10f;


    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        float newZ = transform.position.z;

        if (Input.mousePosition.x > Screen.width - boundary)
        {
            float scale = (Input.mousePosition.x + boundary - Screen.width) / boundary;
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            newX += scale * panSpeed * Time.deltaTime; // move on +X axis
            newZ -= scale * panSpeed * Time.deltaTime; // move on +X axis
        }

        if (Input.mousePosition.x < 0 + boundary)
        {
            float scale = -(Input.mousePosition.x - boundary) / boundary;
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            newX -= scale * panSpeed * Time.deltaTime; // move on -X axis
            newZ += scale * panSpeed * Time.deltaTime; // move on -X axis
        }

        if (Input.mousePosition.y > Screen.height - boundary)
        {
            float scale = (Input.mousePosition.y + boundary - Screen.height) / boundary;
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            newX += scale * panSpeed * Time.deltaTime; // move on +Z axis
            newZ += scale * panSpeed * Time.deltaTime; // move on +Z axis
        }

        if (Input.mousePosition.y < 0 + boundary)
        {
            float scale = -(Input.mousePosition.y - boundary) / boundary;
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            newX -= scale * panSpeed * Time.deltaTime; // move on +Z axis
            newZ -= scale * panSpeed * Time.deltaTime; // move on -Z axis
        }

        transform.position = new Vector3(newX, newY, newZ);
    }
}
