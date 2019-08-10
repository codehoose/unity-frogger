using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private static readonly float COOLDOWN = 0.25f;

    private bool _isCoolingDown = false;

    public GameObject frogSprite;
    public Animator frogAnimation;

    void Update()
    {
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
}
