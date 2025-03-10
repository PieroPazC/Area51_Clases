﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {
    public Transform defaultTarget;
    public float defaultSize = 5f;
    public Transform currentTarget;
    public float currentSize;
    public float speed;
    public float zoomSpeed = 3f;
    public Vector3 camPosition { get { return Camera.main.transform.position; } }

    public float[] axisLimits;

    // Use this for initialization
    void Start()
    {
        currentTarget = defaultTarget;
        currentSize = defaultSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, ConvertToCamDepth(CheckCamLimit(currentTarget.position)), speed * Time.deltaTime);
        if (Camera.main.orthographicSize != currentSize)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, currentSize, zoomSpeed * Time.deltaTime);
        }
    }

    Vector3 CheckCamLimit(Vector3 vector)
    {
        if (currentTarget.position.y <= axisLimits[3] && camPosition.y <= axisLimits[3])
        {
            vector.y = axisLimits[3];
        }
        return vector;
    }

    Vector3 ConvertToCamDepth(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, camPosition.z);
    }

    public void SwitchTarget(Transform target, float size = 5)
    {
        currentTarget = target == null ? defaultTarget : target;
        currentSize = size;
    }
}
