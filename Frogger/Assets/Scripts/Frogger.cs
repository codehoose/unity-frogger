using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private static readonly float COOLDOWN = 0.25f;

    private bool _isCoolingDown = false;
    private Vector3 initialPosition;

    public GameObject frogSprite;
    public Animator frogAnimation;

    public GameObject deathPrefab;

    public float _deathCooldown = 0f;

    void Start()
    {
        initialPosition = transform.position;
    }
    
    void Update()
    {
        if(_deathCooldown > 0)
        {
            _deathCooldown = _deathCooldown - Time.deltaTime;
            if (_deathCooldown <= 0)
            {
                // TODO: RESET ROTATIONS
                transform.position = initialPosition;
                frogSprite.SetActive(true);
                _deathCooldown = 0;
            }
            return;
        }

        if (_isCoolingDown)
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
            }
            else
            {
                frogSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(horiz) * 180));
            }
            StartCoroutine(Move(new Vector3(0, Mathf.Sign(vert) * 16, 0)));
        }
        else if (Mathf.Abs(horiz) > 0)
        {
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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_deathCooldown > 0)
        {
            return;
        }

        _deathCooldown = 1f;
        Instantiate(deathPrefab, transform.position, Quaternion.identity);

        frogSprite.SetActive(false);
    }
}
