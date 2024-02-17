using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject shootingLine;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform bulletsParent;

    [Header(" Actions ")]
    public static Action onShot;

    [Header(" Settings ")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioSource gunSound;
    private bool canShoot;

   
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
        PlayerMovement.onDied += DiedCallback;
    }



    void Start()
    {
        SetShootingLineVisibility(false);
        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;

    }

    private void OnDestroy()
    {
        PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
        PlayerMovement.onDied -= DiedCallback;
    }


    private void EnteredWarzoneCallback()
    {
        
        SetShootingLineVisibility(true);
        canShoot = true;
    }

    private void SetShootingLineVisibility(bool visibility)
    {
        if (shootingLine != null)
        {
            shootingLine.SetActive(visibility);
        }
        
        //shootingLine.SetActive(visibility);
    }

    private void ExitedWarzoneCallback()
    {
        SetShootingLineVisibility(false);
        canShoot = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (canShoot)
            ManageShooting();
    }

    private void ManageShooting()
    {
       if (Input.GetMouseButtonDown(0) && UIBulletsContainer.instance.CanShoot())
          Shoot();
    }

    private void Shoot()
    {

      

        Vector3 direction = bulletSpawnPosition.right;
        direction.z = 0;

        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity,bulletsParent);
        bulletInstance.Configure(direction* bulletSpeed);

        if (gunSound != null && gunSound.clip != null)
        {
            gunSound.PlayOneShot(gunSound.clip);
        }


        onShot?.Invoke();
    }

    private void DiedCallback()
    {
        SetShootingLineVisibility(false);
        canShoot = false;
    }


}
