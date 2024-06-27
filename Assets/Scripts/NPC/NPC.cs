using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;
    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>();

    private void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica se o diálogo está sendo exibido
        if (DialogueControl.instance.isShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);

            // Move em direção ao waypoint atual
            transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

            // Verifica se chegou ao waypoint atual
            if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
            {
                index++;

                // Verifica se chegou ao final da lista de waypoints
                if (index >= paths.Count)
                {
                    //index = 0; //Vai seguir a ordem de trajeto definida pelo código na unity
                    index = Random.Range(0, paths.Count - 1); //Vai seguir ordem de paths(WayPoints) aleatóriamente.
                }

                UpdateRotation();
            }
        }
    }

    void UpdateRotation()
    {
        // Atualiza a rotação com base na direção do próximo waypoint
        Vector2 direction = paths[index].position - transform.position;

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
