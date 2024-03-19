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
        // Return if selectedSite equals null
        if (selectedSite == null)
        {
            return;
        }

        // Get the site level for the selected site
        int siteLevel = (int)selectedSite.Level;

        // Use the SetEnabled() function on every button
        archerButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        // Apply logic based on site level using switch statement
        switch (siteLevel)
        {
            case 0:
                archerButton.SetEnabled(true);
                wizardButton.SetEnabled(true);
                swordButton.SetEnabled(true);
                break;
            case 1:
            case 2:
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                destroyButton.SetEnabled(true);
                break;
            default:
                // Handle invalid site levels if necessary
                break;
        }
    }


    void Start()

    {

        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-tower");

        swordButton = root.Q<Button>("sword-tower");

        wizardButton = root.Q<Button>("wizard-tower");

        updateButton = root.Q<Button>("upgrade");

        destroyButton = root.Q<Button>("delete");



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



    }



    private void OnSwordButtonClicked()

    {



    }



    private void OnWizardButtonClicked()

    {



    }



    private void OnUpdateButtonClicked()

    {



    }



    private void OnDestroyButtonClicked()

    {



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