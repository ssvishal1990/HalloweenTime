using Assets.Scripts.GridSystem;
using Assets.Scripts.Pathfinder;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float characterRotateSpeed = 5f;



    private Vector3 targetPosition;

    private Vector2Int tartgetCoorDinate;
    private Vector2Int currentPlayerCoordinate;

    private PathFinder pathFinder;

    private List<Node> path;
    private Vector2Int middleNodeCoordinate;

    private bool coroutineBusy;

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
    }

    void Start()
    {
        currentPlayerCoordinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
        tartgetCoorDinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
        pathFinder.setStartCoordinate(currentPlayerCoordinate);
        pathFinder.setEndCoordinate(tartgetCoorDinate);
        middleNodeCoordinate = tartgetCoorDinate;
        path = new List<Node>();
        coroutineBusy = false;
    }



    void Update()
    {
        currentPlayerCoordinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
        inputDetected();
        CheckIfPlayerIsOnRequirePosition();
    }

    private void CheckIfPlayerIsOnRequirePosition()
    {
        if (!PlayerIsOnRequiredLocation())
        {
            //StartMovement();
            StartMovementByPathFinding();
        }
    }

    private void StartMovementByPathFinding()
    {
        if (path.Count == 0)
        {
            currentPlayerCoordinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
            path = pathFinder.GetNewPath(currentPlayerCoordinate);
            middleNodeCoordinate = path[0].coordinate;
        }
        if (!coroutineBusy)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    private IEnumerator FollowPath()
    {
        coroutineBusy = true;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = GridSystem.Instance.CoorDinatePositionToWorldPosition(path[i].coordinate);
            endPosition.y = transform.position.y;
            float travelPercent = 0f;
            //transform.position = endPosition;
            bool status = true ;
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * moveSpeed;
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                //transform.position += moveDir * moveSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * characterRotateSpeed);
                yield return new WaitForEndOfFrame();
            }


        }
        yield return new WaitForEndOfFrame();
        coroutineBusy = false;

    }

    private void StartMovement(Vector3 targetPosition)
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * characterRotateSpeed);
        //transform.LookAt(targetPosition);
    }

    private void inputDetected()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = MouseWorld.GetPosition();
            if (!MouseWorld.checkIfValidNodeWasSelected())
            {
                return;
            }
            Vector2Int initialTargetCoordinate = tartgetCoorDinate;
            tartgetCoorDinate = GridSystem.Instance.worldPositionToCoorDinatePosition(targetPosition);
            if (initialTargetCoordinate != tartgetCoorDinate)
            {
                initialTargetCoordinate = tartgetCoorDinate;
                path.Clear();
            }
            pathFinder.setEndCoordinate(tartgetCoorDinate);
            PlayerIsOnRequiredLocation();
        }
    }

    private bool PlayerIsOnRequiredLocation()
    {
        if (GridSystem.Instance.isOnDestination(currentPlayerCoordinate, tartgetCoorDinate))
        {
            return true;
        }
        return false;
    }
}
