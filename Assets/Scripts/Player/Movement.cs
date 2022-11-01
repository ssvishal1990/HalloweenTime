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
    

    void Start()
    {
        currentPlayerCoordinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
        tartgetCoorDinate = GridSystem.Instance.worldPositionToCoorDinatePosition(transform.position);
        
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
            StartMovement();
        }
    }

    private void StartMovement()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * characterRotateSpeed);
    }

    private void inputDetected()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = MouseWorld.GetPosition();
            tartgetCoorDinate = GridSystem.Instance.worldPositionToCoorDinatePosition(targetPosition);
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
