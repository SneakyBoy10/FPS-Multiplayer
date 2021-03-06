﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float camRotLimit = 85f;

    Animator animator;
    PlayerController controller;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCamRotX = 0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controller = GetComponentInChildren<PlayerController>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", true);
                controller.speed = 7.5f;
            }
            else
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                controller.speed = 5f;
            }
        }else if(velocity == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            currentCamRotX -= cameraRotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -camRotLimit, camRotLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
        }
    }

    public void RotateCamera(float _cameraRotation)
    {
        cameraRotationX = _cameraRotation;
    }
}
