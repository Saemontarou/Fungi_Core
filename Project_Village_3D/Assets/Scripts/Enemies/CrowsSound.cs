using UnityEngine;

public class CrowsSound : MonoBehaviour
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
            Invoke("CrowSound", 1f);
        }
    }
    private void CrowSound()
    {
        audioSource.Play();
        collider.enabled = false;
    }
}