using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attempt1
{
    public class GameController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }
        }

        public void Quit()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}

