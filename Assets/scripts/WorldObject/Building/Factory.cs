using UnityEngine;
using System.Collections;

public class Factory : Building {

    protected override void Start ()
    {
        base.Start();
        actions = new string[] { "Worker" };
    }

    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
        CreateUnit(actionToPerform);
    }

}
