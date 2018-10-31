using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunManager : NetworkBehaviour
{
    [SerializeField]
    private Transform gunHolder;

    [SerializeField]
    private string gunLayerName = "Gun";

    [SerializeField]
    private Guns baseGun;

    private Guns currentGun;

    private void Start()
    {
        EquipGun(baseGun);
    }

    public Guns GetCurrentGun()
    {
        return currentGun;
    }

    private void EquipGun(Guns _gun)
    {
        currentGun = _gun;

        GameObject _gunIns = (GameObject) Instantiate(_gun.model, gunHolder.position, gunHolder.rotation);
        _gunIns.transform.SetParent(gunHolder);
        if (isLocalPlayer)
            _gunIns.layer = LayerMask.NameToLayer(gunLayerName);
    }
}
