using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private static readonly float COOLDOWN = 0.25f;

    private bool _isCoolingDown = false;
    private bool _inRiver = false;
    private Vector3 initialPosition;
    private float _horizontalSpeed = 0f;

    public GameObject frogSprite;
    public Animator frogAnimation;

    public GameObject deathPrefab;
    public GameController gameController;

    public float _deathCooldown = 0f;
    
    void Start()
    {
        initialPosition = transform.position;
    }
    
    void Update()
    {
        if (_deathCooldown > 0)
        {
            _deathCooldown = _deathCooldown - Time.deltaTime;
            if (_deathCooldown <= 0)
            {
                ResetFrog();
                _deathCooldown = 0;
            }
            return;
        }

        transform.position += new Vector3(_horizontalSpeed * Time.deltaTime, 0);

        if (_isCoolingDown || gameController.GameOver)
        {
            return;
        }

        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        if (Mathf.Abs(vert) > 0)
        {
            if (vert > 0)
            {
                frogSprite.transform.rotation = Quaternion.identity;
                _horizontalSpeed = 0f;
            }
            else
            {
                frogSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(horiz) * 180));
            }
            AudioController.Instance.PlayLeap();
            StartCoroutine(Move(new Vector3(0, Mathf.Sign(vert) * 16, 0)));
        }
        else if (Mathf.Abs(horiz) > 0)
        {
            AudioController.Instance.PlayLeap();
            frogSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(horiz) * -90));
            StartCoroutine(Move(new Vector3(Mathf.Sign(horiz) * 16, 0, 0)));
        }
    }

    private IEnumerator Move(Vector3 delta)
    {
        _isCoolingDown = true;
        frogAnimation.SetTrigger("move");

        var start = transform.position;
        var end = start + delta;
        var time = 0f;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(start, end, time);
            time = time + Time.deltaTime / COOLDOWN;
            yield return null;
        }

        transform.position = end;
        _isCoolingDown = false;
        
        // If the player lands in the river they are dead
        if (_horizontalSpeed == 0 && _inRiver)
        {
            KillFrog(_inRiver);
        }
        else
        {
            gameController.IncrementScore(ScoreValues.Step);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "floating")
        {
            _horizontalSpeed = col.GetComponent<Vehicle>().Speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_deathCooldown > 0)
        {
            return;
        }

        switch(col.tag)
        {
            case "vehicle":
            case "killzone":
                KillFrog();
                break;
            case "floating":
                _horizontalSpeed = col.GetComponent<Vehicle>().Speed;
                break;
            case "river":
                _inRiver = true;
                break;
            case "Finish":
                FrogIsHome(col.gameObject);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "river":
                _inRiver = false;
                _horizontalSpeed = 0;
                break;
            case "floating":
                _horizontalSpeed = 0;
                break;
        }
    }

    void KillFrog(bool inRiver = false)
    {
        if (inRiver)
        {
            AudioController.Instance.PlayDrown();
        }
        else
        {
            AudioController.Instance.PlaySquish();
        }
        _deathCooldown = 1f;
        gameController.DecrementLives();
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        frogSprite.SetActive(false);
    }

    void FrogIsHome(GameObject go)
    {
        var goal = go.GetComponent<Goal>();
        if (goal.GoalOccupied)
        {
            KillFrog();
        }
        else
        {
            frogSprite.SetActive(false);
            _deathCooldown = 1f;
            gameController.IncrementScore(ScoreValues.Home);
            goal.ShowFrog(true);
        }
    }

    void ResetFrog()
    {
        if (!gameController.GameOver)
        {
            frogSprite.transform.rotation = Quaternion.identity;
            transform.position = initialPosition;
            frogSprite.SetActive(true);
        }
    }
}
