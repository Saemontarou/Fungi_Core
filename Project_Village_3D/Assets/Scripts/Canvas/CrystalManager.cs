using System;
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
    
    public GameObject _canvasEmptyGems;

    public GameObject _canvasSoulKey;
    
    [Header("Кристаллы на локации")]
    public GameObject _redShard;
    public GameObject _greenShard;
    public GameObject _blueShard;
    public GameObject _orangeShard;
    public GameObject _purpleShard;
    
    [Header("Кристаллы на алтаре")]
    public GameObject altarGems;
    
    public GameObject keyFence;

    public AudioSource _takeCrystal;
    
    public int Value;
    
    public static CrystalManager Instance;

    private void OnEnable()
    {
        ActionManager.PutAllCrystals += PutAllGems;
        ActionManager.TakeStoneKey += KeyHide;
        ActionManager.KeyCanvasHide += KeyInsert;
        
        ActionManager.RedShardGrab += TakeGemRed;
        ActionManager.GreenShardGrab += TakeGemGreen;
        ActionManager.BlueShardGrab += TakeGemBlue;
        ActionManager.OrangeShardGrab += TakeGemOrange;
        ActionManager.PurpleShardGrab += TakeGemPurple;
    }

    private void Start()
    {
        Instance = this;
    }
    
    public void TakeGemRed()
    {
        Debug.Log("YOU PICK RED");
        PlayerCrystal.crystals += Value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrystal>().NumberCrystal.text = PlayerCrystal.crystals.ToString();
        _canvasCrystalRed.SetActive(true);
        _redShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemGreen()
    {
        Debug.Log("YOU PICK GREEN");
        PlayerCrystal.crystals += Value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrystal>().NumberCrystal.text = PlayerCrystal.crystals.ToString();
        _canvasCrystalGreen.SetActive(true);
        _greenShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemBlue()
    {
        Debug.Log("YOU PICK BLUE");
        PlayerCrystal.crystals += Value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrystal>().NumberCrystal.text = PlayerCrystal.crystals.ToString();
        _canvasCrystalBlue.SetActive(true);
        _blueShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemOrange()
    {
        Debug.Log("YOU PICK ORANGE");
        PlayerCrystal.crystals += Value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrystal>().NumberCrystal.text = PlayerCrystal.crystals.ToString();
        _canvasCrystalOrange.SetActive(true);
        _orangeShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void TakeGemPurple()
    {
        Debug.Log("YOU PICK PURPLE");
        PlayerCrystal.crystals += Value;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCrystal>().NumberCrystal.text = PlayerCrystal.crystals.ToString();
        _canvasCrystalPurple.SetActive(true);
        _purpleShard.SetActive(false);
        _takeCrystal.Play();
    }
    
    public void PutAllGems()
    {
        altarGems.SetActive(true);
        _canvasEmptyGems.SetActive(false);
    }
    
    public void KeyHide()
    {
        keyFence.SetActive(false);
        _canvasSoulKey.SetActive(true);
    }

    public void KeyInsert()
    {
        _canvasSoulKey.SetActive(false);
    }

    private void OnDisable()
    {
        ActionManager.PutAllCrystals -= PutAllGems;
        ActionManager.TakeStoneKey -= KeyHide;
        ActionManager.KeyCanvasHide -= KeyInsert;
        
        ActionManager.RedShardGrab -= TakeGemRed;
        ActionManager.GreenShardGrab -= TakeGemGreen;
        ActionManager.BlueShardGrab -= TakeGemBlue;
        ActionManager.OrangeShardGrab -= TakeGemOrange;
        ActionManager.PurpleShardGrab -= TakeGemPurple;
    }
}