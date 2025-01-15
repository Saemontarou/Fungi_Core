using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnEnemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ParticleSystem _clickEffect;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left click");
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject effect = Instantiate(_clickEffect.gameObject, transform);
            _clickEffect.Play();
        }
    }
}
