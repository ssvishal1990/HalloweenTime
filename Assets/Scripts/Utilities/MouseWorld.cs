using Assets.Scripts.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField] private LayerMask mouseLayerMask;

    private static bool thisNodeIsValid;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 unit action system  " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this;
        thisNodeIsValid = false;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool rayHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mouseLayerMask);
        if (rayHit)
        {
            thisNodeIsValid = true;
            Debug.Log("Inside get position");
            foreach (Transform child in raycastHit.transform)
            {
                if (child.TryGetComponent<CoordinateLabeler>(out CoordinateLabeler coordinateLabeler))
                {
                    bool status = GridSystem.Instance.Grid.ContainsKey(coordinateLabeler.getCoordinates());
                    Debug.Log("Node with Coordinate Label -> " + coordinateLabeler.getCoordinates() + "  traversable status  " + status);
                }
            }
        }else
        {
            thisNodeIsValid=false;
        }
        
        return raycastHit.point;
    }

    public static bool checkIfValidNodeWasSelected()
    {
        return thisNodeIsValid;
    }
}
