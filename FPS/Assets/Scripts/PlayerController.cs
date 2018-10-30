using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField]
    private float looksens = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHor = transform.right * _xMov;
        Vector3 _movVer = transform.forward * _zMov;

        Vector3 _velocity = (_movHor + _movVer) * speed;

        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0) * looksens;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * looksens;

        motor.RotateCamera(_cameraRotationX);
    }
}
