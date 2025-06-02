using UnityEngine;

public class SmallStones : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActionManager.TakeSmallStones();
        }
    }
}