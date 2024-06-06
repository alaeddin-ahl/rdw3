using UnityEngine;

public class HelicalStairs : MonoBehaviour
{
    // Given parameters
    public float radius = 2.0f;
    public float height = 5.0f;
    public float width = 4.0f;
    public float numberOfTurns = 1.25f;
    public int numberOfSteps = 13;
    public GameObject cubePrefab; // Assign a cube prefab in the inspector

    void Start()
    {
        GenerateHelicalStairs();
    }

    // Function to map an input angle to a position on the helical stairs
    public Vector3 MapAngleToPosition(float angle)
    {
        // Clamp the angle to ensure it is within -90 to 90 degrees
        angle = Mathf.Clamp(angle, -90, 90);

        // Map the angle to the parameter t
        float t = Mathf.Lerp(0, 2 * Mathf.PI * numberOfTurns, Mathf.InverseLerp(-90, 90, angle));

        // Calculate the position
        float x = radius * Mathf.Cos(-t); // Inverted t for opposite direction
        float y = radius * Mathf.Sin(-t); // Inverted t for opposite direction
        float z = (height / (2 * Mathf.PI * numberOfTurns)) * t;

        return new Vector3(x, z, y);
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
            Vector3 position = new Vector3(x, z, y);
            Instantiate(cubePrefab, position, Quaternion.identity, parent);
        }
    }
}