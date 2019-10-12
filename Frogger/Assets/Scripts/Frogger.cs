using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private static readonly float COOLDOWN = 0.25f;

    private bool _moveCooldown = false;
    private Vector3 initialPosition;

    public GameObject frogSprite;
    public Animator frogAnimation;

    public GameObject deathPrefab;

    private Collider2D _floating;
    private bool _inRiver = false;
    private float floatSpeed = 0;

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
                // TODO: RESET ROTATIONS
                transform.position = initialPosition;
                frogSprite.SetActive(true);
                _deathCooldown = 0;
                _inRiver = false;
            }
            return;
        }

        if (_moveCooldown)
        {
            return;
        }

        // Update if on a row
        transform.position += new Vector3(floatSpeed * Time.deltaTime, 0);

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
        _moveCooldown = true;
        frogAnimation.SetTrigger("move");

        var start = transform.position;
        var end = start + delta;
        var time = 0f;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(start, end, time);
            //transform.position += new Vector3(floatSpeed * Time.deltaTime, 0);

            time = time + Time.deltaTime / COOLDOWN;
            yield return null;
        }

        transform.position = end;
        _moveCooldown = false;

        if (_inRiver && !OnFloating())
        {
            DoDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "vehicle":
                DoDeath();
                break;
            case "river":
                _inRiver = true;
                break;
            case "floating":
                var reference = col.GetComponent<RowControllerReference>();
                if (reference != null)
                {
                    floatSpeed = reference.rowControl.speed;
                }
                else
                {
                    
                    floatSpeed = 0;
                }
                _floating = col;
                break;
            case "home":
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "river")
        {
            _inRiver = false;
        }
        else if (col.tag == "floating")
        {
            _floating = null;
            floatSpeed = 0;
        }
    }

    private void DoDeath()
    {
        if (_deathCooldown > 0)
        {
            return;
        }

        _deathCooldown = 1f;
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        frogSprite.SetActive(false);
    }

    private bool OnFloating()
    {
        var thisCollider = GetComponent<Collider2D>();
        return _floating != null && thisCollider.IsTouching(_floating);


        //var collider = Physics2D.OverlapBox(transform.position, Vector2.left / 2, 0, 0, 1);
        //return _floating != null && collider == _floating;
    }
}
