using UnityEngine;
using GameUtil;
using System.Collections.Generic;

namespace Game
{
    public class AudioServer
    {
        public GameObject gameObject;
        public AudioSource audioSource;
        public string audioName;
    }
    public class AudioManager : MonoSingleton<AudioManager>
    {
        List<AudioServer> playList = new List<AudioServer>();
        List<AudioServer> cacheList = new List<AudioServer>();
        Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();
        GameObject audioObjRoot = null;
        string[] audioRes = new string[]
            {
                "Audio/ui",
                "Audio/boxrot",
                "Audio/collect",
                "Audio/boom",
                "Audio/bgm",
                "Audio/playstart",
                "Audio/boxborn",
            };

        public override void Init()
        {
            audioObjRoot = new GameObject("AudioObjRoot");
            UnityEngine.Object.DontDestroyOnLoad(audioObjRoot);
        }

        public void PreLoadAudio()
        {
            for (int i = 0; i < audioRes.Length; i++)
            {
                LoadAudio(audioRes[i]);
            }
        }

        public void PlayAudio(string audioName, GameObject actorObj, bool isLoop = false)
        {
            AudioServer audioServer = GetAudioServer();
            audioServer.audioName = audioName;

            playList.Add(audioServer);
            audioServer.gameObject.transform.position = actorObj.transform.position;

            audioServer.audioSource.clip = LoadAudio(audioName);
            audioServer.audioSource.playOnAwake = false;
            audioServer.audioSource.loop = isLoop;
            audioServer.audioSource.Play();
        }

        public AudioClip LoadAudio(string audioName)
        {
            AudioClip audio;
            if (!clipCache.TryGetValue(audioName, out audio))
            {
                audio = ResourceManager.Instance.GetResource(audioName, typeof(AudioClip), AkResourceType.GameAudio).m_content as AudioClip;
                clipCache[audioName] = audio;
            }
            return audio;
        }

        AudioServer GetAudioServer()
        {
            AudioServer server = null;
            if (cacheList.Count > 0)
            {
                server = cacheList[0];
                cacheList.RemoveAt(0);
            }
            else
            {
                server = new AudioServer();
                server.gameObject = new GameObject("AudioObj");
                server.gameObject.transform.parent = audioObjRoot.transform;
                server.audioSource = server.gameObject.AddComponent<AudioSource>();
            }
            return server;
        }

        void Update()
        {
            UpdateList();
        }

        void UpdateList()
        {
            for (int i = playList.Count - 1; i >= 0; i--)
            {
                AudioServer audioServer = playList[i];
                if (!audioServer.audioSource.isPlaying)
                {
                    playList.RemoveAt(i);
                    cacheList.Add(audioServer);
                }
            }
        }

        public void StopAll()
        {
            for (int i = playList.Count - 1; i >= 0; i--)
            {
                AudioServer audioServer = playList[i];
                audioServer.audioSource.Stop();
                playList.RemoveAt(i);
                cacheList.Add(audioServer);
            }
        }

        public void QuitAudio()
        {
            StopAll();
            clipCache.Clear();
        }

        public void StopAudio(string audioName)
        {
            for (int i = playList.Count - 1; i >= 0; i--)
            {
                AudioServer audioServer = playList[i];
                if (audioServer.audioName == audioName)
                {
                    audioServer.audioSource.Stop();
                    playList.RemoveAt(i);
                    cacheList.Add(audioServer);
                }
            }
        }
    }
}