using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attempt1
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        GameObject interact;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckForInteraction();
        }

        void CheckForInteraction()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    interact = hit.collider.gameObject;
                }
            }
        }

    }
}

