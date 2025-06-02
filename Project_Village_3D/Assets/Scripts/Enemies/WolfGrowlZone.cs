using UnityEngine;

public class WolfGrowlZone : MonoBehaviour
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
            Invoke("WolfGrowl", 1f);
        }
    }
    private void WolfGrowl()
    {
        audioSource.Play();
        _collider.enabled = false;
    }
}
