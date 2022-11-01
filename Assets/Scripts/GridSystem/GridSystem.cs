using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will be responsible for
///  1) World Position to coordinate conversion
///  2) Coordinate Position to world Position Conversion
///  3) Checking the distance between 2 specified Coordinates
///  4) This will contain the matrix which will be used to decide if a particle block is traverasable or not
///  
/// </summary>
public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance { get; private set; }


    [SerializeField] private int snapXEditorSetting;
    [SerializeField] private int snapYEditorSetting;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than 1 unit action system  " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public Vector2Int worldPositionToCoorDinatePosition(Vector3 worldPosition)
    {
        Vector2Int coorDinatePosition = new Vector2Int();
        coorDinatePosition.x = (int)Mathf.Floor(worldPosition.x / snapXEditorSetting);
        coorDinatePosition.y = (int)Mathf.Floor(worldPosition.z / snapXEditorSetting);
        return coorDinatePosition;
    }

    public Vector3 CoorDinatePositionToWorldPosition(Vector2Int coordinates)
    {
        Vector3 worldPosition = new Vector3();
        worldPosition.x  = coordinates .x * snapXEditorSetting;
        worldPosition.z  = coordinates .y * snapYEditorSetting;
        return worldPosition;
    }

    public bool isOnDestination(Vector2Int playerCurrentPosition,
                                Vector2Int playerDesiredPosition)
    {
        if(playerCurrentPosition.x == playerDesiredPosition.x 
            && playerCurrentPosition.y == playerDesiredPosition.y)
            return true;
        return false;
    }
}
