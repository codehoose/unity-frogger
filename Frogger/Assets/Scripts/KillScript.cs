using System.Collections;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    public float duration = 1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
