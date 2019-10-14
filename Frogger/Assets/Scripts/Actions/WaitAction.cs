using System.Collections;
using UnityEngine;

public class WaitAction : IAction
{
    private float _duration;

    public WaitAction(float duration)
    {
        _duration = duration;
    }

    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(_duration);
    }
}