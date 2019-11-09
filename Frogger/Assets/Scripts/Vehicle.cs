using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private float _xMax;

    public float Speed { get; private set; }

    public void Init(Vector3 position, float speed, float xMax = 130)
    {
        transform.position = position;
        Speed = speed;
        _xMax = xMax;
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.position += new Vector3(Speed * Time.deltaTime, 0);
        var absX = Mathf.Abs(transform.position.x);
        if (absX > _xMax)
        {
            gameObject.SetActive(false);
        }
    }
}