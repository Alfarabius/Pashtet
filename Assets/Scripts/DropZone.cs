using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] private Vector2 position;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    
    public void OnDrop(Draggable draggable)
    {
        RectTransform uiRectTransform = draggable.GetUiObject().GetComponent<RectTransform>();
        audioSource.PlayOneShot(clickSound);
        uiRectTransform.anchoredPosition = position;
        Destroy(draggable.gameObject);
        gameObject.SetActive(false);
    }
}

