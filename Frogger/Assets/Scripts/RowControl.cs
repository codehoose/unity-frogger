using UnityEngine;

public class RowControl : MonoBehaviour
{
    private static float SCREEN_WIDTH = 224;

    private float startPos;
    private bool goingLeft = true;

    private GameObject leading;
    private GameObject caboose;

    public GameObject[] rowObjects;

    public float speed;
    
    void Start()
    {
        startPos = rowObjects[0].transform.position.x;
        leading = rowObjects[0];
        caboose = rowObjects[1];
        if (startPos > 0)
        {
            goingLeft = false;
        }
    }

    void Update()
    {
        foreach (var go in rowObjects)
        {
            go.transform.position += new Vector3(speed * Time.deltaTime, 0);
        }

        if (goingLeft)
        {
            if (caboose.transform.position.x <= startPos)
            {
                leading.transform.position = caboose.transform.position;
                caboose.transform.position = leading.transform.position + new Vector3(SCREEN_WIDTH, 0);
            }
        }
        else
        {
            if (caboose.transform.position.x >= startPos)
            {
                leading.transform.position = caboose.transform.position;
                caboose.transform.position = leading.transform.position - new Vector3(SCREEN_WIDTH, 0);
            }
        }
    }
}
