using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D_lsy : Singleton_lsy<Camera2D_lsy>
{
    public Transform Target { get; set; }
    public Vector2 Offset { get; set; }
    public Vector2 PlayerOffset => offset;

    private enum CameraMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    [Header("Target")]
    [SerializeField] private Transform targetTransform;  // which is the Player 

    [Header("Border")]
    [SerializeField] private Vector2 LeftBottom = new Vector2(-4.5f, 0.6f);
    [SerializeField] private Vector2 RightTop = new Vector2(37.5f, 10.5f);

    [Header("Offset")]
    [SerializeField] private Vector2 offset;

    [Header("Mode")]
    [SerializeField] private CameraMode cameraMode = CameraMode.Update;

    protected override void Awake()
    {
        base.Awake();
        Offset = offset;
    }


    private void Update()
    {
        if (cameraMode == CameraMode.Update)
        {
            FollowTarget();
        }
    }

    private void FixedUpdate()
    {
        if (cameraMode == CameraMode.FixedUpdate)
        {
            FollowTarget();
        }
    }

    private void LateUpdate()
    {
        if (cameraMode == CameraMode.LateUpdate)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(targetTransform.position.x + offset.x, targetTransform.position.y + offset.y, transform.position.z);

        //if (isClassroom)
          //  transform.position = new Vector3(11.5f, 10.5f, transform.position.z);
        //else if (isOffice)
          //  transform.position = new Vector3(36.5f, 0.6f, transform.position.z);
        //else
        //{
            if (desiredPosition.x >= LeftBottom.x && desiredPosition.y >= LeftBottom.y && desiredPosition.x <= RightTop.x && desiredPosition.y <= RightTop.y)
                transform.position = desiredPosition;

            else if (desiredPosition.y >= LeftBottom.y && desiredPosition.y <= RightTop.y)
                transform.position = new Vector3(transform.position.x, desiredPosition.y, transform.position.z);

            else if (desiredPosition.x >= LeftBottom.x && desiredPosition.x <= RightTop.x)
                transform.position = new Vector3(desiredPosition.x, transform.position.y, transform.position.z);
        //}
    }
}
