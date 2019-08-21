using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float speed = 100f;
    float fireRate = 0.25f;
    float nextFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
            nextFire = Time.time + fireRate;
            FireBullet();
        }

        // autofire
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireBullet();
        }
    }

    void FireBullet()
    {
        // declaring a vector3 based on mousePos on click
        Vector3 targetPosition = Input.mousePosition;
        // stop moving on z axis (sprite doesn't skew)
        targetPosition.z = transform.position.z - Camera.main.transform.position.z;
        // convert click position to world position
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);

        // rotate towards target position
        Quaternion q = Quaternion.FromToRotation(Vector3.up, targetPosition - transform.position);
        // declare rigibody2D go; which is a spawned rigidbody2d of type bullet
        Vector3 spawnPosition = transform.position;
        // spawnPosition.y = spawnPosition.y + 5;
        Rigidbody2D go = (Rigidbody2D)Instantiate(bullet, spawnPosition, q);
        // AddForce to go's rigidbody2d (direction * speed)
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * speed);
    }
}
