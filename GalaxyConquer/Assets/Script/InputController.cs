using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit)){
                Base selected = hit.collider.GetComponent<Base>();

                // if(selected.type == Type.PLAYER) 
                GameManager.instance.Deploy(selected);
            }
        }
    }
}
