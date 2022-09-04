using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DynamicBox.EventManagement;
using DynamicBox.EventManagement.Speed;


public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float motorForce;
    [SerializeField] private WheelCollider[] wheelsC;
    [SerializeField] private Transform[] wheelT;
    private float steeringAngle;
    private float maxSteerAngle = 60;
    private Vector2 inputs;
    public int speed;

    #region Unity Methods
    private void OnEnable()
    {
        EventManager.Instance.AddListener<OnPlayerMoveEvent>(ShowSpeed);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<OnPlayerMoveEvent>(ShowSpeed);
    }
    private void FixedUpdate()
    {
        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");
        Steer();
        Accelerate();
        UpdateWheelPoses(wheelsC, wheelT);
    }
    #endregion

    #region UI Methods
    private void ShowSpeed(OnPlayerMoveEvent eventDetails)
    {
        eventDetails.Speed.text = Mathf.Round(rb.velocity.magnitude) * 5 + "";
        speed = int.Parse(eventDetails.Speed.text);
    }
    #endregion
    #region Car Control
    private void Accelerate()
    {
        wheelsC[0].motorTorque = inputs.y * motorForce;
        wheelsC[1].motorTorque = inputs.y * motorForce;
    }
    private void Steer()
    {
        steeringAngle = inputs.x*maxSteerAngle;
        wheelsC[0].steerAngle =steeringAngle;
        wheelsC[1].steerAngle =steeringAngle;
    }
    private void UpdateWheelPoses(WheelCollider[] _wheels,Transform[] _points)
    {
        for (int i = 0; i < _wheels.Length; i++)
        {
            Vector3 _pos = _points[i].position;
            Quaternion _quat = _points[i].rotation;
            
            _wheels[i].GetWorldPose(out _pos, out _quat);

            _points[i].position = _pos;
            _points[i].rotation = _quat;
        }
    }
    #endregion
}
