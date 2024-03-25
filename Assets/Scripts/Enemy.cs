using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    public void Damage(int damage)
    {
        // Verlaag de gezondheidswaarde
        health -= damage;

        // Als de gezondheid kleiner of gelijk is aan nul
        if (health <= 0)
        {
            // Vernietig het game object
            GameManager.Instance.AddCredits(points);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                pathIndex++;
                target = EnemySpawner.instance.RequestTarget(path, pathIndex);

                if (target == null)
                {
                    Destroy(gameObject);

                    if (path == Path.Path1)
                    {
                        GameManager.Instance.AttackGate(Path.Path1);
                    }
                    else if (path == Path.Path2)
                    {
                        GameManager.Instance.AttackGate(Path.Path2);
                    }
                }
            }

        }
    }
}
