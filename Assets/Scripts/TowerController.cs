using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    public float speed = 100f;  // should be attribute of weapon or bullet

    public HealthSystem healthSystem;
    public Transform pfHealthBar;

    public WeaponSystem weaponSystem;

    // Start is called before the first frame update
    void Awake()
    {
        healthSystem = new HealthSystem(100);

        string[] results;
        results = AssetDatabase.FindAssets("BasicGun");

        foreach (string guid in results)
        {
            weaponSystem = new WeaponSystem(AssetDatabase.LoadAssetAtPath<ScriptableWeapon>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(-1f, -8.5f), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
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
            weaponSystem.FireBullet(transform.position, speed);
        }
        // autofire
        // TODO: reconsider weapon firerate only for autofire...does that make sense?
        // to shoot faster if you can fire single faster?
        if (Input.GetButton("Fire1") && weaponSystem.LastFired <= 0)
        {
            weaponSystem.FireBullet(transform.position, speed);
        }

        weaponSystem.LastFired -= Time.deltaTime;
        weaponSystem.CheckReload(Time.deltaTime);
    }

    
}
