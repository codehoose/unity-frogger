using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontWipe : MonoBehaviour
{
    public GameObject _fullScreen;
    public TextMeshProUGUI _topRow;
    public TextMeshProUGUI _bottomRow;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _fullScreen.SetActive(false);
        _topRow.gameObject.SetActive(true);
        _bottomRow.gameObject.SetActive(true);

        for (var i = 26; i >= 0; i--)
        {
            var zeros = "".PadRight(i, '0');
            var theRow = $"{zeros}\r\n{zeros}";
            _topRow.text = theRow;
            _bottomRow.text = theRow;
            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene("AttractMode");
    }
}
