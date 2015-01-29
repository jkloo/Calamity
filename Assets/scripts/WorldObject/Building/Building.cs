﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Building : WorldObject {

    public float maxBuildProgress;
    protected Queue< string > buildQueue;
    private float currentBuildProgress = 0.0f;
    private Vector3 spawnPoint;
    public GameObject spawnPointObject;

    protected override void Awake() {
        base.Awake();
        buildQueue = new Queue< string >();
        if(spawnPointObject)
        {
            spawnPoint = spawnPointObject.transform.position;
        }
        else
        {
            spawnPoint = transform.position;
        }
    }

    protected override void Start () {
        base.Start();
    }

    protected override void Update () {
        base.Update();
        ProcessBuildQueue();
    }

    protected override void OnGUI() {
        base.OnGUI();
    }

    protected void CreateUnit(string unitName)
    {
        buildQueue.Enqueue(unitName);
    }

    protected void ProcessBuildQueue()
    {

        if(buildQueue.Count > 0)
        {
            currentBuildProgress += Time.deltaTime * ResourceManager.BuildSpeed;
            if(currentBuildProgress > maxBuildProgress)
            {
                if(player)
                {
                    player.AddUnit(buildQueue.Dequeue(), spawnPoint, transform.rotation);
                }
                currentBuildProgress = 0.0f;
            }
        }
    }

    public string[] getBuildQueueValues()
    {
        string[] values = new string[buildQueue.Count];
        int pos=0;
        foreach(string unit in buildQueue)
            values[pos++] = unit;
        return values;
    }

    public float getBuildPercentage()
    {
        return currentBuildProgress / maxBuildProgress;
    }
}
