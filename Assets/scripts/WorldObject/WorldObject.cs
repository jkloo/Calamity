﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RTS;

public class WorldObject : MonoBehaviour
{

    public string objectName;
    public Sprite buildImage;
    public int foodCost;
    public int woodCost;
    public int foodSellValue;
    public int woodSellValue;
    public int hitPoints;
    public int maxHitPoints;

    protected Player player;
    protected string[] actions = {};
    protected bool currentlySelected = false;
    protected Bounds selectionBounds;

    protected virtual void Awake()
    {
        selectionBounds = ResourceManager.InvalidBounds;
        CalculateBounds();
    }

    protected virtual void Start ()
    {
        player = transform.root.GetComponentInChildren< Player >();
    }

    protected virtual void Update ()
    {

    }

    public virtual void SetSelection(bool selected)
    {
        currentlySelected = selected;
    }

    public string[] GetActions()
    {
        return actions;
    }

    public virtual void PerformAction(string actionToPerform)
    {
        //it is up to children with specific actions to determine what to do with each of those actions
    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        //only handle input if currently selected
        if(currentlySelected && hitObject && hitObject.name != "Ground")
        {
            WorldObject worldObject = hitObject.transform.parent.GetComponent< WorldObject >();
            //clicked on another selectable object
            if(worldObject)
                ChangeSelection(worldObject, controller);
        }
    }

    private void ChangeSelection(WorldObject worldObject, Player controller)
    {
        //this should be called by the following line, but there is an outside chance it will not
        SetSelection(false);
        if(controller.SelectedObject)
            controller.SelectedObject.SetSelection(false);
        controller.SelectedObject = worldObject;
        worldObject.SetSelection(true);
    }

    public virtual void SetHoverState(GameObject hoverObject)
    {
        //only handle input if owned by a human player and currently selected
        if(player && player.human && currentlySelected)
        {
            if(hoverObject.name != "Ground")
                player.hud.SetCursorState(CursorState.Select);
        }
    }

    public void CalculateBounds()
    {
       selectionBounds = new Bounds(transform.position, Vector3.zero);
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            selectionBounds.Encapsulate(r.bounds);
        }
    }

    public bool IsOwnedBy(Player owner)
    {
        if(player && player.Equals(owner))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Sell()
    {
        if(player)
        {
            player.AddResource(ResourceType.Food, foodSellValue);
            player.AddResource(ResourceType.Wood, woodSellValue);
        }

        if(currentlySelected)
        {
            SetSelection(false);
        }
        Destroy(this.gameObject);
    }
}
