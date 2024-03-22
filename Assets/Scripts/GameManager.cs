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
        //TopMenu.UpdateTopMenuLabels(credits, health, currentWave + 1); // Update the labels with the correct wave index
    }

    public int GetCurrentWave()
    {
        return currentWave - 1; // Geef de huidige golfindex terug
    }
    public int GetCredits()
    {
        // Return het huidige aantal credits
        return credits;
    }
    public void RemoveCredits(int amount)
    {
        // Verminder credits
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
}