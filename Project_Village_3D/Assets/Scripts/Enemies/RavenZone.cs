using UnityEngine;

public class RavenZone : MonoBehaviour
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
            Invoke("CrowSound", 1f);
        }
    }
    private void CrowSound()
    {
        audioSource.Play();
        _collider.enabled = false;
    }
}