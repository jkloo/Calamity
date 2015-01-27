using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingConstruction : MonoBehaviour {

    public float buildTime = 2.0f;
    private float elapsedTime = 0.0f;
    private float completionPercent = 0.0f;
    private bool startBuilding = false;
    private GameObject stub;
    private GameObject complete;


    // Use this for initialization
    void Awake ()
    {
        stub = gameObject.transform.FindChild("stub").gameObject;
        complete = gameObject.transform.FindChild("complete").gameObject;
        stub.SetActive(true);
    }

    // Update is called once per frame
    void Update ()
    {
        if(startBuilding)
        {
            elapsedTime = Mathf.MoveTowards(elapsedTime, buildTime, Time.deltaTime);
            completionPercent = elapsedTime / buildTime;
            if (elapsedTime >= buildTime)
            {
                FinishBuilding();
            }
        }
    }

    public void StartBuilding()
    {
        startBuilding = true;
    }

    public void FinishBuilding()
    {
        stub.SetActive(false);
        complete.SetActive(true);
    }
}
