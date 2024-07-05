using Unity.Mathematics;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public Transform objectA; 
    public Transform objectB; 

    public Vector3 direction;
    public Vector3 normalizedDirection;
    public Quaternion rotationToDirection;

    public Quaternion relativeRotation;

    public float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionA = objectA.position;
        Vector3 positionB = objectB.position;

        direction = positionB - positionA;
        normalizedDirection = direction.normalized;

        direction.y = 0; // Ignore height for rotation to face horizontally to the center
        normalizedDirection.y = 0; // Ignore height for rotation to face horizontally to the center

        if (direction != Vector3.zero){
            rotationToDirection = Quaternion.LookRotation(normalizedDirection);

            transform.rotation = rotationToDirection;

            Quaternion fromRotation = objectB.rotation;
            Quaternion toRotation = rotationToDirection;
            relativeRotation = Quaternion.Inverse(fromRotation) * toRotation;


            // Extract the angle and axis from the relative rotation
            relativeRotation.ToAngleAxis(out float angle, out Vector3 axis);
            this.angle = angle;
        }
        

        if (Input.GetKeyDown(KeyCode.F)){
            float f = objectA.GetComponent<ChairRotation>().yRotationNormalized;

            Debug.Log("F - f: " + f);

            objectB.transform.RotateAround(objectA.transform.position, Vector3.up, f);
        }
    }
}
