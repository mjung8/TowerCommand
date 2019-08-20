using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    float xScaleMultiplier = 7.5f;
    private Transform bar;

    // Start is called before the first frame update
    void Awake()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized * xScaleMultiplier, 1f);
    }

}
