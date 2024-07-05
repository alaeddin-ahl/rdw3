using Unity.Mathematics;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public Transform objectA; 
    public Transform objectB; 

    public Vector3 direction;
    public Vector3 normalizedDirection;
    public Quaternion rotationToDirection;

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
            rotationToDirection = Quaternion.LookRotation(direction);

            transform.rotation = rotationToDirection;
        }
        
    }
}
