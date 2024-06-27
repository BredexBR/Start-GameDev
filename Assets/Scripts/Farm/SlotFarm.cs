﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private float waterAmount; //total de agua para nascer uma cenoura
    [SerializeField] private bool detecting;
    private bool dugHole;
    [SerializeField] private int digAmount; //Tempo que deve cavar para abrir um buraco
    private int initialDigAmount;
    private float currentWater;
    private PlayerItems playerItems;

    private void Start(){
        initialDigAmount = digAmount;
        playerItems = FindObjectOfType<PlayerItems>();
    }

    private void Update(){
        if (dugHole)
        {
            if(detecting)
            {
                currentWater += 0.01f;
            }

            //encheu o total de agua necessario
            if (currentWater >= waterAmount)
            {
                //plantar cenoura
                spriteRenderer.sprite = carrot;    

                if (Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.carrots++;
                    currentWater = 0f;
                }
            }    
        }  
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            //fazer o buraco
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if(collision.CompareTag("Water"))
        {
            detecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }
}