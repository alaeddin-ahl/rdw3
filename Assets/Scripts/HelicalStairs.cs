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