using Assets.Scripts.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool canPlace;
    public bool CanPlace { get { return canPlace; } }


    private Vector2Int coordinates;
    CoordinateLabeler coordinateLabeler;

    private void Awake()
    {
        coordinateLabeler = GetComponent<CoordinateLabeler>();
        
    }

    private void Start()
    {
        coordinates = coordinateLabeler.getCoordinates();
        //GridSystem.Instance.SetCoordinateTraversable(coordinates.x, coordinates.y);
        GridSystem.Instance.AddNodeIntoDictionary(coordinates);
    }


}
