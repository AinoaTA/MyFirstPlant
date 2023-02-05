using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class CameraManager : MonoBehaviour
    {
        [System.Serializable]
        public struct Cameras
        {
            public int ID;
            public string name;
            public Camera cam;
        }

        [SerializeField] Cameras[] _cameras;

        public void ChooseCam(string name, bool fades = false)
        {
            StartCoroutine(RoutineName(name, fades));
        }

        public void ChooseCam(int id, bool fades = false)
        {
            StartCoroutine(RoutineID(id, fades));
        }

        public void FinalFade()
        {
            Main.instance.fade.Show();
        }

        public void HideFade()
        {
            Main.instance.fade.Hide();
        }

        #region routines

        IEnumerator RoutineID(int id, bool fades = false)
        {
            if (fades)
            {
                Main.instance.fade.Show();
                yield return new WaitForSeconds(1);
            }

            for (int i = 0; i < _cameras.Length; i++)
                _cameras[i].cam.gameObject.SetActive(_cameras[i].ID == id);

            if (fades)
            {
                Main.instance.fade.Hide();
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator RoutineName(string id, bool fades = false)
        {
            if (fades)
            {
                Main.instance.fade.Show();
                yield return new WaitForSeconds(1);
            }

            for (int i = 0; i < _cameras.Length; i++)
                _cameras[i].cam.gameObject.SetActive(_cameras[i].name == id);

            if (fades)
            {
                Main.instance.fade.Hide();
                yield return new WaitForSeconds(1);
            }
        }
        
        #endregion
    }
}