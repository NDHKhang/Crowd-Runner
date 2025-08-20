using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DoorType { Addition, Subtraction, Multiplication }
public class Doors : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private Collider doorCollider;

    [Header("Door Settings")]
    [SerializeField] private DoorType leftDoorType;
    [SerializeField] private int leftDoorAmount;
    [SerializeField] private DoorType rightDoorType;
    [SerializeField] private int rightDoorAmount;

    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        // Left door configuration
        switch (leftDoorType)
        {
            case DoorType.Addition:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorAmount;
                break;

            case DoorType.Subtraction:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorAmount;
                break;

            case DoorType.Multiplication:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "x" + leftDoorAmount;
                break;
        }

        // Right door configuration
        switch (rightDoorType)
        {
            case DoorType.Addition:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorAmount;
                break;

            case DoorType.Subtraction:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorAmount;
                break;

            case DoorType.Multiplication:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "x" + rightDoorAmount;
                break;
        }
    }

    public int GetDoorAmount(float xPos)
    {
        if (xPos < 0)
            return leftDoorAmount;
        else
            return rightDoorAmount;
    }    

    public DoorType GetDoorType(float xPos)
    {
        if (xPos < 0)
            return leftDoorType;
        else
            return rightDoorType;
    }

    public void Disable()
    {
        doorCollider.enabled = false;
    }
}
