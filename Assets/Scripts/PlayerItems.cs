using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    public int carrots;
    public int totalWood;
    public float currentWater;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 50f;
    public float carrotsLimit = 10f;
    public float woodLimit = 5f;
    public float fishesLimit = 3f;

    public void WaterLimit(float water)
    {
        if(currentWater < waterLimit)
        {
            currentWater += water;
        }
    }

}
