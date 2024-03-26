using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    private int ufoCounter = 0;

    public static EnemySpawner Get { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void StartWave(int number)
    {
        ufoCounter = 0;
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f); // start invoking the wave function
                break;
            default:
                break;
        }
    }
    public void StartWave1()
    {

        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;
        
        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Path.Path1);
        }

        else
        {
            // the last Enemy will be level 2 
            SpawnEnemy(1, Path.Path1);
        }

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1"); // the reverse of InvokeRepeating 
            // depending on your singleton declaration, Get might be somthing else 
            GameManager.Instance.EndWave(); // let the gameManager know. 
        }

    }
    private void SpawnEnemy(int type, Path path)
    {
        GameManager.Instance.AddInGameEnemy();
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        if (path == Path.Path1)
        {
            spawnPosition = Path1[0].transform.position;
            spawnRotation = Path1[0].transform.rotation;
        }
        else if (path == Path.Path2)
        {
            spawnPosition = Path2[0].transform.position;
            spawnRotation = Path2[0].transform.rotation;
        }
        else
        {
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
            Debug.LogError("invalid path secified!");
            return;
        }
        var newEnemy = Instantiate(enemies[type], spawnPosition, spawnRotation);
        var script = newEnemy.GetComponent<Enemy>();

        script.path = path;
        script.target = Path1[1];
    }
    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> currentPath = null;
        switch (path)
        {
            case Path.Path1:
                currentPath = Path1;
                break;
            case Path.Path2:
                currentPath = Path2;
                break;
            default:
                Debug.LogError("invalid path secified!");
                break;
        }
        if (currentPath == null || index < 0 || index >= currentPath.Count)
        {
            Debug.LogError("invalid path or index!");
            return null;
        }
        else { return currentPath[index]; }
    }
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnTester() { SpawnEnemy(0, Path.Path1); }
}
