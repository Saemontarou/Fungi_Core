using UnityEngine;
using UnityEngine.UI;

public class CrystalScore : MonoBehaviour
{
    public static int Crystals;

    [SerializeField] public Text numberCrystal;

    private void Start()
    {
        Crystals = 0;
    }
}