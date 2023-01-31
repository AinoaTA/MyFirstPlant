using UnityEngine;

namespace Tinder
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [HideInInspector] public Camera cam;

        private void Awake()
        {
            controller = this;
            cam = Camera.main;
        }


        public void Accept()
        {
            print("accepted");
        }
        public void Deny()
        {
            print("Denied");
        }
    }
}