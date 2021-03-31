using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : GenericHealth
{
    public RectTransform energyUI;
    private float energyUiScale = 0f;

    public Energy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        energyUiScale = CalculateEnergyUiScale(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        energyUiScale = CalculateEnergyUiScale(health, maxHealth);
        energyUI.localScale = new Vector3(energyUiScale, 1.0f, 1.0f);
    }

    float CalculateEnergyUiScale(int energy, int maxEnergy)
    {
        float energyF = energy;
        float maxEnergyF = maxEnergy;
        float scale = energyF / maxEnergyF;
        Debug.Log(scale);
        return scale;
    }
}
