using UnityEngine;

public class AxisLines : MonoBehaviour
{
    public float lineLength = .5f;
    public float lineWidth = 0.025f;
    private LineRenderer xLineRenderer;
    private LineRenderer yLineRenderer;
    private LineRenderer zLineRenderer;

    void Start()
    {
        // xLineRenderer = CreateLine("X_Axis", Color.red, transform.right);
        // yLineRenderer = CreateLine("Y_Axis", Color.green, transform.up);
        zLineRenderer = CreateLine("Z_Axis", Color.blue, transform.forward);
    }

    void Update()
    {
    }

    LineRenderer CreateLine(string name, Color color, Vector3 direction)
    {
        GameObject lineObject = new GameObject(name);
        lineObject.transform.SetParent(transform);

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        
        UpdateLine(lineRenderer, direction);

        return lineRenderer;
    }

    void UpdateLine(LineRenderer lineRenderer, Vector3 direction)
    {
        Vector3 cubePosition = transform.position;
        lineRenderer.SetPosition(0, cubePosition);
        lineRenderer.SetPosition(1, cubePosition + direction * lineLength);
    }
}
