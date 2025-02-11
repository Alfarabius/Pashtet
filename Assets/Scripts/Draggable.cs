using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 _offset;
    private Camera _mainCamera;
    [SerializeField] private DropZone dropZone;
    private Vector3 _startPosition;
    
    [SerializeField] private GameObject uiObject;

    public GameObject GetUiObject() => uiObject;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        _startPosition = transform.position;
        _offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + _offset;
    }

    private void OnMouseUp()
    {
        if (dropZone != null)
        {
            dropZone.OnDrop(this);
        }
        else
        {
            transform.position = _startPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        return _mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {
            dropZone = other.GetComponent<DropZone>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropZone"))
        {

            if (dropZone != other.GetComponent<DropZone>())
                return;
            
            dropZone = null;
        }
    }
}
