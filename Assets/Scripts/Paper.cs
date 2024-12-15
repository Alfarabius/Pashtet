using UnityEngine;
using DG.Tweening;

public class Paper : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float liftDistance;
    [SerializeField] private float liftDuration = 0.5f;
    
    [SerializeField] private Sprite ruSprite;
    [SerializeField] private Sprite enSprite;
    [SerializeField] private Sprite finalSprite;
    
    private Vector3 _originalPosition;
    private bool _isLifted = false;
    [SerializeField] private Transform target;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _originalPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnLanguageChanged(Localization.Languages language)
    {
        _spriteRenderer.sprite = language switch
        {
            Localization.Languages.English => enSprite,
            Localization.Languages.Russian => ruSprite,
            _ => _spriteRenderer.sprite
        };
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

    public void OnGameEnd()
    {
        _spriteRenderer.sprite = finalSprite;
        LiftPaper();
    }

    private void LiftPaper()
    {
        _isLifted = true;
        transform.DOMove(target.position , liftDuration).OnComplete(() =>
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
