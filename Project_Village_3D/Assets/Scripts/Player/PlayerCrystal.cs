using UnityEngine;
using UnityEngine.UI;

public class PlayerCrystal : MonoBehaviour
{
    static public int crystals;

    [SerializeField] public Text NumberCrystal;

    private void Start()
    {
        crystals = 0;
    }
}
