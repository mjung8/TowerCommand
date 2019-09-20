using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem
{
    public event Action<int> OnMagazineChange;
    public event Action<bool> OnReloadChange;

    private ScriptableWeapon currentWeapon;
    public float LastFired { get; set; } = 0f;
    public float ReloadTimer { get; set; } = 0f;
    public int Magazine { get; protected set; }   // this can be property with a setter
    public bool Reloading { get; set; } = false;

    public WeaponSystem(ScriptableWeapon weapon)
    {
        currentWeapon = weapon;
        ReloadTimer = currentWeapon.reloadSpeed;
        Magazine = currentWeapon.magazineCapacity;
    }

    public void FireBullet(Vector3 pos, float speed)
    {
        Debug.Log("FireBullet!");
        if (!Reloading)
        {
            if (Magazine == 1)
                StartReload();

            Vector3 thisPosition = pos;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            // rotate towards target position
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, targetPosition - thisPosition);
            GameObject goBullet = UnityEngine.Object.Instantiate(currentWeapon.bulletPrefab, thisPosition, rotation);
            Bullet bullet = goBullet.GetComponent<Bullet>();
            Vector3 targetVector = targetPosition - thisPosition;
            bullet.GetComponent<Rigidbody2D>().AddForce(targetVector.normalized * speed);

            LastFired = currentWeapon.fireRate;
            Magazine -= 1;
            OnMagazineChange?.Invoke(Magazine);
        }
    }

    private void StartReload()
    {
        Reloading = true;
        ReloadTimer = currentWeapon.reloadSpeed;
        OnReloadChange?.Invoke(Reloading);
    }

    public void CheckReload(float time)
    {
        if (Reloading)
        {
            ReloadTimer -= time;
            if (ReloadTimer <= 0)
            {
                Reloading = false;
                Magazine = currentWeapon.magazineCapacity;
                OnReloadChange?.Invoke(Reloading);
            }
        }
        
    }

}
