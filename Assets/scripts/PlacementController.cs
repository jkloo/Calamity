using UnityEngine;
using System.Collections;

public class PlacementController : MonoBehaviour
{
    private GameObject obj;
    public GameObject grid;
    public float gridSize = 2f;

    // Use this for initialization
    void Start ()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        obj = Instantiate(Resources.Load("short_building")) as GameObject;
        Vector3 pos = GetMousePositionOnPlane();
        obj.transform.position = SnapToGrid(pos);
    }

    void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");
        Vector3 pos = GetMousePositionOnPlane();
        obj.transform.position = SnapToGrid(pos);
    }

    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        Vector3 pos = GetMousePositionOnPlane();
        obj.transform.position = SnapToGrid(pos);
        obj = null;
    }

    Vector3 GetMousePositionOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, ~LayerMask.NameToLayer("World"));
        Vector3 pos;
        if(hits.Length > 0)
            pos = hits[0].point;
        else
            pos = Vector3.zero;

        return pos;
    }

    Vector3 SnapToGrid(Vector3 Position)
    {
        if (!grid)
            return Position;
        // Debug.Log(Position);

        //    get grid size from the texture tiling
        Vector3 GridSize = grid.renderer.bounds.size;
        Debug.Log(GridSize);

        //    get position on the quad from -0.5...0.5 (regardless of scale)
        Vector3 gridPosition = grid.transform.InverseTransformPoint( Position );
        Debug.Log(gridPosition);
        //    scale up to a number on the grid, round the number to a whole number, then put back to local size
        gridPosition.x = Mathf.Round( (gridPosition.x * gridSize) / gridSize);
        gridPosition.z = Mathf.Round( (gridPosition.z * gridSize) / gridSize);

        //    don't go out of bounds
        gridPosition.x = Mathf.Min ( 0.5f * GridSize.x, Mathf.Max (-0.5f * GridSize.x, gridPosition.x) );
        gridPosition.z = Mathf.Min ( 0.5f * GridSize.z, Mathf.Max (-0.5f * GridSize.z, gridPosition.z) );

        Position = grid.transform.TransformPoint( gridPosition );
        Position.y = 0.5f;
        // Debug.Log(Position);
        return Position;
    }
}
