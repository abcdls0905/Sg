using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity {

    public enum AvgResetInterval {
        Always = 1,
        VeryFast = 30,
        Fast = 60,
        Normal = 120,
        Slow = 300,
        Never = int.MaxValue
    }

    public class DebugSystems : Systems
    {

        public static AvgResetInterval avgResetInterval = AvgResetInterval.Never;

        public int totalInitializeSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _initializeSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalInitializeSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalExecuteSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _executeSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalExecuteSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalFixedExecuteSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _fixedExecuteSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalFixedExecuteSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalCleanupSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _cleanupSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalCleanupSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalFixedCleanupSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _fixedCleanupSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalFixedCleanupSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalTearDownSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _tearDownSystems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalTearDownSystemsCount : 1;
                }
                return total;
            }
        }

        public int totalSystemsCount
        {
            get
            {
                var total = 0;
                foreach (var system in _systems)
                {
                    var debugSystems = system as DebugSystems;
                    total += debugSystems != null ? debugSystems.totalSystemsCount : 1;
                }
                return total;
            }
        }

        public int initializeSystemsCount { get { return _initializeSystems.Count; } }
        public int executeSystemsCount { get { return _executeSystems.Count; } }
        public int fixedExecuteSystemsCount { get { return _fixedExecuteSystems.Count; } }
        public int cleanupSystemsCount { get { return _cleanupSystems.Count; } }
        public int fixedCleanupSystemsCount { get { return _fixedCleanupSystems.Count; } }
        public int tearDownSystemsCount { get { return _tearDownSystems.Count; } }

        public string name { get { return _name; } }
        public GameObject gameObject { get { return _gameObject; } }
        public SystemInfo systemInfo { get { return _systemInfo; } }

        public double executeDuration { get { return _executeDuration; } }
        public double fixedExecuteDuration { get { return _fixedExecuteDuration; } }
        public double cleanupDuration { get { return _cleanupDuration; } }
        public double fixedCleanupDuration { get { return _fixedCleanupDuration; } }

        public SystemInfo[] initializeSystemInfos { get { return _initializeSystemInfos.ToArray(); } }
        public SystemInfo[] executeSystemInfos { get { return _executeSystemInfos.ToArray(); } }
        public SystemInfo[] fixedExecuteSystemInfos { get { return _fixedExecuteSystemInfos.ToArray(); } }
        public SystemInfo[] cleanupSystemInfos { get { return _cleanupSystemInfos.ToArray(); } }
        public SystemInfo[] fixedCleanupSystemInfos { get { return _fixedCleanupSystemInfos.ToArray(); } }
        public SystemInfo[] tearDownSystemInfos { get { return _tearDownSystemInfos.ToArray(); } }

        public bool paused;

        string _name;

        List<ISystem> _systems;
        GameObject _gameObject;
        SystemInfo _systemInfo;

        List<SystemInfo> _initializeSystemInfos;
        List<SystemInfo> _executeSystemInfos;
        List<SystemInfo> _fixedExecuteSystemInfos;
        List<SystemInfo> _cleanupSystemInfos;
        List<SystemInfo> _fixedCleanupSystemInfos;
        List<SystemInfo> _tearDownSystemInfos;
        List<SystemInfo> _restartSystemInfos;

        Stopwatch _stopwatch;

        double _executeDuration;
        double _fixedExecuteDuration;
        double _cleanupDuration;
        double _fixedCleanupDuration;

        public DebugSystems(string name)
        {
            initialize(name);
        }

        protected DebugSystems(bool noInit)
        {
        }

        protected void initialize(string name)
        {
            _name = name;
            _gameObject = new GameObject(name);
            GameObject.DontDestroyOnLoad(_gameObject);
            _gameObject.AddComponent<DebugSystemsBehaviour>().Init(this);

            _systemInfo = new SystemInfo(this);

            _systems = new List<ISystem>();
            _initializeSystemInfos = new List<SystemInfo>();
            _executeSystemInfos = new List<SystemInfo>();
            _fixedExecuteSystemInfos = new List<SystemInfo>();
            _cleanupSystemInfos = new List<SystemInfo>();
            _fixedCleanupSystemInfos = new List<SystemInfo>();
            _tearDownSystemInfos = new List<SystemInfo>();
            _restartSystemInfos = new List<SystemInfo>();

            _stopwatch = new Stopwatch();
        }

        public override Systems Add(ISystem system)
        {
            _systems.Add(system);

            SystemInfo childSystemInfo;

            var debugSystems = system as DebugSystems;
            if (debugSystems != null)
            {
                childSystemInfo = debugSystems.systemInfo;
                debugSystems.gameObject.transform.SetParent(_gameObject.transform, false);
            }
            else
            {
                childSystemInfo = new SystemInfo(system);
            }

            childSystemInfo.parentSystemInfo = _systemInfo;

            if (childSystemInfo.isInitializeSystems)
            {
                _initializeSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isExecuteSystems)
            {
                _executeSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isFixedExecuteSystems)
            {
                _fixedExecuteSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isCleanupSystems)
            {
                _cleanupSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isFixedCleanupSystems)
            {
                _fixedCleanupSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isTearDownSystems)
            {
                _tearDownSystemInfos.Add(childSystemInfo);
            }
            if (childSystemInfo.isRestartSystems)
            {
                _restartSystemInfos.Add(childSystemInfo);
            }
            return base.Add(system);
        }

        public void ResetDurations()
        {
            foreach (var systemInfo in _executeSystemInfos)
            {
                systemInfo.ResetDurations();
            }
            foreach (var systemInfo in _fixedExecuteSystemInfos)
            {
                systemInfo.ResetDurations();
            }
            foreach (var systemInfo in _cleanupSystemInfos)
            {
                systemInfo.ResetDurations();
            }
            foreach (var systemInfo in _fixedCleanupSystemInfos)
            {
                systemInfo.ResetDurations();
            }

            foreach (var system in _systems)
            {
                var debugSystems = system as DebugSystems;
                if (debugSystems != null)
                {
                    debugSystems.ResetDurations();
                }
            }
        }

        public override void Initialize()
        {
            for (int i = 0; i < _initializeSystems.Count; i++)
            {
                var systemInfo = _initializeSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _initializeSystems[i].Initialize();
                    _stopwatch.Stop();
                    systemInfo.initializationDuration = _stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }

        public override void Execute()
        {
            if (!paused)
            {
                StepExecute();
            }
        }

        public override void FixedExecute()
        {
            if (!paused)
            {
                StepFixedExecute();
            }
        }

        public override void Cleanup()
        {
            if (!paused)
            {
                StepCleanup();
            }
        }

        public override void FixedCleanup()
        {
            if (!paused)
            {
                StepFixedCleanup();
            }
        }

        public override void TearDown()
        {
            if (!paused)
            {
                StepTearDown();
            }
        }

        public override void Restart()
        {
            if (!paused)
            {
                StepRestart();
            }
        }

        public void StepExecute()
        {
            _executeDuration = 0;
            if (Time.frameCount % (int)avgResetInterval == 0)
            {
                ResetDurations();
            }
            for (int i = 0; i < _executeSystems.Count; i++)
            {
                var systemInfo = _executeSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _executeSystems[i].Execute();
                    _stopwatch.Stop();
                    var duration = _stopwatch.Elapsed.TotalMilliseconds;
                    _executeDuration += duration;
                    systemInfo.AddExecutionDuration(duration);
                }
            }
        }
        public void StepFixedExecute()
        {
            _fixedExecuteDuration = 0;
            for (int i = 0; i < _fixedExecuteSystems.Count; i++)
            {
                var systemInfo = _fixedExecuteSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _fixedExecuteSystems[i].FixedExecute();
                    _stopwatch.Stop();
                    var duration = _stopwatch.Elapsed.TotalMilliseconds;
                    _fixedExecuteDuration += duration;
                    systemInfo.AddFixedExecutionDuration(duration);
                }
            }
        }

        public void StepCleanup()
        {
            _cleanupDuration = 0;
            for (int i = 0; i < _cleanupSystems.Count; i++)
            {
                var systemInfo = _cleanupSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _cleanupSystems[i].Cleanup();
                    _stopwatch.Stop();
                    var duration = _stopwatch.Elapsed.TotalMilliseconds;
                    _cleanupDuration += duration;
                    systemInfo.AddCleanupDuration(duration);
                }
            }
        }

        public void StepFixedCleanup()
        {
            _fixedCleanupDuration = 0;
            for (int i = 0; i < _fixedCleanupSystems.Count; i++)
            {
                var systemInfo = _fixedCleanupSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _fixedCleanupSystems[i].FixedCleanup();
                    _stopwatch.Stop();
                    var duration = _stopwatch.Elapsed.TotalMilliseconds;
                    _fixedCleanupDuration += duration;
                    systemInfo.AddFixedCleanupDuration(duration);
                }
            }
        }

        public void StepTearDown()
        {
            for (int i = 0; i < _tearDownSystems.Count; i++)
            {
                var systemInfo = _tearDownSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _tearDownSystems[i].TearDown();
                    _stopwatch.Stop();
                }
            }
            if (_gameObject != null)
                GameObject.Destroy(_gameObject);
        }

        public void StepRestart()
        {
            for (int i = 0; i < _restartSystems.Count; i++)
            {
                var systemInfo = _restartSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _restartSystems[i].Restart();
                    _stopwatch.Stop();
                }
            }
        }
    }
}
