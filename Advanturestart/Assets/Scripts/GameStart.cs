using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameStart : UIBase
{
    protected override void Awake()
    {
        base.Awake();
        GameObjectPool.Instance.Awake();
        RoleLogicData.Init(); 
        UIManager.Instance.Init();
        StartCoroutine(TableData.Instance.InitData());
        UIManager.Instance.OpenWindows(ResourceConst.UI.MessageItemPoolVIew);
    }
    protected override void Start()
    {
        base.Start();
        //TestSerialize(ResourceConst.UI.MessageItemPoolVIew);
        
        var text = ResourceManager.Instance.LoadTextConfigByPath(ResourceConst.JSONCONFIG.ServerConfig);
        var servers = JsonMapper.ToObject<Dictionary<string, ServerConfigVO>>(text);
        if (servers == null)
        {
            Debug.LogError("无网络连接信息");
            return;
        }
        //NetManager.Instance.Connect(servers["1"].ip, servers["1"].port);
        
        //StartCoroutine(NetManager.Instance.CheckNet());
        //读取配表信息
        //var text = ResourceManager.Instance.LoadTextConfigByPath(ResourceConst.JSONCONFIG.TempCfg);
        //var a = JsonMapper.ToObject<Dictionary<string, TempVo>>(text);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //NetManager.Instance.Update();
        GameObjectPool.Instance.Update();
    }
    /// <summary>
    /// 退出程序的一些处理，比如数据库添加
    /// </summary>
    private void OnApplicationQuit()
    {
        RoleLogicData.Destory();
        //NetManager.Instance.Close();
    }
    private void TestSerialize(string str)
    {
        string path = "Assets/StreamingAssets/Config/table/UIConfig.json.txt";
        Dictionary<string, UISourceVO> uiDicts = null;
        if (!File.Exists(path))  // 判断是否已有相同文件 
        {
            uiDicts = new Dictionary<string, UISourceVO>();
            
        }
        else
        {
            var text = ResourceManager.Instance.LoadTextConfigByPath(ResourceConst.JSONCONFIG.UIConfig);
            uiDicts  = JsonMapper.ToObject<Dictionary<string, UISourceVO>>(text);
        }
        uiDicts.Add(str, new UISourceVO() { name = str });
        string outputFile = ConvertStringToJson(JsonMapper.ToJson(uiDicts));
        
        // FileStream aFile = new FileStream("Assets/StreamingAssets/Config/table/UIConfig.json.txt", FileMode.OpenOrCreate);
        File.WriteAllText(path, outputFile, new UTF8Encoding(false));//生成不带bom编码的utf8文件
    }
    /// <summary>
    /// 格式化json字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string ConvertStringToJson(string str)
    {
        //格式化json字符串
        JsonSerializer serializer = new JsonSerializer();
        TextReader tr = new StringReader(str);
        JsonTextReader jtr = new JsonTextReader(tr);
        object obj = serializer.Deserialize(jtr);
        if (obj != null)
        {
            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }
        else
        {
            return str;
        }
    }
}
