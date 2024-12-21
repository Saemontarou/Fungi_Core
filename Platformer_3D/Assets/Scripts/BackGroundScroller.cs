using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
   [SerializeField] private float _scrollSpeed = 0.1f;
   [SerializeField] private float _backgroundLenght;
   private Vector3 _startPosition;


   private void Start()
   {
      _startPosition = transform.position;
   }

   private void Update()
   {
      float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, _backgroundLenght);
      transform.position = _startPosition + Vector3.up * newPosition;
   }
}
