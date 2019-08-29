using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    public float speed = 100f;
    float fireRate = 0.25f;
    float nextFire = 0f;

    public ScriptableWeapon currentWeapon;
    private float lastFired = 0f;
    private float reloadTimer = 0f;
    private int magazine;
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = currentWeapon.reloadSpeed;
        magazine = currentWeapon.magazineCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // fire single
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
        // autofire
        // TODO: reconsider weapon firerate only for autofire...does that make sense?
        // to shoot faster if you can fire single faster?
        if (Input.GetButton("Fire1") && lastFired <= 0)
        {
            FireBullet();
        }

        lastFired -= Time.deltaTime;

        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                reloading = false;
                magazine = currentWeapon.magazineCapacity;
            }
        }
    }

    private void FireBullet()
    {
        if (!reloading)
        {
            if (magazine == 1)
                StartReload();

            Vector3 thisPosition = transform.position;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            // rotate towards target position
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, targetPosition - thisPosition);
            GameObject goBullet = Instantiate(currentWeapon.bulletPrefab, thisPosition, rotation);
            Bullet bullet = goBullet.GetComponent<Bullet>();
            Vector3 targetVector = targetPosition - thisPosition;
            bullet.GetComponent<Rigidbody2D>().AddForce(targetVector.normalized * speed);

            lastFired = currentWeapon.fireRate;
            magazine -= 1;
        }
    }

    private void StartReload()
    {
        reloading = true;
        reloadTimer = currentWeapon.reloadSpeed;
    }
}
