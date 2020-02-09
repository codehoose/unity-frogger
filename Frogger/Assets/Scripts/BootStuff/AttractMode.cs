using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractMode : MonoBehaviour
{

    public GameObject[] _letters;
    public GameObject _konamiCopyright;

    public GameObject _pointTable;
    public GameObject _frogPrefab;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        var frogs = new List<GameObject>();
        var count = 0;

        for (var i = 0; i < 7; i++)
        {
            var currentFrog = Instantiate(_frogPrefab, new Vector3(112, -16), Quaternion.Euler(0, 0, 90));
            frogs.Add(currentFrog);
            var anim = currentFrog.GetComponentInChildren<Animator>();
            anim.SetTrigger("move");

            var target = new Vector3(_letters[i].transform.position.x, -16);
            var t = 0f;
            count = 0;
            while (t < 1.0f)
            {
                currentFrog.transform.position = Vector3.Lerp(new Vector3(112, -16), target, t);
                t += Time.deltaTime / 2;
                count++;
                if (count % 8 == 0)
                {
                    count = 0;
                    anim.SetTrigger("move");
                }
                yield return null;
            }
            currentFrog.transform.position = Vector3.Lerp(new Vector3(112, -16), target, 1);
            currentFrog.transform.rotation = Quaternion.Euler(0, 0, 0);

            yield return null;
        }

        var time = 0f;
        count = 0;

        var startPos = new List<Vector3>();
        foreach(var go in frogs)
        {
            startPos.Add(go.transform.position);
        }

        while (time < 1f)
        {
            var doAnim = false;
            count++;
            if (count % 8 == 0)
            {
                doAnim = true;
                count = 0;
            }

            for (var i = 0; i < 7; i++)
            {
                frogs[i].transform.position = Vector3.Lerp(startPos[i], _letters[i].transform.position, time);
                if (doAnim)
                {
                    frogs[i].GetComponentInChildren<Animator>().SetTrigger("move");
                }
            }

            time += Time.deltaTime / 2;
            yield return null;
        }

        for (var i = 0; i < 7; i++)
        {
            frogs[i].transform.position =  _letters[i].transform.position;
        }

        yield return new WaitForSeconds(2);

        for (var i = 0; i < 7; i++)
        {
            _letters[i].SetActive(true);
            frogs[i].SetActive(false);
            yield return new WaitForSeconds(1);
        }

        _konamiCopyright.SetActive(true);
        _pointTable.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("score", 0);
            SceneManager.LoadScene("Game");
        }
    }
}
