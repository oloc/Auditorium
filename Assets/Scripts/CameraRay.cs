using System;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    [SerializeField]
    private LayerMask _interactableLayerMask;

    [Header("Textures")]
    [SerializeField]
    private Texture2D _moveCursorTexture;
    [SerializeField]
    private Texture2D _resizeCursorTexture;

    private Camera _camera;
    private Transform _currentColliderTransform;
    private bool _isMoving;
    private bool _isResizing;
    private CircleShape _circleShape;

    private const string EFFECTOR_CENTER_TAG_NAME = "EffectorCenter";
    private const string EFFECTOR_EDGE_TAG_NAME = "EffectorEdge";

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = CastRay();

            // Start Interaction
            if (collider != null)
            {
                _currentColliderTransform = collider.transform;
                if (_currentColliderTransform.CompareTag(EFFECTOR_CENTER_TAG_NAME))
                {
                    _isMoving = true;
                }
                else if (_currentColliderTransform.CompareTag(EFFECTOR_EDGE_TAG_NAME))
                {
                    _isResizing = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Stop Interaction
            _isMoving = false;
            _isResizing = false;
            _currentColliderTransform = null;
        }

        if (_isMoving)
        {
            DoMove(_currentColliderTransform);
        }
        if (_isResizing)
        {
            DoResize(_currentColliderTransform);
        }
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private Collider2D CastRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _interactableLayerMask);

        if (hit.collider != null)
        {
            _currentColliderTransform = hit.collider.transform;

            if (hit.collider.CompareTag(EFFECTOR_CENTER_TAG_NAME))
            {
                // move
                ChangeCursorTexture(_moveCursorTexture);
            }
            else if (hit.collider.CompareTag(EFFECTOR_EDGE_TAG_NAME))
            {
                // resize
                ChangeCursorTexture(_resizeCursorTexture);
            }
        }
        else
        {
            if (!_isMoving && _isResizing)
            {
                _currentColliderTransform = null;
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        return hit.collider;
    }

    private void ChangeCursorTexture(Texture2D texture)
    {
        Vector2 hotspot = new(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, hotspot, CursorMode.ForceSoftware);
    }

    private void DoMove(Transform transform)
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        try
        {
            transform.parent.position = mousePosition;
        }
        catch (Exception err)
        {
            Debug.LogError($"Transform: {transform}, error: {err}");
        }
    }

    private void DoResize(Transform transform)
    {


        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 transformPosition = transform.position;
        float distance = (mousePosition - transformPosition).magnitude;
        if (distance > 0.5f)
        {
            try
            {
                _circleShape = transform.GetComponent<CircleShape>();
                _circleShape.Radius = Mathf.Clamp(Vector2.Distance(transformPosition, mousePosition), 0.65f, 5f);
            }
            catch (Exception err)
            {
                Debug.LogError($"Transform: {transform}, error: {err}");
            }
        }
    }
}