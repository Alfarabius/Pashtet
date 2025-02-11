using DG.Tweening;
using UnityEngine;

public class Box : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float liftDuration = 0.5f;
    [SerializeField] private Transform originalPosition;
    
    private bool _isLifted = false;
    [SerializeField] private Transform target;
    
    private void PlaySound()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    
    private void OnMouseDown()
    {
        PlaySound();
        if (_isLifted)
        {
            ReturnToOriginalPosition();
        }
        else
        {
            LiftBox();
        }
    }

    public void OnGameEvent()
    {
        LiftBox();
    }
    
    private void LiftBox()
    {
        _isLifted = true;
        transform.DOMove(target.position , liftDuration).OnComplete(() =>
        {
            
        });
    }

    private void ReturnToOriginalPosition()
    {
        transform.DOMove(originalPosition.position, liftDuration).OnComplete(() =>
        {
            _isLifted = false;
        });
    }
    
}
