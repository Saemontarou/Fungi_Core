using UnityEngine;

public class WolfGrowlSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider collider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
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
        collider.enabled = false;
    }
}
