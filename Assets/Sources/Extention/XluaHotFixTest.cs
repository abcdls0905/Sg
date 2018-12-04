using UnityEngine;

[XLua.Hotfix(XLua.HotfixFlag.Stateless)]
public class XluaHotFixTest
{
    public string name;
    public int Add(int a, int b)
    {
        return a - b;
    }
    //delegate........................................................
    [XLua.CSharpCallLua]
    public delegate void delegateTest();

    public delegateTest delegateTestxxxx = ()=>
    {
        Debug.Log("delegate: this is C# call");
    };
    public int DelegateTest(delegateTest xxx)
    {
        return 0;
    }

    //events........................................................
    [XLua.CSharpCallLua]
    public delegate void eventDelegate();
    public event eventDelegate events;
    public void TriggerEvents()
    {
        events();
    }

    //·ºÐÍ........................................................
    public void AddComp<T>() where T : Component
    {
        Debug.Log("·ºÐÍ: this is C# call");
    }
}