using UnityEngine;

public class Extinguish : MonoBehaviour, IInteract
{
    private bool isPick = false;

    public void interact()
    {
        if (isPick)
        {
            drop();
        }
        else
        {
            pickUp();
        }
    }

    private void pickUp()
    {
        Debug.Log("픽업");
    }
    private void drop()
    {
        Debug.Log("드랍");
    }

}
