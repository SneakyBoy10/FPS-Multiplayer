using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(GunManager))]
public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    private Guns currentGun;

    [SerializeField]
    private GameObject gunGFX;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private GunManager gunManager;

    private void Start()
    {
        if (cam == null)
        {
            Debug.Log("PlayerShoot: No camera referenced");
            this.enabled = false;
        }

        gunManager = GetComponent<GunManager>();
    }

    private void Update()
    {
        currentGun = gunManager.GetCurrentGun();
        if (currentGun.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentGun.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentGun.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentGun.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }
}