﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float radius;
    public LayerMask layer;
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;
    

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;
    
    private Player player;
    private bool detectPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Caso queira que o skeleton persiga o jogador independente
        // se estiver perto ou não, retire o trecho a seguir"&& detectPlayer"
        if(!isDead && detectPlayer)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                //chegou no limite de distancia | skeleton para
                animControl.PlayAnim(2);
            }
            else
            {
                //skeleton segue o player
                animControl.PlayAnim(1);
            }
            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0)
            {
                //Faz virar a direita
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                //Faz virar a esquerda
                transform.eulerAngles = new Vector2(0, 180);
            }
        }        
    }
    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            //Enxergou o player
            detectPlayer = true;            
        }
        else
        {
            //Não está vendo o player
            detectPlayer = false;
            animControl.PlayAnim(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
