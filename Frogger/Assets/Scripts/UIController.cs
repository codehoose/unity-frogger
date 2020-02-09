using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameController gameController;

    public TextMeshProUGUI score;
    public TextMeshProUGUI hiScore;
    public TextMeshProUGUI gameOver;

    public Transform panel;

    public GameObject livesIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = $"{gameController.Score}";
        hiScore.text = $"{gameController.HiScore}";
        UpdateLivesIndicator();
        if (gameController.GameOver)
        {
            gameOver.gameObject.SetActive(true);
            StartCoroutine(WaitAndLoadAttactMode());
        }
    }

    void UpdateLivesIndicator()
    {
        var currentValue = gameController.Lives - 1;
        if (currentValue == panel.childCount)
        {
            return;
        }

        foreach (Transform t in panel)
        {
            Destroy(t.gameObject);
        }

        for (var i = 0; i < currentValue; i++)
        {
            Instantiate(livesIndicator, panel);
        }
    }

    IEnumerator WaitAndLoadAttactMode()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("AttractMode");
    }
}
