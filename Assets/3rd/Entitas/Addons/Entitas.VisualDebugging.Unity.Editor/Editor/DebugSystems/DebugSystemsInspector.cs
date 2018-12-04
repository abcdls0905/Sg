using System.Collections.Generic;
using System.Linq;
using Entitas.Unity.Editor;
using Entitas.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor {

    [CustomEditor(typeof(DebugSystemsBehaviour))]
    public class DebugSystemsInspector : UnityEditor.Editor {

        enum SortMethod {
            OrderOfOccurrence,

            Name,
            NameDescending,

            ExecutionTime,
            ExecutionTimeDescending
        }

        SystemsMonitor _systemsMonitor;
        Queue<float> _systemMonitorData;
        const int SYSTEM_MONITOR_DATA_LENGTH = 60;

        static bool _showDetails = false;
        static bool _showSystemsMonitor = true;
        static bool _showSystemsList = true;

        static bool _showInitializeSystems = true;
        static bool _showExecuteSystems = true;
        static bool _showFixedExecuteSystems = true;
        static bool _showCleanupSystems = true;
        static bool _showFixedCleanupSystems = true;
        static bool _showTearDownSystems = true;
        static bool _hideEmptySystems = true;
        static string _systemNameSearchString = string.Empty;

        int _systemWarningThreshold;

        float _threshold;
        SortMethod _systemSortMethod;

        int _lastRenderedFrameCount;

        GUIContent _stepButtonContent;
        GUIContent _pauseButtonContent;

        void OnEnable() {
            var config = new VisualDebuggingConfig();
            config.Configure(Preferences.LoadProperties());
            _systemWarningThreshold = config.systemWarningThreshold;
        }

        public override void OnInspectorGUI() {
            var debugSystemsBehaviour = (DebugSystemsBehaviour)target;
            var systems = debugSystemsBehaviour.systems;

            EditorGUILayout.Space();
            drawSystemsOverview(systems);

            EditorGUILayout.Space();
            drawSystemsMonitor(systems);

            EditorGUILayout.Space();
            drawSystemList(systems);

            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);
        }

        static void drawSystemsOverview(DebugSystems systems) {
            _showDetails = EntitasEditorLayout.DrawSectionHeaderToggle("Details", _showDetails);
            if (_showDetails) {
                EntitasEditorLayout.BeginSectionContent();
                {
                    EditorGUILayout.LabelField(systems.name, EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Initialize Systems", systems.totalInitializeSystemsCount.ToString());
                    EditorGUILayout.LabelField("Execute Systems", systems.totalExecuteSystemsCount.ToString());
                    EditorGUILayout.LabelField("Fixed Execute Systems", systems.totalFixedExecuteSystemsCount.ToString());
                    EditorGUILayout.LabelField("Cleanup Systems", systems.totalCleanupSystemsCount.ToString());
                    EditorGUILayout.LabelField("Fixed Cleanup Systems", systems.totalFixedCleanupSystemsCount.ToString());
                    EditorGUILayout.LabelField("TearDown Systems", systems.totalTearDownSystemsCount.ToString());
                    EditorGUILayout.LabelField("Total Systems", systems.totalSystemsCount.ToString());
                }
                EntitasEditorLayout.EndSectionContent();
            }
        }

        void drawSystemsMonitor(DebugSystems systems) {
            if (_systemsMonitor == null) {
                _systemsMonitor = new SystemsMonitor(SYSTEM_MONITOR_DATA_LENGTH);
                _systemMonitorData = new Queue<float>(new float[SYSTEM_MONITOR_DATA_LENGTH]);
            }

            _showSystemsMonitor = EntitasEditorLayout.DrawSectionHeaderToggle("Performance", _showSystemsMonitor);
            if (_showSystemsMonitor) {
                EntitasEditorLayout.BeginSectionContent();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.LabelField("Execution duration", systems.executeDuration.ToString());
                            EditorGUILayout.LabelField("Fixed Execution duration", systems.fixedExecuteDuration.ToString());
                            EditorGUILayout.LabelField("Cleanup duration", systems.cleanupDuration.ToString());
                            EditorGUILayout.LabelField("Fixed Cleanup duration", systems.fixedCleanupDuration.ToString());
                        }
                        EditorGUILayout.EndVertical();

                        if (_stepButtonContent == null) {
                            _stepButtonContent = EditorGUIUtility.IconContent("StepButton On");
                        }
                        if (_pauseButtonContent == null) {
                            _pauseButtonContent = EditorGUIUtility.IconContent("PauseButton On");
                        }

                        systems.paused = GUILayout.Toggle(systems.paused, _pauseButtonContent, "CommandLeft");

                        if (GUILayout.Button(_stepButtonContent, "CommandRight")) {
                            systems.paused = true;
                            systems.StepExecute();
                            systems.StepCleanup();
                            addDuration((float)systems.executeDuration + (float)systems.cleanupDuration +
                                        (float)systems.fixedExecuteDuration + (float)systems.fixedCleanupDuration);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (!EditorApplication.isPaused && !systems.paused) {
                        addDuration((float)systems.executeDuration + (float)systems.cleanupDuration +
                                    (float)systems.fixedExecuteDuration + (float)systems.fixedCleanupDuration);
                    }
                    _systemsMonitor.Draw(_systemMonitorData.ToArray(), 80f);
                }
                EntitasEditorLayout.EndSectionContent();
            }
        }

        void drawSystemList(DebugSystems systems) {
            _showSystemsList = EntitasEditorLayout.DrawSectionHeaderToggle("Systems", _showSystemsList);
            if (_showSystemsList) {
                EntitasEditorLayout.BeginSectionContent();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        DebugSystems.avgResetInterval = (AvgResetInterval)EditorGUILayout.EnumPopup("Reset average duration Ø", DebugSystems.avgResetInterval);
                        if (GUILayout.Button("Reset Ø now", EditorStyles.miniButton, GUILayout.Width(88))) {
                            systems.ResetDurations();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    _threshold = EditorGUILayout.Slider("Threshold Ø ms", _threshold, 0f, 33f);

                    _hideEmptySystems = EditorGUILayout.Toggle("Hide empty systems", _hideEmptySystems);
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    {
                        _systemSortMethod = (SortMethod)EditorGUILayout.EnumPopup(_systemSortMethod, EditorStyles.popup, GUILayout.Width(150));
                        _systemNameSearchString = EntitasEditorLayout.SearchTextField(_systemNameSearchString);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    _showInitializeSystems = EntitasEditorLayout.DrawSectionHeaderToggle("Initialize Systems", _showInitializeSystems);
                    if (_showInitializeSystems && shouldShowSystems(systems, SystemInterfaceFlags.IInitializeSystem)) {
                        EntitasEditorLayout.BeginSectionContent();
                        {
                            var systemsDrawn = drawSystemInfos(systems, SystemInterfaceFlags.IInitializeSystem);
                            if (systemsDrawn == 0) {
                                EditorGUILayout.LabelField(string.Empty);
                            }
                        }
                        EntitasEditorLayout.EndSectionContent();
                    }

                    _showExecuteSystems = EntitasEditorLayout.DrawSectionHeaderToggle("Execute Systems", _showExecuteSystems);
                    if (_showExecuteSystems && shouldShowSystems(systems, SystemInterfaceFlags.IExecuteSystem)) {
                        EntitasEditorLayout.BeginSectionContent();
                        {
                            var systemsDrawn = drawSystemInfos(systems, SystemInterfaceFlags.IExecuteSystem);
                            if (systemsDrawn == 0) {
                                EditorGUILayout.LabelField(string.Empty);
                            }
                        }
                        EntitasEditorLayout.EndSectionContent();
                    }

                    _showFixedExecuteSystems = EntitasEditorLayout.DrawSectionHeaderToggle("Fixed Execute Systems", _showFixedExecuteSystems);
                    if (_showFixedExecuteSystems && shouldShowSystems(systems, SystemInterfaceFlags.IFixedExecuteSystem)) {
                        EntitasEditorLayout.BeginSectionContent();
                        {
                            var systemsDrawn = drawSystemInfos(systems, SystemInterfaceFlags.IFixedExecuteSystem);
                            if (systemsDrawn == 0)
                            {
                                EditorGUILayout.LabelField(string.Empty);
                            }
                        }
                        EntitasEditorLayout.EndSectionContent();
                    }

                    _showFixedCleanupSystems = EntitasEditorLayout.DrawSectionHeaderToggle("Fixed Cleanup Systems", _showFixedCleanupSystems);
                    if (_showFixedCleanupSystems && shouldShowSystems(systems, SystemInterfaceFlags.IFixedCleanupSystem)) {
                        EntitasEditorLayout.BeginSectionContent();
                        {
                            var systemsDrawn = drawSystemInfos(systems, SystemInterfaceFlags.IFixedCleanupSystem);
                            if (systemsDrawn == 0)
                            {
                                EditorGUILayout.LabelField(string.Empty);
                            }
                        }
                        EntitasEditorLayout.EndSectionContent();
                    }
                }
                EntitasEditorLayout.EndSectionContent();
            }
        }

        int drawSystemInfos(DebugSystems systems, SystemInterfaceFlags type) {
            SystemInfo[] systemInfos = null;

            switch (type) {
                case SystemInterfaceFlags.IInitializeSystem:
                    systemInfos = systems.initializeSystemInfos
                        .Where(systemInfo => systemInfo.initializationDuration >= _threshold)
                        .ToArray();
                    break;
                case SystemInterfaceFlags.IExecuteSystem:
                    systemInfos = systems.executeSystemInfos
                        .Where(systemInfo => systemInfo.averageExecutionDuration >= _threshold)
                        .ToArray();
                    break;
                case SystemInterfaceFlags.IFixedExecuteSystem:
                    systemInfos = systems.fixedExecuteSystemInfos
                        .Where(systemInfo => systemInfo.averageFixedExecutionDuration >= _threshold)
                        .ToArray();
                    break;
                case SystemInterfaceFlags.IFixedCleanupSystem:
                    systemInfos = systems.fixedCleanupSystemInfos
                        .Where(systemInfo => systemInfo.averageFixedCleanupDuration >= _threshold)
                        .ToArray();
                    break;
            }

            systemInfos = getSortedSystemInfos(systemInfos, _systemSortMethod);

            var systemsDrawn = 0;
            foreach (var systemInfo in systemInfos) {
                var debugSystems = systemInfo.system as DebugSystems;
                if (debugSystems != null) {
                    if (!shouldShowSystems(debugSystems, type)) {
                        continue;
                    }
                }

                if (EntitasEditorLayout.MatchesSearchString(systemInfo.systemName.ToLower(), _systemNameSearchString.ToLower())) {
                    EditorGUILayout.BeginHorizontal();
                    {
                        var indent = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0;

                        var wasActive = systemInfo.isActive;
                        if (systemInfo.areAllParentsActive) {
                            systemInfo.isActive = EditorGUILayout.Toggle(systemInfo.isActive, GUILayout.Width(20));
                        } else {
                            EditorGUI.BeginDisabledGroup(true);
                            {
                                EditorGUILayout.Toggle(false, GUILayout.Width(20));
                            }
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUI.indentLevel = indent;

                        if (systemInfo.isActive != wasActive) {
                            var reactiveSystem = systemInfo.system as IReactiveSystem;
                            if (reactiveSystem != null) {
                                if (systemInfo.isActive) {
                                    reactiveSystem.Activate();
                                } else {
                                    reactiveSystem.Deactivate();
                                }
                            }
                        }

                        switch (type) {
                            case SystemInterfaceFlags.IInitializeSystem:
                                EditorGUILayout.LabelField(systemInfo.systemName, systemInfo.initializationDuration.ToString(), getSystemStyle(systemInfo, SystemInterfaceFlags.IInitializeSystem));
                                break;
                            case SystemInterfaceFlags.IExecuteSystem:
                                var avgE = string.Format("Ø {0:00.000}", systemInfo.averageExecutionDuration).PadRight(12);
                                var minE = string.Format("▼ {0:00.000}", systemInfo.minExecutionDuration).PadRight(12);
                                var maxE = string.Format("▲ {0:00.000}", systemInfo.maxExecutionDuration);
                                EditorGUILayout.LabelField(systemInfo.systemName, avgE + minE + maxE, getSystemStyle(systemInfo, SystemInterfaceFlags.IExecuteSystem));
                                break;
                            case SystemInterfaceFlags.IFixedExecuteSystem:
                                var avgFE = string.Format("Ø {0:00.000}", systemInfo.averageFixedExecutionDuration).PadRight(12);
                                var minFE = string.Format("▼ {0:00.000}", systemInfo.minFixedExecutionDuration).PadRight(12);
                                var maxFE = string.Format("▲ {0:00.000}", systemInfo.maxFixedExecutionDuration);
                                EditorGUILayout.LabelField(systemInfo.systemName, avgFE + minFE + maxFE, getSystemStyle(systemInfo, SystemInterfaceFlags.IFixedExecuteSystem));
                                break;
                            case SystemInterfaceFlags.IFixedCleanupSystem:
                                var avgFC = string.Format("Ø {0:00.000}", systemInfo.averageFixedCleanupDuration).PadRight(12);
                                var minFC = string.Format("▼ {0:00.000}", systemInfo.minFixedCleanupDuration).PadRight(12);
                                var maxFC = string.Format("▲ {0:00.000}", systemInfo.maxFixedCleanupDuration);
                                EditorGUILayout.LabelField(systemInfo.systemName, avgFC + minFC + maxFC, getSystemStyle(systemInfo, SystemInterfaceFlags.IFixedCleanupSystem));
                                break;
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    systemsDrawn += 1;
                }

                var debugSystem = systemInfo.system as DebugSystems;
                if (debugSystem != null) {
                    var indent = EditorGUI.indentLevel;
                    EditorGUI.indentLevel += 1;
                    systemsDrawn += drawSystemInfos(debugSystem, type);
                    EditorGUI.indentLevel = indent;
                }
            }

            return systemsDrawn;
        }

        static SystemInfo[] getSortedSystemInfos(SystemInfo[] systemInfos, SortMethod sortMethod) {
            if (sortMethod == SortMethod.Name) {
                return systemInfos
                    .OrderBy(systemInfo => systemInfo.systemName)
                    .ToArray();
            }
            if (sortMethod == SortMethod.NameDescending) {
                return systemInfos
                    .OrderByDescending(systemInfo => systemInfo.systemName)
                    .ToArray();
            }

            if (sortMethod == SortMethod.ExecutionTime) {
                return systemInfos
                    .OrderBy(systemInfo => systemInfo.averageExecutionDuration)
                    .ToArray();
            }
            if (sortMethod == SortMethod.ExecutionTimeDescending) {
                return systemInfos
                    .OrderByDescending(systemInfo => systemInfo.averageExecutionDuration)
                    .ToArray();
            }

            return systemInfos;
        }

        static bool shouldShowSystems(DebugSystems systems, SystemInterfaceFlags type) {
            if (!_hideEmptySystems) {
                return true;
            }

            switch (type) {
                case SystemInterfaceFlags.IInitializeSystem:
                    return systems.totalInitializeSystemsCount > 0;
                case SystemInterfaceFlags.IExecuteSystem:
                    return systems.totalExecuteSystemsCount > 0;
                case SystemInterfaceFlags.IFixedExecuteSystem:
                    return systems.totalFixedExecuteSystemsCount > 0;
                case SystemInterfaceFlags.IFixedCleanupSystem:
                    return systems.totalFixedCleanupSystemsCount > 0;
                default:
                    return true;
            }
        }

        GUIStyle getSystemStyle(SystemInfo systemInfo, SystemInterfaceFlags systemFlag) {
            var style = new GUIStyle(GUI.skin.label);
            var color = EditorGUIUtility.isProSkin
                            ? Color.white
                            : style.normal.textColor;

            if (systemFlag == SystemInterfaceFlags.IExecuteSystem && systemInfo.averageExecutionDuration >= _systemWarningThreshold) {
                color = Color.red;
            }

            if (systemFlag == SystemInterfaceFlags.IFixedExecuteSystem && systemInfo.averageFixedExecutionDuration >= _systemWarningThreshold) {
                color = Color.red;
            }

            if (systemFlag == SystemInterfaceFlags.IFixedCleanupSystem && systemInfo.averageFixedCleanupDuration >= _systemWarningThreshold) {
                color = Color.red;
            }

            style.normal.textColor = color;

            return style;
        }

        void addDuration(float duration) {
            // OnInspectorGUI is called twice per frame - only add duration once
            if (Time.renderedFrameCount != _lastRenderedFrameCount) {
                _lastRenderedFrameCount = Time.renderedFrameCount;

                if (_systemMonitorData.Count >= SYSTEM_MONITOR_DATA_LENGTH) {
                    _systemMonitorData.Dequeue();
                }

                _systemMonitorData.Enqueue(duration);
            }
        }
    }
}
