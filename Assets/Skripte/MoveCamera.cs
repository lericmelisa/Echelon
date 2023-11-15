using UnityEngine;

namespace Game.Movement
{
    public class MoveCamera : MonoBehaviour
    {

        public Transform cameraPosition;

        private void Update()
        {
            if(!EscapeMenu.isPaused)
                transform.position = cameraPosition.position;


        }
    }
}
