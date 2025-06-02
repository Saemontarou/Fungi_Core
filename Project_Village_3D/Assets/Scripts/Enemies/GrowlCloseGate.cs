using UnityEngine;

public class GrowlCloseGate : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider _collider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("WolfGrowlGate", 1f);
        }
    }
    private void WolfGrowlGate()
    {
        audioSource.Play();
        ActionManager.CloseMainGate();
        _collider.enabled = false;
    }
}