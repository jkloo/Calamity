using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingConstruction : MonoBehaviour {

    public float buildTime = 2.0f;
    public bool complete = false;
    private float elapsedTime = 0.0f;
    private float completionPercent = 0.0f;

    private bool startBuilding = false;

    private GameObject stubBuilding;
    private GameObject completeBuilding;


    // Use this for initialization
    void Awake ()
    {
        stubBuilding = gameObject.transform.FindChild("stub").gameObject;
        completeBuilding = gameObject.transform.FindChild("complete").gameObject;
        stubBuilding.SetActive(!complete);
        completeBuilding.SetActive(complete);
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
        startBuilding = !complete;
    }

    public void FinishBuilding()
    {
        stubBuilding.SetActive(false);
        completeBuilding.SetActive(true);
        complete = true;
    }
}
