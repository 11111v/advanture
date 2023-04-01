using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BindingHelper : MonoBehaviour
{
    [Header("临时对象")]
    public BindData obj;
    /// <summary>
    /// obj绑定对象
    /// </summary>
    [Header("绑定对象")]
    public List<BindData> objsBinding = new List<BindData>();
    private string csName;
    //根Transform 不是UIRoot的情况下
    private Transform rootTransform;

    [ContextMenu("Create Abstract And UIClass")]
    private void CreateAllCode()
    {
        InitInfo();
        CreateAbstract();
        CreateCs();
    }
    [ContextMenu("CreateAbstract")]
    private void CreateAbstract()
    {
        InitInfo();
        Dictionary<int, List<string>> context = new Dictionary<int, List<string>>();
        //变量成员
        context.Add(0, new List<string>());
        //变量赋值
        context.Add(1, new List<string>());
        int len = objsBinding.Count;
        for (int i = 0; i < len; i++)
        {
            var bindData = objsBinding[i];
            var memberStr = bindData.GetMemberStr();
            if (memberStr == string.Empty)
                return;
            if (context[0].Contains(memberStr))
            {
                Debug.LogError($"index:{i} 重复命名:{memberStr}");
                return;
            }

            context[0].Add(memberStr);
            var loadStr = bindData.GetAssignStr(this.transform);
            context[1].Add(loadStr);
        }
        WriteCs(context, AbstractTempPath, AbstractOutPath, (value) =>
        {
            if (value.Contains("AbstractTemplate"))
            {
                value = value.Replace("AbstractTemplate", AbstractCsName);
            }
            return value;

        });
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

    }
    [ContextMenu("CreateCs")]

    private void CreateCs()
    {

        InitInfo();
        WriteCs(new Dictionary<int, List<string>>(), CsTempPath, CsOutPath, (string value) =>
        {
            if (value.Contains("AbstractTemplate"))
            {
                value = value.Replace("AbstractTemplate", AbstractCsName);
            }
            if (value.Contains("Template"))
            {
                value = value.Replace("Template", csName);
            }

            return value;
        });
        if (gameObject.GetComponent(csName) == null)
            gameObject.AddComponent(Assembly.GetExecutingAssembly().GetType(csName));
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

    }
    [ContextMenu("CleanNullInObjsBinding")]
    private void CleanNullInObjsBinding()
    {
        int len = objsBinding.Count;
        List<BindData> indexList = new List<BindData>();
        for (int i = 0; i < len; i++)
        {
            var value = objsBinding[i];
            if (value.obj == null)
            {
                indexList.Add(value);
            }
        }
        len = indexList.Count;
        for (int i = 0; i < len; i++)
        {
            objsBinding.Remove(indexList[i]);
        }
    }
    private void InitInfo()
    {
        rootTransform = transform;
        while (!rootTransform.CompareTag("UIRoot"))
        {
            if (rootTransform.parent.CompareTag("UIRoot"))
            {
                break;
            }
            rootTransform = rootTransform.parent;
        }
        csName = gameObject.name;
        csName = csName.Replace("Template", "");
    }
    private void WriteCs(Dictionary<int, List<string>> context, string tempPath, string savePath, Func<string, string> func)
    {
        ///读取模板内容
        List<string> contextList = new List<string>();
        using (FileStream file = new FileStream(tempPath, FileMode.Open))
        using (StreamReader reader = new StreamReader(file))
        {
            while (!reader.EndOfStream)
            {
                var value = reader.ReadLine();
                if (func != null)
                {
                    value = func(value);
                }

                if (value.Contains("Member"))
                {
                    contextList.Add(value);
                    contextList.AddRange(context[0]);
                }
                else if (value.Contains("查找物体"))
                {
                    //contextList.Add(value);
                    contextList.AddRange(context[1]);
                }
                else
                {

                    contextList.Add(value);
                }

            }
        }
        var path = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        using (FileStream file = new FileStream(savePath, FileMode.Create))
        using (StreamWriter writer = new StreamWriter(file, System.Text.Encoding.UTF8))
        {
            int len = contextList.Count;
            for (int i = 0; i < len; i++)
            {
                writer.WriteLine(contextList[i]);
            }
        }
    }
    #region PathTotal
    /// <summary>
    /// 基础路径
    /// </summary>
    private string BasePath
    {
        get
        {
            ///C:\Users\pcp\Desktop\unity-project\MyClient\Assets
            return Application.dataPath.Replace("/", @"\");
        }
    }
    /// <summary>
    /// 根节点路径 = @"C:\Users\pcp\Desktop\unity-project\MyClient\Assets";
    /// </summary>
    private string CsRootPath
    {
        get
        {
            return BasePath + @"\Scripts\ClientDriver\UI\View\";
        }
    }

    /// <summary>
    /// 生成abstract模板路径 @"C:\Users\pcp\Desktop\unity-project\MyClient\Assets\Scripts\ClientDriver\UI\Base\Template\AbstractTemplate.cs";
    /// </summary>
    private string AbstractTempPath
    {
        get
        {
            return BasePath + @"\Scripts\ClientDriver\UI\Base\Template\AbstractTemplate.cs";
        }
    }
    /// <summary>
    /// cs名字
    /// </summary>
    private string AbstractCsName
    {
        get
        {
            return "Abstract_" + this.csName;
        }
    }
    /// <summary>
    /// 输出abstract模板路径
    /// </summary>
    private string AbstractOutPath
    {
        get
        {
            return string.Format(@"{0}{1}\Abstract_{2}.cs", CsRootPath, rootTransform.name, csName);
        }
    }

    /// <summary>
    /// cs模板代码
    /// </summary>
    private string CsTempPath
    {
        get
        {
            return BasePath + @"\Scripts\ClientDriver\UI\Base\Template\Template.cs";
        }
    }
    /// <summary>
    /// 输出cs模板路径
    /// </summary>
    private string CsOutPath
    {
        get
        {
            return string.Format(@"{0}{1}\{2}.cs", CsRootPath, rootTransform.name, csName);
        }
    }
    #endregion
}

[System.Serializable]
public class BindData
{
    //[System.Serializable]
    public enum Type
    {
        GameObject,
        Transform,
        Text,
        TextMeshProUGUI,
        Image,
        RawImage,
        Button,
        Toggle,
        Slider,
        Scrollbar,
        ScrollRect,
        Dropdown,
        InputField,
        LoopListView2,

    }
    public string name;
    public GameObject obj;
    public Type type = Type.GameObject;

    private string memberName
    {
        get
        {
            string formatName = this.obj.name.Substring(0, 1).ToLower() + this.obj.name.Substring(1);
            this.name = string.Format("{0}_{1}", formatName, type.ToString());
            return name;
            //return string.Format("{0}_{1}", name,type.ToString());
        }
    }

    public string GetMemberStr()
    {
        if (this.obj == null)
        {
            Debug.LogError("不能有空物体");
            return string.Empty;
        }
        string formatName = this.obj.name.Substring(0, 1).ToLower() + this.obj.name.Substring(1);
        this.name = string.Format("{0}_{1}", formatName, type.ToString());
        return string.Format(@"    protected {0} {1};", type.ToString(), memberName);
    }

    public string GetAssignStr(Transform root, string baseTranStr = "transform")
    {
        List<string> list = new List<string>();
        //如果自身有引用
        if (obj.transform == root)
        {
            switch (this.type)
            {
                case Type.GameObject:
                    return string.Format(@"        {0} = {1}.gameObject;", memberName, baseTranStr);
                case Type.Transform:
                    return string.Format(@"        {0} = {1};", memberName, baseTranStr);
                default:
                    return string.Format(@"        {0} = {1}.GetComponent<{2}>();", memberName, baseTranStr, this.type.ToString());
            }
        }
        GetPathList(obj, root, ref list);
        list.Reverse();
        string path = string.Join("/", list.ToArray());
        switch (this.type)
        {
            case Type.GameObject:
                return string.Format(@"        {0} = {2}.Find(""{1}"").gameObject;", memberName, path, baseTranStr);
            case Type.Transform:
                return string.Format(@"        {0} = {2}.Find(""{1}"").transform;", memberName, path, baseTranStr);
            default:
                return string.Format(@"        {0} = {3}.Find(""{1}"").GetComponent<{2}>();", memberName, path, this.type.ToString(), baseTranStr);
        }
    }

    /// <summary>
    /// 查找路径
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="root"></param>
    /// <param name="list"></param>
    private void GetPathList(GameObject obj, Transform root, ref List<string> list)
    {
        list.Add(obj.name.Trim());
        if (obj.transform.parent != root)
        {
            GetPathList(obj.transform.parent.gameObject, root, ref list);
        }
    }
}
