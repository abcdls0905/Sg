using UnityEngine;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.Xml;
#endif
using System.IO;

public static class XCodePostProcess
{
    public static bool isIphone = true;
#if UNITY_EDITOR
    [PostProcessBuild(100)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
        {
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        //得到xcode工程的路径
        string path = Path.GetFullPath(pathToBuiltProject);
        //System.IO.File.Copy(Application.dataPath + "/Engine/3rdParty/XUPorter/Mods/Entitlements.plist", path + "/Entitlements.plist", true);

        // Create a new project object from build target
        XCProject project = new XCProject(pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        //在这里面把frameworks添加在你的xcode工程里面
        string[] files = Directory.GetFiles(Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            project.ApplyMod(file);
        }
        project.overwriteBuildSetting("ARCHS", "arm64");
        project.overwriteBuildSetting("ENABLE_BITCODE", "NO");
        project.overwriteBuildSetting("PRODUCT_NAME", "sugar");
        project.overwriteBuildSetting("CODE_SIGN_ENTITLEMENTS", "Entitlements.plist");
        //增加一个编译标记。。没有的话sharesdk会报错。。
        //project.AddOtherLinkerFlags("-licucore");

        if (isIphone)
        {
            project.overwriteBuildSetting("PROVISIONING_PROFILE", "ffbb5356-e206-474b-8c4c-a99972d05161");
            project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Distribution: Guangzhou Duoyi Network Technology Co,.Ltd.");
        }
        else
        {
            project.overwriteBuildSetting("PRODUCT_BUNDLE_IDENTIFIER", "com.person.popstar");
            project.overwriteBuildSetting("PROVISIONING_PROFILE", "b6eec1e3-b848-4a0d-bd44-bc7ca009e0f2");
            project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Developer: Shaona Guo (DSSZAJ22YS)");
        }
        //设置签名的证书， 第二个参数 你可以设置成你的证书

        //project.
        //project.copyBuildPhases
        // 编辑plist 文件
        EditorPlist(path);
        // 添加火星验证
        AddMarsVerification(project);
        //编辑代码文件
        //EditorCode(path);

        // Finally save the xcode project
        project.Save();
    }

    private static void AddMarsVerification(XCProject xcproject)
    {
        PBXDictionary attributes = (PBXDictionary)xcproject.project.data["attributes"];
        PBXDictionary TargetAttributes = (PBXDictionary)attributes["TargetAttributes"];
        foreach (var item in TargetAttributes)
        {
            PBXDictionary enabled = new PBXDictionary();
            enabled.Add("enabled", "1");
            PBXDictionary keychain = new PBXDictionary();
            keychain.Add("com.apple.Keychain", enabled);
            PBXDictionary systemCapabilities = new PBXDictionary();
            systemCapabilities.Add("SystemCapabilities", keychain);
            PBXDictionary developmentTeam = new PBXDictionary();
            developmentTeam.Add("DevelopmentTeam", "MDDB52YB8L");
            // 设置automatically manage signing
            PBXDictionary autoSetting = new PBXDictionary();
            autoSetting["ProvisioningStyle"] = "Manual";
            PBXDictionary targetAtt = (PBXDictionary)TargetAttributes[item.Key];

            if (targetAtt.ContainsKey("DevelopmentTeam"))
            {
                targetAtt.Remove("DevelopmentTeam");
            }
            if (targetAtt.ContainsKey("SystemCapabilities"))
            {
                targetAtt.Remove("SystemCapabilities");
            }
            if (targetAtt.ContainsKey("ProvisioningStyle"))
            {
                targetAtt.Remove("ProvisioningStyle");
            }
            targetAtt.Append(developmentTeam);
            targetAtt.Append(systemCapabilities);
            targetAtt.Append(autoSetting);
        }
    }
    private static void EditorPlist(string filePath)
    {

        XCPlist list = new XCPlist(filePath);
        string bundle = "com.duoyient.i100test05";
        string PlistAdd =
        @"<key>LSApplicationQueriesSchemes</key>
        <array>
        <string>calltoimccQYB</string>
        </array>";
        string FileSharingAdd =
        @"<key>UIFileSharingEnabled</key>
        <true/>";

        //在plist里面增加一行
        list.AddKey(PlistAdd);
        // 增加文件共享
        list.AddKey(FileSharingAdd);
        //在plist里面替换一行
        list.ReplaceKey("<string>com.duoyient.${PRODUCT_NAME}</string>", "<string>" + bundle + "</string>");
        //保存
        list.Save();

    }

    private static void EditorCode(string filePath)
    {
        //读取UnityAppController.mm文件
        XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        //在指定代码后面增加一行代码
        UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"", "#import <ShareSDK/ShareSDK.h>");

        //在指定代码中替换一行
        UnityAppController.Replace("return YES;", "return [ShareSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation wxDelegate:nil];");

        //在指定代码后面增加一行
        UnityAppController.WriteBelow("UnityCleanup();\n}", "- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url\r{\r    return [ShareSDK handleOpenURL:url wxDelegate:nil];\r}");
    }

#endif
}