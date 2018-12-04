
using System;
using System.Collections.Generic;

namespace GameBuild
{
    public class AssetReferenceNode
    {
        public string AssetPath { get; set; }

        public List<string> depenceOnMe = new List<string>();
        public List<string> depence = new List<string>();
        public List<string> buildAssets = new List<string>();
    }
}