using Unity.VisualScripting;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    [Header("Кристаллы в Canvas")]
    public GameObject _canvasCrystalRed;
    public GameObject _canvasCrystalGreen;
    public GameObject _canvasCrystalBlue;
    public GameObject _canvasCrystalOrange;
    public GameObject _canvasCrystalPurple;

    public GameObject _canvasKeyFence;
    
    [Header("Кристаллы на локации")]
    public GameObject _redShard;
    public GameObject _greenShard;
    public GameObject _blueShard;
    public GameObject _orangeShard;
    public GameObject _purpleShard;
    
    [Header("Кристаллы на алтаре")]
    public GameObject altarGems;

    public GameObject allGems;
    
    public GameObject keyFence;

    public AudioSource _takeCrystal;
    
    public static CrystalManager Instance;
    
    private void Start()
    {
        Instance = this;
    }
    
    public void TakeGemRed()
    {
        _canvasCrystalRed.SetActive(true);
        _redShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemGreen()
    {
        _canvasCrystalGreen.SetActive(true);
        _greenShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemBlue()
    {
        _canvasCrystalBlue.SetActive(true);
        _blueShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemOrange()
    {
        _canvasCrystalOrange.SetActive(true);
        _orangeShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemPurple()
    {
        _canvasCrystalPurple.SetActive(true);
        _purpleShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void PutAllGems()
    {
        altarGems.SetActive(true);
        allGems.SetActive(false);
    }
    
    public void KeyHide()
    {
        keyFence.SetActive(false);
        _canvasKeyFence.SetActive(true);
        
    }
}