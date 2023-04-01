using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public sealed class RoleLogicData
{
    public static TestManager testManager { private set; get; }
    private static HashSet<Type> m_ConfigTypeMap = new HashSet<Type>();
    /// <summary>
    /// 注册Manger
    /// </summary>
    /// <param name="configType"></param>
    private static void RegisterConfigType(Type configType)
    {
        if (configType == null)
            return;
        if (m_ConfigTypeMap.Contains(configType))
            return;
        m_ConfigTypeMap.Add(configType);
    }
    private static PropertyInfo[] m_Props = null;
    private static void CreateProperty(PropertyInfo info, Type infoType)
    {
        if (info == null || infoType == null)
            return;
        Object target = Activator.CreateInstance(infoType);
        info.SetValue(null, target, null);
    }
    private static void RegisterManagerPropertyCreate()
    {
        if (m_Props == null)
        {
            Type type = typeof(RoleLogicData);
            m_Props = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty);
        }
        if (m_Props == null)
            return;

        for (int i = 0; i < m_Props.Length; ++i)
        {
            var prop = m_Props[i];
            if (prop != null && prop.CanRead && prop.CanWrite)
            {
                if (m_ConfigTypeMap.Contains(prop.PropertyType))
                {
                    CreateProperty(prop, prop.PropertyType);
                }
            }
        }
    }
    /// <summary>
    /// 重置管理器数据，每次都执行，在Clear之后
    /// </summary>
    private static void InitData()
    {
        if (testManager != null) testManager.InitData();
    }
    /// <summary>
    /// 清空管理器数据，第一次不执行
    /// </summary>
    private static void ClearData()
    {
        if (testManager != null) testManager.ClearData();
    }
    public static void Init()
    {
        ClearData();
        RegisterConfigType(typeof(TestManager));
        RegisterManagerPropertyCreate();
        InitData();
    }
    public static void Destory()
    {
        DestoryManager();
    }
    public static void DestoryManager()
    {
        if (testManager != null) testManager.Destory();
    }
}

