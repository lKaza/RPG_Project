using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.EventSystems;
using UnityEngine.AI;

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
        [SerializeField] float navMeshProjection=1f;
        [SerializeField] float maxNavMeshPathLength = 40f;
        [SerializeField] float raycastRadius = 1f;

        private static Ray GetClickRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        void Update()
        {
            
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
            
            if(InteractWithMovement()) {
                return;}
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
            RaycastHit[] hits = Physics.SphereCastAll(GetClickRay(),raycastRadius);
            float[] distances = new float[hits.Length]; 
            for(int i = 0 ; i<hits.Length ; i++){
               
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances,hits);

            return hits;
        }

        private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if(!GetComponent<Mover>().CanMoveTo(target)){
                    return false;
                }
                if (Input.GetMouseButton(0))
                {    
                GetComponent<Mover>().StartMoveAction(target,1f);            
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target){
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetClickRay(), out hit);
            target = new Vector3();
            if(!hasHit) return false;
            NavMeshHit navhit;
            if (!NavMesh.SamplePosition(hit.point, out navhit, navMeshProjection, NavMesh.AllAreas))             
                    return false;     

            target = navhit.position;
            return true;

          
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
