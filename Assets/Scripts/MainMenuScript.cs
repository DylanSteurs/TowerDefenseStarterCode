using UnityEngine;
using UnityEngine.UIElements;
public class MainMenuScript : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public TextField nameField;
    private VisualElement root;
    // Start is called before the first frame update
    public void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        exitButton = root.Q<Button>("exit-button");
        nameField = root.Q<TextField>("playername");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
