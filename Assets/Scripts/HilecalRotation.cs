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
        float x = radius * Mathf.Cos(t); // Inverted t for opposite direction
        float y = radius * Mathf.Sin(t); // Inverted t for opposite direction
        float z = (height / (2 * Mathf.PI * numberOfTurns)) * t;

        return new Vector3(x, z, y);
    }

    public float   previousYRotation = 0;


    private void OnChairRotation(Vector3 rotation, float yRotationNormalized, float yRotation)
    {
        Debug.Log("OnChairRotation: " + rotation + " yRotationNormalized: " + yRotationNormalized);

        transform.position = MapAngleToPosition(yRotationNormalized);

        Vector3 directionToCenter = new Vector3(
                transform.position.x,
                0,
                transform.position.z);
        Quaternion endRotation = Quaternion.LookRotation(-directionToCenter);

        // Calculate direction to the chair's position
        Vector3 directionToChair = chairRotation.transform.position - transform.position;
        directionToChair.x = 0;
        directionToChair.z = 0; // Ignore height for horizontal rotation

        // Set rotation to face the chair
        // Quaternion targetRotation = Quaternion.LookRotation(directionToChair);
        // transform.rotation = targetRotation;

        // Calculate the difference in y rotation
        // float currentYRotation = yRotationNormalized;
        // float rotationDifference = currentYRotation - previousYRotation;

        // Debug.Log("rotationDifference:"+ rotationDifference);

        // previousYRotation = currentYRotation;

        // transform.RotateAround(chairRotation.transform.position, Vector3.up, rotationDifference);

        // Calculate the relative rotation needed to go from startRotation to endRotation
        Debug.Log("from: " + transform.rotation + " to: " + endRotation);    

        Quaternion relativeRotation = Quaternion.Inverse(transform.rotation) * endRotation;

        // Extract the angle and axis from the relative rotation
        relativeRotation.ToAngleAxis(out float angle, out Vector3 axis);

        Debug.Log("angle: " + angle + " axis: " + axis);
        // transform.RotateAround(chairRotation.transform.position, axis, angle);
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
