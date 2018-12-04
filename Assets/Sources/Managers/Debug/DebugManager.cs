using GameUtil;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DebugManager : MonoSingleton<DebugManager>
    {
        public override void Init()
        {
        }

        List<Vector2> gestureDetector = new List<Vector2>();
        int gestureCount = 0;
        int numOfCircleToShow = 2;
        bool IsGestureDone()
        {
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touches.Length != 1)
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                }
                else
                {
                    if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                        gestureDetector.Clear();
                    else if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        Vector2 p = Input.touches[0].position;
                        if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                            gestureDetector.Add(p);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        Vector2 p = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                            gestureDetector.Add(p);
                    }
                }
            }

            if (gestureDetector.Count < 10)
                return false;

            Vector2 prevDelta = Vector2.zero;
            float girty = 0, dia = 0;
            Vector2 diap = Vector2.zero;
            for (int i = 0; i < gestureDetector.Count - 2; i++)
            {

                Vector2 delta = gestureDetector[i + 1] - gestureDetector[i];
                girty += delta.magnitude;

                float dot = Vector2.Dot(delta, prevDelta);
                if (dot < 0f)
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                    return false;
                }
                prevDelta = delta;

                var temp = (gestureDetector[i + 1] - gestureDetector[0]).magnitude;
                if(temp > dia)
                {
                    dia = temp;
                    diap = (gestureDetector[i + 1] + gestureDetector[0]) / 2;
                }
                if (girty > dia * 3)
                {
                    var center = new Vector2(Screen.width / 2, Screen.height / 2);
                    if (dia > (Screen.width + Screen.height) / 4 && (center - diap).magnitude < 200)
                    {
                        gestureDetector.Clear();
                        gestureCount++;
                        break;
                    }
                    else
                    {
                        gestureDetector.Clear();
                        break;
                    }
                }
            }
            if (gestureCount >= numOfCircleToShow)
            {
                gestureDetector.Clear();
                gestureCount = 0;
                return true;
            }
            return false;
        }

        public void Update()
        {
//             if (DataManager.Instance.gameCfg.Data.opengm)
//             {
//                 if (IsGestureDone())
//                 {
//                     if (UIManager.Instance.GetPage<GMPage>() == null)
//                         UIManager.Instance.OpenPage<GMPage>();
//                 }
// 
//                 if (Input.GetKeyUp(KeyCode.Tab))
//                 {
//                     if (UIManager.Instance.GetPage<GMPage>() == null)
//                         UIManager.Instance.OpenPage<GMPage>();
//                     else
//                         UIManager.Instance.ClosePage<GMPage>();
//                 }
//             }
        }
    }
}
