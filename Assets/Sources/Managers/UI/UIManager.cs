using UnityEngine;
using FairyGUI;
using GameUtil;
using System.Collections.Generic;
using System;
using DG.Tweening;
using GameLua;
using System.Linq;

namespace Game
{
    public class UICache<T>
    {
        public T val;
        public UICache(T data) { val = data; }
    }

    [XLua.LuaCallCSharp]
    public class UIManager : Singleton<UIManager>
    {
        struct SDelayDelPack
        {
            public string path;
            public float time;
        }
        const int maxCacheNum = 1000;
        const int maxCacheCompNum = 20;

        Dictionary<string, UIPackage> _cachePack = new Dictionary<string, UIPackage>();
        List<SDelayDelPack> _delayPack = new List<SDelayDelPack>();
        Dictionary<Type, UIPage> _openedPages = new Dictionary<Type, UIPage>();
        Dictionary<Type, UIPage> _cachedPages = new Dictionary<Type, UIPage>();
        Dictionary<int, string> _cacheNum = new Dictionary<int, string>();
        Dictionary<string, Queue<GObject>> _cacheObject = new Dictionary<string, Queue<GObject>>();
        Dictionary<string, int> preloadComps;
        public override void Init()
        {
            int x = GameEnvSetting.ResolutionX;
            int y = GameEnvSetting.ResolutionY;
            GRoot.inst.SetContentScaleFactor(x, y);

            for (int i = 0; i < maxCacheNum; ++i)
                _cacheNum.Add(i, i.ToString());

            AddUIPackage("UI/Start");
            AddUIPackage("UI/Common");
            DOTween.defaultEaseType = Ease.Linear;
        }

        public void PreloadUIComponent()
        {
            preloadComps = new Dictionary<string, int> {
                {"MiniMapPoint", 20},
                {"ComPlyActor", 10},
                {"ActorPoint", 10},
                {"CompSignalTower", 2},
                {"CompName", 15},
                {"CompHP", 15},
                {"CompCircle", 10},
                {"ComPickNoticeItem", 15},
                {"CompStateName", 15 },
                {"ComSeparateLine", 20}
            };
            var iter_preload = preloadComps.GetEnumerator();
            while (iter_preload.MoveNext())
            {
                int count = iter_preload.Current.Value;
                string componentName = iter_preload.Current.Key;
                for (int i = 0; i < count; i++)
                {
                    GObject component = UIPackage.CreateObject("Game", componentName);
                    RecycleObject(componentName, component);
                }
            }
        }

        public UIPackage AddUIPackage(string path)
        {
            path = path.ToLower();
            UIPackage package = null;
            if (_cachePack.TryGetValue(path, out package))
            {
                return package;
            }

            if (!GameEnvSetting.EnableAssetBundle)
            {
                package = UIPackage.AddPackage(path, (string name, string extension, System.Type type) => { return Resource.LoadResource(name, extension, type); });
            }
            else
            {
                // AssetBundles 全小写，iOS区分大小写
                AssetBundleInfo abInfo = AssetBundleManager.Instance.LoadAssetBundle(path);
                if (abInfo != null && abInfo.m_AssetBundle != null)
                {
                    package = UIPackage.AddPackage(abInfo.m_AssetBundle, false); // 自己来管AssetBunle删除!!!!!!!!!!!!!, Unity2017_2_f3有bug，需要延迟卸载，否则会产生错误
                    AddPackDelayDel(path);
                }
                //AssetBundleManager.Instance.UnloadAssetBundle(path, false);
            }
            if (package != null)
                _cachePack[path] = package;
            return package;
        }

        public void AddPackDelayDel(string path)
        {
            for (int i = 0; i < _delayPack.Count; ++i)
            {
                if (_delayPack[i].path == path)
                {
                    _delayPack.RemoveAt(i);
                    break;
                }
            }

            _delayPack.Add(new SDelayDelPack()
            {
                time = Time.time + 0.5f,
                path = path
            });
        }

