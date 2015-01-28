using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Player : MonoBehaviour
{

    public HUD hud;
    public string username = "Bot";
    public bool human = false;
    public WorldObject SelectedObject { get; set; }

    public int startFood, startFoodLimit;
    public int startWood, startWoodLimit;
    private Dictionary< ResourceType, int > resources, resourceLimits;

    void Awake()
    {
        resources = InitResourceList();
        resourceLimits = InitResourceList();
    }

    void Start ()
    {
        hud = GetComponent<HUD>();
        AddStartResourceLimits();
        AddStartResources();
    }

    void Update ()
    {
        if(human)
        {
            hud.SetResourceValues(resources, resourceLimits);
        }
    }

    private Dictionary< ResourceType, int > InitResourceList()
    {
        Dictionary< ResourceType, int > list = new Dictionary< ResourceType, int >();
        list.Add(ResourceType.Food, 0);
        list.Add(ResourceType.Wood, 0);
        return list;
    }

    private void AddStartResourceLimits()
    {
        IncrementResourceLimit(ResourceType.Food, startFoodLimit);
        IncrementResourceLimit(ResourceType.Wood, startWoodLimit);
    }

    private void AddStartResources()
    {
        AddResource(ResourceType.Food, startFood);
        AddResource(ResourceType.Wood, startWood);
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }

    public void IncrementResourceLimit(ResourceType type, int amount)
    {
        resourceLimits[type] += amount;
    }
}
