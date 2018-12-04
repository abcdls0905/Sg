using UnityEngine;

public static class ResolutionSetting
{
    static int DesignWidth = 1280;
    static int DefaultScreenWidth;
    static int DefaultScreenHeight;

    public static void SetDesignResolution(int width, int height)
    {
        DesignWidth = Mathf.Max(width, height);
    }

    private static void InitResolution()
    {
        if (DefaultScreenWidth == 0 || DefaultScreenHeight == 0)
        {
            int width = Screen.width;
            int height = Screen.height;
            DefaultScreenWidth = Mathf.Max(width, height);
            DefaultScreenHeight = Mathf.Min(width, height);
        }
    }

    public static void SetResolution(bool bMax, bool fullScreen = true)
    {
        InitResolution();
        int num = DefaultScreenWidth;
        int num2 = DefaultScreenHeight;
        if (!bMax)
        {
            num = DesignWidth;
            num2 = num * DefaultScreenHeight / DefaultScreenWidth;
        }
        if (num != Screen.width || num2 != Screen.height)
        {
            Screen.SetResolution(num, num2, fullScreen);
        }
    }
}