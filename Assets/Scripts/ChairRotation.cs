using UnityEngine;

public class ChairRotation : MonoBehaviour
{
    public delegate void RotationEventHandler(
        Vector3 rotation, 
        float yRotationNormalized);

    public event RotationEventHandler OnChairRotate;

    public float rotationSpeed = 100.0f;
    public float maxRotation = 90.0f;
    public float minRotation = -90.0f;

    public Transform controllerTransform;

    public float yController = 0.0f;
    public float yControllerLast = 0.0f;

    private float yRotation = 0.0f;
    private float yRotationNormalized = 0.0f;

    static float NormalizeAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }

    bool IsValidRotation (float y) {
        return y > minRotation
            && y < maxRotation;
    }

    void Update()
    {
        bool isRotating = false;
        Vector3 rotationDelta = Vector3.zero;

        yRotation = transform.rotation.eulerAngles.y;
        yRotationNormalized = NormalizeAngle(transform.rotation.eulerAngles.y);

        // Rotate the chair around the y-axis
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationDelta = Vector3.up * -rotationSpeed * Time.deltaTime;
            // transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            isRotating = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationDelta = Vector3.up * rotationSpeed * Time.deltaTime;
            // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            isRotating = true;
        }

        if (isRotating && IsValidRotation(yRotationNormalized))  
        {
            transform.Rotate(rotationDelta);
            this.OnChairRotate?.Invoke(rotationDelta, yRotationNormalized);
        }

        if (controllerTransform != null)
        {
            yController = controllerTransform.rotation.eulerAngles.y;
            yController = NormalizeAngle(yController);

            if (yControllerLast != yController 
                && IsValidRotation(yController))
            {
                yControllerLast = yController;

                this.transform.eulerAngles = new Vector3(
                    this.transform.eulerAngles.x,
                    yController,
                    this.transform.eulerAngles.z);
                    
                this.OnChairRotate?.Invoke(Vector3.zero, yController);
            }
        }
    }
}