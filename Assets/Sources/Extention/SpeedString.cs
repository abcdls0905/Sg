using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class SpeedString
{
    public string string_base;
    public StringBuilder string_builder;
    private char[] int_parser = new char[11];

    public SpeedString(int capacity)
    {
        string_builder = new StringBuilder(capacity, capacity);
        string_base = (string)string_builder.GetType().GetField(
          "_str",
          System.Reflection.BindingFlags.NonPublic |
          System.Reflection.BindingFlags.Instance).GetValue(string_builder);
    }

    public int i;
    public void Clear()
    {
        string_builder.Length = 0;
        for(i = 0; i < string_builder.Capacity; i++)
        {
            string_builder.Append('\0');
        }
        string_builder.Length = 0;
    }

    //public void Draw(ref Text text)
    //{
    //    text.text = "";
    //    text.text = string_base;
    //    text.cachedTextGenerator.Invalidate();
    //}

    public void Append(string value)
    {
        string_builder.Append(value);
    }

    int count;
    public void Append(int value)
    {
        if(value >= 0)
        {
            count = ToCharArray((uint)value, int_parser, 0);
        }
        else
        {
            int_parser[0] = '-';
            count = ToCharArray((uint)-value, int_parser, 1) + 1;
        }
        for(i = 0; i < count; i++)
        {
            string_builder.Append(int_parser[i]);
        }
    }

    public static int ToCharArray(uint value, char[] buffer, int bufferIndex)
    {
        if(value == 0)
        {
            buffer[bufferIndex] = '0';
            return 1;
        }
        int len = 1;
        for(uint rem = value/10; rem > 0; rem /= 10)
        {
            len++;
        }
        for(int i = len - 1; i >= 0; i--)
        {
            buffer[bufferIndex + i] = (char)('0' + (value % 10));
            value /= 10;
        }
        return len;
    }

    override public string ToString()
    {
        return string_base;
    }
}

public class CacheStringManager : GameUtil.Singleton<CacheStringManager>
{
    private Dictionary<int, List<SpeedString>> speedStrings = new Dictionary<int, List<SpeedString>>();
    public Dictionary<string, string> cacheStrings = null;

    public override void Init()
    {
        base.Init();
        for (int i = 0; i < 4; i++)
        {
            List<SpeedString> list = new List<SpeedString>();
            speedStrings.Add((int)System.Math.Pow(2, i + 2), list);
        }
        cacheStrings = new Dictionary<string, string>();
    }

    public override void UnInit()
    {
        base.UnInit();
        speedStrings.Clear();
        cacheStrings.Clear();
    }

    public string GetCacheString(string key)
    {
        string value;
        if (cacheStrings.TryGetValue(key, out value))
            return value;
        return "";
    }

    public void CacheString(string key, string value)
    {
        cacheStrings.Add(key, value);
    }
}
