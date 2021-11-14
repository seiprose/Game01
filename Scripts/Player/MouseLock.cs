using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    void Update()
    {
        LockCursor();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
