using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject homeFrog;

    public bool GoalOccupied { get { return homeFrog.activeInHierarchy; } }

    public void ShowFrog(bool showFrog)
    {
        homeFrog.SetActive(showFrog);
    }
}
