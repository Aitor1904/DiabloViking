using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Interactable focus;

    Camera cam;
    Mover mover;

    bool hasHit;
    RaycastHit hit;

    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    LayerMask whatIfInteractable;
    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            hasHit = Physics.Raycast(ray, out hit, 1000f, whatIsGround);

            if (hasHit)
            {
                mover.Move(hit.point);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            hasHit = Physics.Raycast(ray, out hit, 1000f, whatIfInteractable);
            if (hasHit)
            {
                SetFocus(hit.collider.GetComponent<Interactable>());
                Vector3 direction = (hit.collider.gameObject.transform.position - transform.position).normalized;
                var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f * Time.deltaTime);
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(focus != newFocus && focus != null)
        {
            focus.OnDefocused();
        }

        focus = newFocus;

        if (focus != null)
        {
            focus.OnFocused(gameObject.transform);
        }
    }
    private void LateUpdate()
    {
        if (hasHit)
        {
            mover.HandleTargetPointSprite(hit.point);
        }
    }
}