        public void RemoveUIPackage(string path, bool unloadAssets = false)
        {
            path = path.ToLower();

            UIPackage package;
            if (_cachePack.TryGetValue(path, out package))
            {
                UIPackage.RemovePackage(package.id, unloadAssets);
                _cachePack.Remove(path);
            }
        }

        public void LoadUIPackage(bool isEnterBattle)
        {
            if (isEnterBattle)
            {
                //AddUIPackage("UI/Basics");
                AddUIPackage("UI/Game");
                RemoveUIPackage("UI/Start");
            }
            else
            {
                //RemoveUIPackage("UI/Basics");
                RemoveUIPackage("UI/Game");
                AddUIPackage("UI/Start");
            }
        }

        public T GetPage<T>() where T : UIPage
        {
            UIPage page;
            if (!_openedPages.TryGetValue(typeof(T), out page))
            {
                return null;
            }
            return (T)page;
        }

        public bool IsPageOpened<T>() where T : UIPage
        {
            UIPage page;
            if (!_openedPages.TryGetValue(typeof(T), out page))
                return false;
            return true;
        }

        public T OpenPage<T>() where T : UIPage, new()
        {
            return (T)OpenPage(typeof(T));
        }

        public void ClosePage<T>() where T : UIPage
        {
            ClosePage(typeof(T));
        }

        public void PreparePage<T>() where T : UIPage
        {
            PreparePage(typeof(T));
        }

        public void HidePage<T>() where T : UIPage
        {
            var type = typeof(T);
            UIPage page;
            if (!_openedPages.TryGetValue(type, out page))
            {
                return;
            }
            page.visible = false;
        }

        public void ShowPage<T>() where T : UIPage
        {
            var type = typeof(T);
            UIPage page;
            if (!_openedPages.TryGetValue(type, out page))
            {
                return;
            }
            page.visible = true;
        }

        public UIPage OpenPage(Type type)
        {
            UIPage page;
            bool opened = _openedPages.TryGetValue(type, out page);
            if (!opened && !_cachedPages.TryGetValue(type, out page))
                page = (UIPage)Activator.CreateInstance(type);
            if (!opened)
                _openedPages[type] = page;
            if (page != null && !page.isShowing)
                page.Show();
            if (page != null && !page.visible)
                page.visible = true;
            return page;
        }

        public void ClosePage(Type type)
        {
            UIPage page;
            if (!_openedPages.TryGetValue(type, out page))
            {
                return;
            }
            _openedPages.Remove(type);
            page.Hide();
            if (page.needCache)
            {
                if (!_cachedPages.ContainsKey(type))
                    _cachedPages.Add(type, page);
            }
            else
            {
                page.OnDispose();
                page.Dispose();
            }
        }

        public void PreparePage(Type type)
        {
            UIPage page;
            bool opened = _openedPages.TryGetValue(type, out page);
            if (opened || _cachedPages.TryGetValue(type, out page))
                return;
            page = (UIPage)Activator.CreateInstance(type);
            page.Init();
            _cachedPages.Add(type, page);
        }


        // 仅适用于Game Package
        const string packName = "Game";
        public GObject NewObject(string objName)
        {
            Queue<GObject> objs;
            if (!_cacheObject.TryGetValue(objName, out objs))
            {
                objs = new Queue<GObject>();
                _cacheObject.Add(objName, objs);
            }
            GObject obj;
            if (objs.Count <= 0)
            {
                //Debug.Log("create new component " + objName);
                obj = UIPackage.CreateObject(packName, objName);
            }
            else
                obj = objs.Dequeue();
            return obj;
        }

        public void RecycleObject(string objName, GObject obj)
        {
            Queue<GObject> objs;
            if (!_cacheObject.TryGetValue(objName, out objs))
            {
                objs = new Queue<GObject>(maxCacheNum);
                _cacheObject.Add(objName, objs);
            }

            if (objs.Count >= maxCacheCompNum)
            {
                obj.RemoveFromParent();
                obj.Dispose();
            }
            else
            {
                obj.RemoveFromParent();
                objs.Enqueue(obj);
            }
        }

