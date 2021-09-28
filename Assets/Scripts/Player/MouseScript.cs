using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorTextureEnemy;
    private CursorMode _mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    public GameObject mousePointPrefab;

    private void Update()
    {
        CursorChanger();
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 lastPos = hit.point;
                    lastPos.y = 0.03f;
                    Instantiate(mousePointPrefab, lastPos, Quaternion.identity);
                }
            }
        }
    }

    private void CursorChanger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                Cursor.SetCursor(cursorTextureEnemy, hotSpot, _mode);
            }
            else
            {
                Cursor.SetCursor(cursorTexture, hotSpot, _mode);
            }
        }
    }
}