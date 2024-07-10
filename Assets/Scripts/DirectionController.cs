using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Direction))]
public class DirectionController : MonoBehaviour
{

    public enum DirectionType
    {
        Upward,
        Downward,
        Inward,
        Outward
    }

    public DirectionType currentDirection = DirectionType.Upward;
    public TextMeshProUGUI directionText;
    public Direction direction;

    void Start()
    {
        direction = GetComponent<Direction>();
        HandleDirection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change to your desired button
        {
            ToggleDirection();
        }

    }

    public void ToggleDirection()
    {
        currentDirection = (DirectionType)(((int)currentDirection + 1) % 4);
        Debug.Log("Current Direction: " + currentDirection);
                    
        HandleDirection();
    }

    void HandleDirection()
    {
        switch (currentDirection)
        {
            case DirectionType.Upward:
                // Handle upward direction
                direction.is90Deg = true;
                direction.isReverseDirection = true;
                break;
            case DirectionType.Downward:
                // Handle downward direction
                direction.is90Deg = true;
                direction.isReverseDirection = false;

                break;
            case DirectionType.Inward:
                // Handle inward direction
                direction.is90Deg = false;
                direction.isReverseDirection = false;
                break;
            case DirectionType.Outward:
                // Handle outward direction
                direction.is90Deg = false;
                direction.isReverseDirection = true;
                break;
        }

        directionText.text = currentDirection.ToString();
    }
}