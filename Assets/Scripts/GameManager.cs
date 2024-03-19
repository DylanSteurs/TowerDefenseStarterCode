using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ConstructionSite selectedSite;
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();

    // Start is called before the first frame update 
    public void SelectSite(ConstructionSite site)
    {
        this.selectedSite = site;
        
        towerMenu.SetSite(site);

    }
    void Start()

    {

        towerMenu = TowerMenu.GetComponent<TowerMenu>();

    }


}
