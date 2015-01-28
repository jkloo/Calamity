using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class HUD : MonoBehaviour
{

    public Texture2D activeCursor;
    public Texture2D defaultCursor, selectCursor, moveCursor, attackCursor, harvestCursor;
    public Texture2D foodResourceIcon, woodResourceIcon;
    private CursorState cursorState;
    private Dictionary<CursorState, Texture2D> cursorMap;
    private Dictionary<ResourceType, int> resourceValues, resourceLimits;
    public Text foodResourceLabel, woodResourceLabel;

    void Start()
    {
        cursorMap = new Dictionary<CursorState, Texture2D>()
        {
            {CursorState.Default, defaultCursor},
            {CursorState.Select, selectCursor},
            {CursorState.Move, moveCursor},
            {CursorState.Attack, attackCursor},
            {CursorState.Harvest, harvestCursor}
        };
        resourceValues = new Dictionary< ResourceType, int >();
        resourceLimits = new Dictionary< ResourceType, int >();

        SetCursorState(CursorState.Default);
    }

    void Update ()
    {

    }

    public bool MouseInBounds()
    {
        // TODO: Actually implement when HUD layout is finalized
        return true;
    }

    public void SetCursorState(CursorState newState) {
        cursorState = newState;
        switch(newState) {
        case CursorState.Default:
            activeCursor = defaultCursor;
            break;
        case CursorState.Select:
            activeCursor = selectCursor;
            break;
        case CursorState.Attack:
            activeCursor = attackCursor;
            break;
        case CursorState.Harvest:
            activeCursor = harvestCursor;
            break;
        case CursorState.Move:
            activeCursor = moveCursor;
            break;
        default:
            break;
        }
        Cursor.SetCursor(cursorMap[cursorState], Vector2.zero, CursorMode.Auto);
    }

    public void SetResourceValues(Dictionary<ResourceType, int> resourceValues, Dictionary<ResourceType, int> resourceLimits)
    {
        this.resourceValues = resourceValues;
        this.resourceLimits = resourceLimits;
        foodResourceLabel.text = resourceValues[ResourceType.Food].ToString();
        woodResourceLabel.text = resourceValues[ResourceType.Wood].ToString();
    }
}
