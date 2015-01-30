using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class HUD : MonoBehaviour
{

    public Texture2D activeCursor;
    public Texture2D defaultCursor, selectCursor, moveCursor, attackCursor, harvestCursor, rallyPointCursor;
    public Texture2D rallyPointImage;
    public Texture2D foodResourceIcon, woodResourceIcon;
    public Text foodResourceLabel, woodResourceLabel;
    private Player player;

    private CursorState activeCursorState;
    private Dictionary<CursorState, Texture2D> cursorMap;
    private Dictionary<ResourceType, int> resourceValues, resourceLimits;

    public Image[] actionButtons;
    public Image sellButton;
    public Image rallyButton;

    public Slider buildProgressSlider;
    public Image[] buildQueueImages;

    public Slider healthSlider;
    private WorldObject lastSelection;
    private float sliderValue;

    private CursorState previousCursorState;

    void Start()
    {
        player = gameObject.GetComponent< Player >();

        cursorMap = new Dictionary<CursorState, Texture2D>()
        {
            {CursorState.Default, defaultCursor},
            {CursorState.Select, selectCursor},
            {CursorState.Move, moveCursor},
            {CursorState.Attack, attackCursor},
            {CursorState.Harvest, harvestCursor},
            {CursorState.RallyPoint, rallyPointCursor}
        };
        resourceValues = new Dictionary< ResourceType, int >();
        resourceLimits = new Dictionary< ResourceType, int >();

        SetCursorState(CursorState.Default);
    }

    void Update ()
    {
        ClearBuildQueue();
        if(player && player.SelectedObject && player.SelectedObject.IsOwnedBy(player))
        {
            //reset slider value if the selected object has changed
            if(lastSelection && lastSelection != player.SelectedObject)
            {
                sliderValue = 0.0f;
            }
            DrawActions(player.SelectedObject.GetActions());
            //store the current selection
            lastSelection = player.SelectedObject;
            Building selectedBuilding = lastSelection.GetComponent<Building>();
            if(selectedBuilding)
            {
                DrawBuildQueue(selectedBuilding.getBuildQueueValues(), selectedBuilding.getBuildPercentage());
                DrawStandardBuildingOptions(selectedBuilding);
            }
        }
        DrawHealthBar();
    }

    public bool MouseInBounds()
    {
        // TODO: Actually implement when HUD layout is finalized
        return true;
    }

    public void SetCursorState(CursorState newState)
    {
        Debug.Log("Cursor state: " + newState);
        if(activeCursorState != newState)
            previousCursorState = activeCursorState;
        activeCursorState = newState;
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
        case CursorState.RallyPoint:
            activeCursor = rallyPointCursor;
            break;
        default:
            break;
        }
        Cursor.SetCursor(cursorMap[activeCursorState], Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorStateRallyPoint()
    {
        SetCursorState(CursorState.RallyPoint);
    }

    public void SetResourceValues(Dictionary<ResourceType, int> resourceValues, Dictionary<ResourceType, int> resourceLimits)
    {
        this.resourceValues = resourceValues;
        this.resourceLimits = resourceLimits;
        foodResourceLabel.text = resourceValues[ResourceType.Food].ToString();
        woodResourceLabel.text = resourceValues[ResourceType.Wood].ToString();
    }

    private void DrawActions(string[] actions)
    {
        for(int i=0; i < actionButtons.Length; i++)
        {
            actionButtons[i].enabled = false;
        }
        for(int i = 0; i < actions.Length && i < actionButtons.Length; i++)
        {
            if(actions[i] == "")
            {
                continue;
            }
            Sprite actionIcon = ResourceManager.GetBuildImage(actions[i]);
            actionButtons[i].enabled = true;
            actionButtons[i].sprite = actionIcon;
        }
    }

    private void DrawBuildQueue(string[] buildQueue, float buildPercentage)
    {
        if(buildQueue.Length > 0)
        {
            buildProgressSlider.transform.parent.gameObject.SetActive(true);
            buildProgressSlider.value = buildPercentage;
            for(int i=0; i < buildQueue.Length && i < buildQueueImages.Length; i++)
            {
                buildQueueImages[i].enabled = true;
                buildQueueImages[i].sprite = ResourceManager.GetBuildImage(buildQueue[i]);
            }
        }
    }

    private void DrawHealthBar()
    {
        if(lastSelection)
        {
            healthSlider.transform.parent.gameObject.SetActive(true);
            healthSlider.value = (float)lastSelection.hitPoints / (float)lastSelection.maxHitPoints;
        }
        else
        {
            healthSlider.transform.parent.gameObject.SetActive(false);
        }
    }

    private void ClearBuildQueue()
    {
		buildProgressSlider.transform.parent.gameObject.SetActive (false);
		for(int i=0; i < buildQueueImages.Length; i++)
        {
            buildQueueImages[i].enabled = false;
        }
    }

    public void PerformAction(int index)
    {
        player.SelectedObject.PerformAction(player.SelectedObject.GetActions()[index]);
    }

    private void DrawStandardBuildingOptions(Building building)
    {
        rallyButton.enabled = true;
        sellButton.enabled = true;
    }

    public CursorState GetPreviousCursorState()
    {
        return previousCursorState;
    }

    public CursorState GetCursorState()
    {
        return activeCursorState;
    }

    public void SellSelectedObject()
    {
        player.SelectedObject.Sell();
    }
}
