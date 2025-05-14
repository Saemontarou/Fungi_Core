using UnityEngine;

public class SnakesSound : MonoBehaviour
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
            Invoke("SnakeSound", 1f);
        }
    }
    private void SnakeSound()
    {
        audioSource.Play();
        collider.enabled = false;
    }
}