using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditor
{
    public class PBXProject : PBXObject
    {
        protected string MAINGROUP_KEY = "mainGroup";
        protected string KNOWN_REGIONS_KEY = "knownRegions";
        protected string TARGETS_KEY = "targets";
        protected string ATTRIBUTES_KEY = "attributes";

        protected bool _clearedLoc = false;
        public PBXProject() : base()
        {
        }

        public PBXProject(string guid, PBXDictionary dictionary) : base(guid, dictionary)
        {
        }

        public string mainGroupID
        {
            get
            {
                return (string)_data[MAINGROUP_KEY];
            }
        }

        public PBXList targets
        {
            get
            {
                return (PBXList)_data[TARGETS_KEY];
            }
        }

        public PBXDictionary attributes
        {
            get
            {
                return (PBXDictionary)_data[ATTRIBUTES_KEY];
            }
        }

    }
}
