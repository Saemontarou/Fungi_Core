using UnityEngine;

public class SnakeZone : MonoBehaviour
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
            Invoke("SnakeSound", 1f);
        }
    }
    private void SnakeSound()
    {
        audioSource.Play();
        _collider.enabled = false;
    }
}