using System;

namespace Entitas.VisualDebugging.Unity {

    [Flags]
    public enum SystemInterfaceFlags {
        None                = 0,
        IInitializeSystem   = 1 << 1,
        IExecuteSystem      = 1 << 2,
        IFixedExecuteSystem = 1 << 3,
        ICleanupSystem      = 1 << 4,
        IFixedCleanupSystem = 1 << 5,
        ITearDownSystem     = 1 << 6,
        IReactiveSystem     = 1 << 7,
        IRestartSystem      = 1 << 8,
    }

    public class SystemInfo {

        public ISystem system { get { return _system; } }
        public string systemName { get { return _systemName; } }

        public bool isInitializeSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.IInitializeSystem) == SystemInterfaceFlags.IInitializeSystem; }
        }

        public bool isExecuteSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.IExecuteSystem) == SystemInterfaceFlags.IExecuteSystem; }
        }

        public bool isFixedExecuteSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.IFixedExecuteSystem) == SystemInterfaceFlags.IFixedExecuteSystem; }
        }

        public bool isCleanupSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.ICleanupSystem) == SystemInterfaceFlags.ICleanupSystem; }
        }

        public bool isFixedCleanupSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.IFixedCleanupSystem) == SystemInterfaceFlags.IFixedCleanupSystem; }
        }

        public bool isTearDownSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.ITearDownSystem) == SystemInterfaceFlags.ITearDownSystem; }
        }

        public bool isRestartSystems
        {
            get { return (_interfaceFlags & SystemInterfaceFlags.IRestartSystem) == SystemInterfaceFlags.IRestartSystem; }
        }

        public bool isReactiveSystems {
            get { return (_interfaceFlags & SystemInterfaceFlags.IReactiveSystem) == SystemInterfaceFlags.IReactiveSystem; }
        }

        public double initializationDuration { get; set; }

        public double accumulatedExecutionDuration { get { return _accumulatedExecutionDuration; } }
        public double minExecutionDuration { get { return _minExecutionDuration; } }
        public double maxExecutionDuration { get { return _maxExecutionDuration; } }
        public double averageExecutionDuration {
            get { return _executionDurationsCount == 0 ? 0 : _accumulatedExecutionDuration / _executionDurationsCount; }
        }

        public double accumulatedFixedExecutionDuration { get { return _accumulatedFixedExecutionDuration; } }
        public double minFixedExecutionDuration { get { return _minFixedExecutionDuration; } }
        public double maxFixedExecutionDuration { get { return _maxFixedExecutionDuration; } }
        public double averageFixedExecutionDuration {
            get { return _fixedExecutionDurationsCount == 0 ? 0 : _accumulatedFixedExecutionDuration / _fixedExecutionDurationsCount; }
        }

        public double accumulatedCleanupDuration { get { return _accumulatedCleanupDuration; } }
        public double minCleanupDuration { get { return _minCleanupDuration; } }
        public double maxCleanupDuration { get { return _maxCleanupDuration; } }
        public double averageCleanupDuration {
            get { return _cleanupDurationsCount == 0 ? 0 : _accumulatedCleanupDuration / _cleanupDurationsCount; }
        }

        public double accumulatedFixedCleanupDuration { get { return _accumulatedFixedCleanupDuration; } }
        public double minFixedCleanupDuration { get { return _minFixedCleanupDuration; } }
        public double maxFixedCleanupDuration { get { return _maxFixedCleanupDuration; } }
        public double averageFixedCleanupDuration {
            get { return _fixedCleanupDurationsCount == 0 ? 0 : _accumulatedFixedCleanupDuration / _fixedCleanupDurationsCount; }
        }

        public double cleanupDuration { get; set; }
        public double teardownDuration { get; set; }

        public bool areAllParentsActive {
            get { return parentSystemInfo == null || (parentSystemInfo.isActive && parentSystemInfo.areAllParentsActive); }
        }

        public SystemInfo parentSystemInfo;
        public bool isActive;

        readonly ISystem _system;
        readonly SystemInterfaceFlags _interfaceFlags;
        readonly string _systemName;

        double _accumulatedExecutionDuration;
        double _minExecutionDuration;
        double _maxExecutionDuration;
        int _executionDurationsCount;

        double _accumulatedFixedExecutionDuration;
        double _minFixedExecutionDuration;
        double _maxFixedExecutionDuration;
        int _fixedExecutionDurationsCount;

        double _accumulatedCleanupDuration;
        double _minCleanupDuration;
        double _maxCleanupDuration;
        int _cleanupDurationsCount;

        double _accumulatedFixedCleanupDuration;
        double _minFixedCleanupDuration;
        double _maxFixedCleanupDuration;
        int _fixedCleanupDurationsCount;

        const string SYSTEM_SUFFIX = "System";

        public SystemInfo(ISystem system) {
            _system = system;
            _interfaceFlags = getInterfaceFlags(system);

            var debugSystem = system as DebugSystems;
            if (debugSystem != null) {
                _systemName = debugSystem.name;
            } else {
                var systemType = system.GetType();
                _systemName = systemType.Name.EndsWith(SYSTEM_SUFFIX, StringComparison.Ordinal)
                    ? systemType.Name.Substring(0, systemType.Name.Length - SYSTEM_SUFFIX.Length)
                    : systemType.Name;
            }

            isActive = true;
        }

        public void AddExecutionDuration(double executionDuration) {
            if (executionDuration < _minExecutionDuration || _minExecutionDuration == 0) {
                _minExecutionDuration = executionDuration;
            }
            if (executionDuration > _maxExecutionDuration) {
                _maxExecutionDuration = executionDuration;
            }

            _accumulatedExecutionDuration += executionDuration;
            _executionDurationsCount += 1;
        }

        public void AddFixedExecutionDuration(double fixedExecutionDuration) {
            if (fixedExecutionDuration < _minFixedExecutionDuration || _minFixedExecutionDuration == 0)
            {
                _minFixedExecutionDuration = fixedExecutionDuration;
            }
            if (fixedExecutionDuration > _maxFixedExecutionDuration)
            {
                _maxFixedExecutionDuration = fixedExecutionDuration;
            }

            _accumulatedFixedExecutionDuration += fixedExecutionDuration;
            _fixedExecutionDurationsCount += 1;
        }

        public void AddCleanupDuration(double cleanupDuration) {
            if (cleanupDuration < _minCleanupDuration || _minCleanupDuration == 0) {
                _minCleanupDuration = cleanupDuration;
            }
            if (cleanupDuration > _maxCleanupDuration) {
                _maxCleanupDuration = cleanupDuration;
            }

            _accumulatedCleanupDuration += cleanupDuration;
            _cleanupDurationsCount += 1;
        }

        public void AddFixedCleanupDuration(double fixedCleanupDuration) {
            if (fixedCleanupDuration < _minFixedCleanupDuration || _minFixedCleanupDuration == 0) {
                _minFixedCleanupDuration = fixedCleanupDuration;
            }
            if (fixedCleanupDuration > _maxFixedCleanupDuration) {
                _maxFixedCleanupDuration = fixedCleanupDuration;
            }

            _accumulatedFixedCleanupDuration += fixedCleanupDuration;
            _fixedCleanupDurationsCount += 1;
        }

        public void ResetDurations() {
            _accumulatedExecutionDuration = 0;
            _executionDurationsCount = 0;

            _accumulatedFixedExecutionDuration = 0;
            _fixedExecutionDurationsCount = 0;

            _accumulatedCleanupDuration = 0;
            _cleanupDurationsCount = 0;

            _accumulatedFixedCleanupDuration = 0;
            _fixedCleanupDurationsCount = 0;
        }

        static SystemInterfaceFlags getInterfaceFlags(ISystem system) {
            var flags = SystemInterfaceFlags.None;
            if (system is IInitializeSystem) {
                flags |= SystemInterfaceFlags.IInitializeSystem;
            }
            if (system is IReactiveSystem) {
                flags |= SystemInterfaceFlags.IReactiveSystem;
            } else if (system is IExecuteSystem) {
                flags |= SystemInterfaceFlags.IExecuteSystem;
            }
            if (system is IFixedExecuteSystem) {
                flags |= SystemInterfaceFlags.IFixedExecuteSystem;
            }
            if (system is ICleanupSystem) {
                flags |= SystemInterfaceFlags.ICleanupSystem;
            }
            if (system is IFixedCleanupSystem) {
                flags |= SystemInterfaceFlags.IFixedCleanupSystem;
            }
            if (system is ITearDownSystem) {
                flags |= SystemInterfaceFlags.ITearDownSystem;
            }
            if (system is IRestartSystem)
            {
                flags |= SystemInterfaceFlags.IRestartSystem;
            }

            return flags;
        }
    }
}
