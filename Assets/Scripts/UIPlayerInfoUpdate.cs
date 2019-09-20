using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerInfoUpdate : MonoBehaviour
{
    public TextMeshProUGUI magazineTMP;
    public TextMeshProUGUI reloadingTMP;
    public TowerController tc;

    private void Awake()
    {
        reloadingTMP.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        tc.weaponSystem.OnMagazineChange += UpdateMagazineDisplay;
        tc.weaponSystem.OnReloadChange += UpdateReloadingDisplay;
    }

    private void UpdateMagazineDisplay(int magazine)
    {
        string displayText = "";
        for (int i = 0; i < magazine; i++)
        {
            displayText = displayText + "O";
        }
        magazineTMP.text = displayText;
    }

    private void UpdateReloadingDisplay(bool reloading)
    {
        if (reloading)
        {
            reloadingTMP.enabled = true;
        }
        else
        {
            reloadingTMP.enabled = false;
        }
    }
}
