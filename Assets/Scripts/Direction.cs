using Unity.Mathematics;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public Transform objectA; 
    public Transform objectB; 

    public Transform objectC;

    public Vector3 direction;
    public Vector3 normalizedDirection;
    public Quaternion rotationToDirection;
    public Quaternion relativeRotation;

    public bool isReverseDirection = false;

    public bool is90Deg = false;

    public float ry;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 positionA = objectA.position;
        Vector3 positionB = objectB.position;

        direction = positionB - positionA;
        
        if (isReverseDirection) {
            direction = positionA - positionB;
        }

        if (is90Deg) {
            direction = new Vector3(direction.z, direction.y, -direction.x);
        }
        

        normalizedDirection = direction.normalized;

        direction.y = 0; 
        normalizedDirection.y = 0; 

        if (direction != Vector3.zero){
            rotationToDirection = Quaternion.LookRotation(normalizedDirection);

            transform.rotation = rotationToDirection;

            Quaternion fromRotation = transform.rotation;
            Quaternion toRotation = objectA.rotation;
            relativeRotation = Quaternion.Inverse(fromRotation) * toRotation;


            // Extract the angle and axis from the relative rotation
            // relativeRotation.ToAngleAxis(out float angle, out Vector3 axis);
            // this.angle = angle;
            // this.axis = axis;

            this.ry = relativeRotation.eulerAngles.y;
        }
        

        if (Input.GetKeyDown(KeyCode.F)){
            float f = this.ry;
            Debug.Log("F - f: " + f);

            objectC.transform.RotateAround(objectA.transform.position, Vector3.up, f);
        }
    }
}
