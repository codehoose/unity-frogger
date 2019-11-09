using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject homeFrog;

    public void ShowFrog(bool showFrog)
    {
        homeFrog.SetActive(showFrog);
    }
}
