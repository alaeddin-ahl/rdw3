using System;
using NUnit.Framework;
using UnityEngine;

public class HilecalRotation : MonoBehaviour
{
    public ChairRotation chairRotation;

    // Given parameters
    public float radius = 2.0f;
    public float height = 4.0f;
    public float width = 4.0f;
    public float numberOfTurns = 1.0f;

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
    // Function to map an input angle to a position on the helical stairs
    public Vector3 MapAngleToPosition(float angle)
    {
        // Clamp the angle to ensure it is within -90 to 90 degrees
        // angle = Mathf.Clamp(angle, -90, 90);

        // Map the angle to the parameter t
        float t = Mathf.Lerp(0, 2 * Mathf.PI * numberOfTurns, Mathf.InverseLerp(-90, 90, angle));

        // Calculate the position
        float x = radius * Mathf.Cos(t); 
        float y = radius * Mathf.Sin(t); 
        float z = (height / (2 * Mathf.PI * numberOfTurns)) * t;

        return new Vector3(x, z, y);
    }

    public float   previousYRotation = 0;


    private void OnChairRotation(Vector3 rotation, float yRotationNormalized, float yRotation)
    {
        Debug.Log("OnChairRotation: " + rotation + " yRotationNormalized: " + yRotationNormalized);

        transform.position = MapAngleToPosition(yRotationNormalized);

    }

    public bool IsRotating = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            IsRotating = !IsRotating;
        }

        if (IsRotating)
        {   
            float f = 20 * Time.deltaTime;
            Debug.Log("f: " + f);   
            transform.RotateAround(chairRotation.transform.position, Vector3.up, f);
        }
    }
}
