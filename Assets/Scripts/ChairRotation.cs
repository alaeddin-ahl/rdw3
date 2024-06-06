using UnityEngine;

public class ChairRotation : MonoBehaviour
{
    public delegate void RotationEventHandler(Vector3 rotation, float yRotation);
    public event RotationEventHandler OnChairRotate;

    public float rotationSpeed = 100.0f;

    public float maxRotation = 90.0f;
    public float minRotation = -90.0f;

    public float yRotation = 0.0f;

    static float NormalizeAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }

    void Update()
    {
        bool isRotating = false;
        Vector3 rotationDelta = Vector3.zero;

        yRotation = NormalizeAngle(transform.rotation.eulerAngles.y);

        // Rotate the chair around the y-axis
        if (Input.GetKey(KeyCode.LeftArrow) 
            && yRotation > minRotation)
        {
            rotationDelta = Vector3.up * -rotationSpeed * Time.deltaTime;
            // transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            isRotating = true;
        }

        if (Input.GetKey(KeyCode.RightArrow) 
            && yRotation < maxRotation)
        {
            rotationDelta = Vector3.up * rotationSpeed * Time.deltaTime;
            // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            isRotating = true;
        }

        if (isRotating)
        {
            transform.Rotate(rotationDelta);
            this.OnChairRotate?.Invoke(rotationDelta, yRotation);
        }

    }
}