        public void CloseAllPage(bool cache = false)
        {
            if (cache)
            {
                foreach (var iter in _openedPages.ToList())
                {
                    var page = iter.Value;
                    page.Hide();
                    if (_cachedPages.ContainsValue(page))
                        continue;
                    page.OnDispose();
                    page.Dispose();
                    _openedPages.Remove(iter.Key);
                }
                _openedPages.Clear();
            }
            else
            {
                foreach (var iter in _openedPages.ToList())
                {
                    var page = iter.Value;
                    page.Hide();
                    if (!_cachedPages.ContainsValue(page))
                    {
                        page.OnDispose();
                        page.Dispose();
                    }
                    _openedPages.Remove(iter.Key);
                }
                foreach (var iter in _cachedPages.ToList())
                {
                    var page = iter.Value;
                    page.OnDispose();
                    page.Dispose();
                    _cachedPages.Remove(iter.Key);
                }

                _openedPages.Clear();
                _cachedPages.Clear();
            }

            foreach (var iter in _cacheObject)
            {
                var objs = iter.Value;
                foreach (var obj in objs)
                    obj.Dispose();
            }
            _cacheObject.Clear();
        }

        public void Update(float deltaTime)
        {
            foreach (var iter in _openedPages)
            {
                var page = iter.Value;
                page.Update(deltaTime);
            }
            for (int i = _delayPack.Count - 1; i >= 0; --i)
            {
                if (_delayPack[i].time < Time.time)
                {
                    AssetBundleManager.Instance.UnloadAssetBundle(_delayPack[i].path, false);
                    _delayPack.RemoveAt(i);
                }
            }
        }

        public void CloseAllLuaPages()
        {
            GameLua.LuaManager.Instance.PushLuaEvent(GameLua.LuaEventType.LE_CloseAllPage);
        }

        public SpeedString speedStr = new SpeedString(10);
        public string GetCacheNumString(int num, bool bSpecial)
        {
            if (_cacheNum.ContainsKey(num))
                return _cacheNum[num];
            else
            {
                if (bSpecial)
                {
                    speedStr.Clear();
                    speedStr.Append(num);
                    return speedStr.string_base;
                }
                else
                    return num.ToString();
            }
        }

        public static Vector2 WorldPosToUIPos(Vector3 pos, out bool inScreen)
        {
            var camera = Util.MainCamera;
            Vector3 vp = pos;
            if (camera != null)
                vp = camera.WorldToViewportPoint(pos);

            if (vp.x < 0 || vp.x > 1 || vp.y < 0 || vp.y > 1 || vp.z < 0)
            {
                inScreen = false;
            }
            else
            {
                inScreen = true;
            }
            //--Viewport是左下角0，0， 右上角1，1
            //--UI是左上角0，0 右下角1，1
            //--所以需要1.0 - y
            vp.y = (float)1.0 - vp.y;
            float x = vp.x * FairyGUI.GRoot.inst.width;
            float y = vp.y * FairyGUI.GRoot.inst.height;
            return new Vector2(x, y);
        }

        public void MsgTip(string text, float time = 2, int type = 1, bool show = true, float totalTime = 0)
        {
            LuaManager.Instance.PushLuaEvent(LuaEventType.LE_TopTip, text, time, type, show, totalTime);
        }

        public void ShowTip(TipArgs args, int tipType, int tipStyle)
        {
            LuaManager.Instance.PushLuaEvent(LuaEventType.LE_CommonTip, true, tipType, args, tipStyle);
        }

        public void CloseTip()
        {
            LuaManager.Instance.PushLuaEvent(LuaEventType.LE_CommonTip, false);
        }

        public void MoveForwad(Window page)
        {
            page.z -= 500;
        }
    }
}
