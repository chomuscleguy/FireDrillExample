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
        Debug.Log("�Ⱦ�");
    }
    private void drop()
    {
        Debug.Log("���");
    }

}
