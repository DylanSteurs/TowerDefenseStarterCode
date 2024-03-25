using UnityEngine;
using UnityEngine.UIElements;
public class TowerMenu : MonoBehaviour
{
    private ConstructionSite selectedSite;
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;
    private VisualElement root;
    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            // If selectedSite is null, return without enabling any buttons
            return;
        }

        // Access the site level property of selectedSite
        int siteLevel = (int)selectedSite.Level;
        int availableCredits = GameManager.Instance.GetCredits();

        // Disable all buttons initially
        archerButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        if (selectedSite.Level == SiteLevel.Onbebouwd)
        {
            // Voor Level0: Toon alleen de knoppen voor het bouwen van torens
            archerButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Archer, selectedSite.Level));
            swordButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Sword, selectedSite.Level));
            wizardButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Wizard, selectedSite.Level));
            updateButton.SetEnabled(false); // De upgrade-knop is niet beschikbaar op niveau 0
            destroyButton.SetEnabled(false); // De vernietigingsknop is niet beschikbaar op niveau 0
        }
        else if (selectedSite.Level < SiteLevel.lvl3)
        {
            // Voor Level1 en Level2: Toon de upgrade-knop en alle torenbouwknoppen
            archerButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Archer, selectedSite.Level));
            swordButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Sword, selectedSite.Level));
            wizardButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(TowerType.Wizard, selectedSite.Level));
            updateButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(selectedSite.TowerType.Value, selectedSite.Level + 1));
            destroyButton.SetEnabled(true); // De vernietigingsknop is altijd beschikbaar voor upgradebaar niveau
        }
        else if (selectedSite.Level == SiteLevel.lvl3)
        {
            // Voor Level3: Toon alleen de vernietigingsknop, upgrade-knop is niet beschikbaar
            archerButton.SetEnabled(false); // De torenbouwknoppen zijn niet beschikbaar op niveau 3
            swordButton.SetEnabled(false);
            wizardButton.SetEnabled(false);
            updateButton.SetEnabled(false); // De upgrade-knop is niet beschikbaar op niveau 3
            destroyButton.SetEnabled(true); // De vernietigingsknop is altijd beschikbaar voor niveau 3
        }
    }
    public void SetSite(ConstructionSite site)
    {
        // Assign the site to the selectedSite variable
        selectedSite = site;

        if (selectedSite == null)
        {
            // If the selected site is null, hide the menu and return
            root.visible = false;
            return;
        }
        else
        {
            // If the selected site is not null, make sure the menu is visible
            root.visible = true;

            // Call the EvaluateMenu method to update button visibility
            EvaluateMenu();
        }
    }
    void Start()

    {

        root = GetComponent<UIDocument>().rootVisualElement;



        archerButton = root.Q<Button>("archer-button");

        swordButton = root.Q<Button>("sword-button");

        wizardButton = root.Q<Button>("wizard-button");

        updateButton = root.Q<Button>("upgrade-button");

        destroyButton = root.Q<Button>("delete-button");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }
        root.visible = false;
    }
    private void OnArcherButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Archer, SiteLevel.lvl1);
    }
    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Sword, SiteLevel.lvl1);
    }
    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Wizard, SiteLevel.lvl1);
    }
    private void OnUpdateButtonClicked()
    {
        if (selectedSite != null)
        {
            SiteLevel newlevel = selectedSite.Level + 1;

            GameManager.Instance.Build(selectedSite.TowerType.Value, newlevel);
            EvaluateMenu();
        }
    }
    private void OnDestroyButtonClicked()
    {
        if (selectedSite == null)
        {
            return;
        }
        selectedSite.SetTower(null, SiteLevel.Onbebouwd, TowerType.None);
        EvaluateMenu(); 
    }
    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
}