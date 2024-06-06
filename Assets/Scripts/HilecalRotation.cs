using System;
using UnityEngine;

public class HilecalRotation : MonoBehaviour
{
    public ChairRotation chairRotation;

    public float radius = 3f; // Radius of the helix
    public float pitch = 1f;  // Pitch of the helix
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void OnEnable() 
    {
        chairRotation.OnChairRotate += OnChairRotation;
    }

    void OnDisable() 
    {
        chairRotation.OnChairRotate -= OnChairRotation;
    }

    private void OnChairRotation(Vector3 rotation, float yRotation)
    {
        Debug.Log("OnChairRotation: " + rotation + " yRotation: " + yRotation);

        float angleInRadians = yRotation * Mathf.Deg2Rad;
        
        // Calculate the new position on the helical ramp
        float x = radius * Mathf.Cos(angleInRadians);
        float y = radius * Mathf.Sin(angleInRadians);
        float z = (pitch / (2 * Mathf.PI)) * angleInRadians;

        // Update the object's position
        transform.position = new Vector3(x, y, z);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
