using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;

    private Vector3 _mouseWorldPosiiton;

    private IClickable _clickable;
    private IClickable _hoverTarget;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        MousePosition();
    }

    private void MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Check the layer of the object hit
            string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            _clickable = hit.collider.GetComponent<IClickable>();

            if (hit.point != Vector3.zero && hitLayerName == "Ground")
            {
                // If we click on the ground and not on a clickable, we set clickable to null
                _mouseWorldPosiiton = hit.point;
                _clickable = null;

                if (_hoverTarget != null)
                {
                    _hoverTarget.OnHoverExit();
                    _hoverTarget = null;
                }
            }

            else if (_clickable != null && _clickable.CanBeClicked())
            {
                if (_hoverTarget != _clickable)
                {
                    if (_hoverTarget != null)
                    {
                        _hoverTarget.OnHoverExit();
                    }

                    _hoverTarget = _clickable;
                    _hoverTarget.OnHover();
                }
            }
            else if (_hoverTarget != null)
            {
                // If no clickable object is hit, reset the hover target
                _hoverTarget.OnHoverExit();
                _hoverTarget = null;
            }
        }
        else if (_hoverTarget != null)
        {
            // If the raycast hits nothing, reset the hover target
            _hoverTarget.OnHoverExit();
            _hoverTarget = null;
        }
    }
    
    public Vector3 GetMousePosition() => _mouseWorldPosiiton;

    public IClickable GetClickable() => _clickable;
}
