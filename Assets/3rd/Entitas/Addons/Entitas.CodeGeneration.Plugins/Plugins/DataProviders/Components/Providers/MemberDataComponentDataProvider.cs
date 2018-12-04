﻿using System;
using System.Linq;
using Entitas.Utils;

namespace Entitas.CodeGeneration.Plugins {

    public class MemberDataComponentDataProvider : IComponentDataProvider {

        public void Provide(Type type, ComponentData data) {
            var memberData = type.GetPublicMemberInfos()
                                 .Select(info => new MemberData(
                                     info.type.ToCompilableString(),
                                     info.name,
                                     info.ShouldGenerateMembers()))
                                 .ToArray();

            data.SetMemberData(memberData);
        }
    }

    public static class MemberInfosComponentDataExtension {

        public const string COMPONENT_MEMBER_INFOS = "component_memberInfos";

        public static MemberData[] GetMemberData(this ComponentData data) {
            return (MemberData[])data[COMPONENT_MEMBER_INFOS];
        }

        public static void SetMemberData(this ComponentData data, MemberData[] memberInfos) {
            data[COMPONENT_MEMBER_INFOS] = memberInfos;
        }
    }
}
