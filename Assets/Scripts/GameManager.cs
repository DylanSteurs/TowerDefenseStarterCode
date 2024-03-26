using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private ConstructionSite selectedSite;
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();
    private int health;
    private int credits;
    private int wave;
    private int currentWave;
    public TopMenu topMenu;
    private bool waveActive = false;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private Dictionary<TowerType, List<int>> towerPrefabCosts = new Dictionary<TowerType, List<int>>()
    {
        { TowerType.Archer, new List<int> { 35, 70, 140 } }, // Kosten voor Archer-torens op niveau 0, 1 en 2
        { TowerType.Sword, new List<int> { 60, 135, 185 } }, // Kosten voor Sword-torens op niveau 0, 1 en 2
        { TowerType.Wizard, new List<int> { 120, 180, 250 } } // Kosten voor Wizard-torens op niveau 0, 1 en 2
    };
    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde site
        this.selectedSite = site;
        // Geef de geselecteerde site door aan het towerMenu door SetSite aan te roepen
        towerMenu.SetSite(site);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        StartGame();
    }
    public void Build(TowerType type, SiteLevel level)
    {
        // Je kunt niet bouwen als er geen site is geselecteerd
        if (selectedSite == null)
        {
            return;
        }

        // Selecteer de juiste lijst op basis van het torentype
        List<GameObject> towerList = null;
        switch (type)
        {
            case TowerType.Archer:
                towerList = Archers;
                break;
            case TowerType.Sword:
                towerList = Swords;
                break;
            case TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        // Gebruik een switch met het niveau om een GameObject-toren te maken
        GameObject towerPrefab = towerList[(int)level];

        // Haal de positie van de ConstructionSite op
        Vector3 buildPosition = selectedSite.GetBuildPosition();

        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
        int towerCost = GetCost(type, level);
        AddCredits(-towerCost);
        // Configureer de geselecteerde site om de toren in te stellen
        selectedSite.SetTower(towerInstance, level, type); // Voeg level en type toe als
        towerMenu.SetSite(null);
    }
    public void StartGame()
    {
        // Stel de startwaarden in
        
        credits = 500;
        health = 10;
        currentWave = 0; // Initialize with 0 to start with the first wave
        topMenu.UpdateTopMenuLabels(credits, health, currentWave + 1); // Update the labels with the correct wave index
    }

 
    public int GetCredits()
    {
        return credits;
    }
    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public void AttackGate(Path path)
    {
        if (path == Path.Path1 || path == Path.Path2)
        {
            health--;
        }
        else
        {
            Debug.LogWarning("Unkown path: " + path);
        }
    }
    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;

        if (selling)
        {
            cost = towerPrefabCosts[type][(int)level] / 2;
        }
        else
        {
            cost = towerPrefabCosts[type][(int)level];
        }

        return cost;
    }
    public void StartWave()
    {
        if (currentWave == 0 || currentWave > 0)
        {
            currentWave++;
            topMenu.SetWaveLabel("Wave: " + currentWave);
            waveActive = true;
        }
        else
        {
            currentWave = 0; // Initialize with 0 to start with the first wave
            waveActive = true;
        }
    }
    public void EndWave()
    {
        waveActive = false;
    }
}