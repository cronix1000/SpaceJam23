using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMouseRotation : MonoBehaviour
{
    public Camera cam;
    public float maxLength;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 dir;
    private Vector3 mousePos;
    private float angle;
    private Quaternion rot;
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    public void Look() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {


        mousePos = Input.mousePosition;
        mousePos.z = 0;
        pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - pos.x;
        mousePos.y = mousePos.y - pos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //angle
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-180));
    }
    //void OnApplicationFocus(bool hasFocus)
    //{
    //    if (hasFocus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Debug.Log("Application is focussed");
    //    }
    //    else
    //    {
    //        Debug.Log("Application lost focus");
    //    }
    //}

    void Update()
    {
        //if (cam != null) {
        //    RaycastHit hit;
        //    var mousePos = Input.mousePosition;
        //    rayMouse = cam.ScreenPointToRay(mousePos);
        //    if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
        //    {
        //        RotateToMouseDirection(gameObject, hit.point);
        //    }
        //    else {
        //        var pos = rayMouse.GetPoint(maxLength);
        //        RotateToMouseDirection(gameObject,pos);
        //    }
        //}

        Look();
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        dir = destination - obj.transform.position;
        rot = Quaternion.LookRotation(dir);

        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rot, 1);
    }

    public Quaternion GetRotation()
    {
        return rot;
    }
}

