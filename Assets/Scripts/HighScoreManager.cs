using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager _instance;

    // Singleton instance property
    public static HighScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("HighScoreManager");
                _instance = singletonObject.AddComponent<HighScoreManager>();
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    // Public properties
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
