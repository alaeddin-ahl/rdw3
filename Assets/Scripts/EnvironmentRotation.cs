using System;
using UnityEngine;

public class EnvironmentRotation : MonoBehaviour
{
    public ChairRotation chairRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (chairRotation == null)
        {
            Debug.LogError("ChairRotation not found");
        }
    }
    
    void OnEnable() 
    {
        chairRotation.OnChairRotate += OnChairRotation;
    }

    void OnDisable() 
    {
        chairRotation.OnChairRotate -= OnChairRotation;
    }

    private void OnChairRotation(Vector3 rotation, float yRotationNormalized)
    {
        Debug.Log("OnChairRotation: " + rotation );

        transform.Rotate(-rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        // }
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        // }

        
    }
}
