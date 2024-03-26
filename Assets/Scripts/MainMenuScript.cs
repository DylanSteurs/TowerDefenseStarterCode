using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public TextField nameField;
    // Start is called before the first frame update
    public void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        exitButton = root.Q<Button>("exit-button");
        nameField = root.Q<TextField>("playername");

        startButton.clicked += Start;
        exitButton.clicked += Quit;
    }
    private void Start()
    {
        SceneManager.LoadScene("GameScene");
    }
    private void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
