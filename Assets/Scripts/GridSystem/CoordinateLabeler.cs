using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
[RequireComponent(typeof(WayPoint))]
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro coordinateLabel;
    WayPoint wayPoint;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        coordinateLabel = GetComponent<TextMeshPro>();
        wayPoint = GetComponent<WayPoint>();
        GetDisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            GetDisplayCoordinates();
            SetParentObjectName();
        }
        Togglelabels();
    }

    void Togglelabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            coordinateLabel.enabled = !coordinateLabel.enabled;
        }
    }

    private void SetParentObjectName()
    {
        transform.parent.gameObject.name = coordinateLabel.text;
    }

    private void GetDisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        coordinateLabel.text = coordinates.x + " ," + coordinates.y;
    }
}
