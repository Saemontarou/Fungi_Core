using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject[] _hazards;
   [SerializeField] private Vector3 _spawnValues;
   [SerializeField] private int _hazardCount;
   [SerializeField] private float _spawnWait;
   [SerializeField] private float _startWait;
   [SerializeField] private float _waveWait;

   [SerializeField] private TMP_Text _scoreText;

   private bool _isGameOver;
   private bool _isRestart;
   private int _score;

   private void Start()
   {
      _isGameOver = false;
      _isRestart = false;
      _score = 0;
      UpdateScore();
      StartCoroutine(SpawnWave());
   }


   private void Update()
   {
      if (_isRestart)
      {
         if (Input.GetKeyDown(KeyCode.R))
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         }
      }
   }

   IEnumerator SpawnWave()
   {
      yield return new WaitForSeconds(_startWait);
      while (true)
      {
         for (int i = 0; i < _hazardCount; i++)
         {
            GameObject hazard = _hazards[Random.Range(0, _hazards.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), _spawnValues.y, _spawnValues.z);
            Quaternion SpawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, SpawnRotation);
            yield return new WaitForSeconds(_spawnWait);
         }

         yield return new WaitForSeconds(_waveWait);
         if (_isGameOver)
         {
            _isRestart = true;
            break;
         }
      }
   }


   public void AddScore(int newScoreValue)
   {
      _score += newScoreValue;
      UpdateScore();
   }

   private void UpdateScore()
   {
      _scoreText.text = "Score: " + _score;
   }

   public void GameOver()
   {
      _isGameOver = true;
   }
}
