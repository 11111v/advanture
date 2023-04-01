using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableData :Singleton<TableData>
{
   // private Dictionary<string, PoemVO> poemDict = null;
    public void Clear()
    {
       // poemDict = null;
    }
    public IEnumerator InitData()
    {
        Clear();
        yield return null;
       // InitPoemConfigData();
    }
    #region 古诗
    //public void InitPoemConfigData()
    //{
    //    if (poemDict != null)
    //    {
    //        return;
    //    }
    //    var text = ResourceManager.Instance.LoadTextConfigByPath(ResourceConst.JSONCONFIG.PoemConfig);
    //    poemDict = JsonMapper.ToObject<Dictionary<string, PoemVO>>(text);
    //}
    //public PoemVO GetPoemConfigById(string id)
    //{
    //    if (poemDict == null)
    //        InitPoemConfigData();
    //    if (poemDict == null)
    //        return null;
    //    if (poemDict.ContainsKey(id))
    //        return poemDict[id];
    //    return null;
    //}
    //public Dictionary<string, PoemVO> GetPoemDict()
    //{
    //    if (poemDict == null)
    //        InitPoemConfigData();
    //    if (poemDict == null)
    //        return null;
    //    return poemDict;
    //}
    #endregion
}
