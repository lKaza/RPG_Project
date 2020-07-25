using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField]  CursorMapping[] cursorMappings = null;

        private static Ray GetClickRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        void Update()
        {
            RaycastAllSorteds();
            if(InteractWithUI()) {
                SetCursor(CursorType.UI);
                return;
            }
            if(GetComponent<Health>().IsDead()) {
                SetCursor(CursorType.None);
                return;
            }
            if(InteractWithComponent()){
                return;
            }
            
            if(InteractWithMovement()) return;
            SetCursor(CursorType.None);
           

        }

        private bool InteractWithUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }


        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorteds();
            foreach(RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorteds()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetClickRay());
            float[] distances = new float[hits.Length]; 
            for(int i = 0 ; i<hits.Length ; i++){
               
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances,hits);

            return hits;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            
            bool hasHit = Physics.Raycast(GetClickRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                
                GetComponent<Mover>().StartMoveAction(hit.point,1f);
                    
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach(CursorMapping cursor in cursorMappings){
                if(cursor.type == type){
                    return cursor;
                }
            }
           return cursorMappings[0];
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture,mapping.hotspot,CursorMode.Auto);
        }
    }
}
