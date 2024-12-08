using UnityEngine;
using DG.Tweening;

public class Paper : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float liftDistance;
    [SerializeField] private float liftDuration = 0.5f;

    private Vector3 _originalPosition;
    private bool _isLifted = false;

    private void Awake()
    {
        _originalPosition = transform.position;
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
            LiftPaper();
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void LiftPaper()
    {
        _isLifted = true;
        transform.DOMoveY(transform.position.y + liftDistance, liftDuration).OnComplete(() =>
        {
            
        });
    }

    private void ReturnToOriginalPosition()
    {
        transform.DOMove(_originalPosition, liftDuration).OnComplete(() =>
        {
            _isLifted = false;
        });
    }
}
