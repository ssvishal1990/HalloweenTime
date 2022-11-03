using Assets.Scripts.GridSystem;
using System.Collections;
using UnityEngine;


public class GridChecker : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        outputGrid();
    }

    public void outputGrid()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(GridSystem.Instance.Grid.Count);
        }
    }

}