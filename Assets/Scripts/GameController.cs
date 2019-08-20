using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float healthBarSize = 1f;
    private float initialDuration = 30f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetSize(healthBarSize);
    }

    private void Update()
    {
        //if (healthBarSize > 0)
        //{
        //    //healthBarSize = Mathf.Lerp(healthBarSize, healthBarSize - 1 / 30f, Time.deltaTime * 5f);
        //    healthBarSize -= Time.deltaTime * (1 / initialDuration);
        //}

        //if (healthBarSize < 0)
        //{
        //    healthBarSize = 0;
        //    // game over
        //}


    }

    private void FixedUpdate()
    {
        //healthBar.SetSize(healthBarSize);
    }

}
