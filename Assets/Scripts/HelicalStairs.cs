using Unity.VisualScripting;
using UnityEngine;

public class HelicalStairs : MonoBehaviour
{
    // Given parameters
    public float radius = 2.0f;
    public float height = 4.0f;
    public float width = 4.0f;
    public float numberOfTurns = 1f;
    public int numberOfSteps = 13;
    public GameObject cubePrefab; // Assign a cube prefab in the inspector
    
    public Transform offsetPosition;

    void Start()
    {
        GenerateHelicalStairs();
    }
    
    void GenerateHelicalStairs()
    {
        var parent = this.transform;

        for (int i = 0; i < numberOfSteps; i++)
        {
            float t = (2 * Mathf.PI * numberOfTurns / numberOfSteps) * i;
            float x = radius * Mathf.Cos(-t);
            float y = radius * Mathf.Sin(-t);
            float z = (height / numberOfSteps) * i;

            // Create a new cube at the calculated position
            Vector3 localPosition = new Vector3(x, z, y);
            Vector3 position = parent.TransformPoint(localPosition + offsetPosition.position);

            Debug.Log("position: " + position);

            Vector3 directionToCenter = new Vector3(
                position.x,
                position.y,
                position.z);
            directionToCenter.y = 0; // Ignore height for rotation to face horizontally to the center
            
            Quaternion localRotation = Quaternion.LookRotation(directionToCenter);

            // Quaternion rotation = parent.rotation * localRotation;
            Quaternion rotation = localRotation;
            

            GameObject a = Instantiate(cubePrefab, position, rotation, parent);

            a.GetComponent<CubeController>().id = i;
        }
    }

}