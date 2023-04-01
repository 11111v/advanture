using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    // 读取配置文件
    public string LoadTextConfigByPath(string fileName)
    {
        string ret = GetStringConfigByPath(fileName);
        return ret;
    }
    /// <summary>
    ///  加载配置文件字符串, 如果读取AB或者Resources目录的配置采用加密的读取
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public string GetStringConfigByPath(string fileName)
    {
        string ret = string.Empty;
        byte[] buf = GetBytesConfigByWritePath(fileName);
        if (buf != null)
            ret=Encoding.UTF8.GetString(buf);

        return ret;
    }
    private byte[] GetBytesConfigByWritePath(string fileName)
    {
        //defaultCompany/myClient/这种路径，这个必须支持下载的情况下，现在暂时没有服务器，所以用本地的方法
        string persistFileName = string.Format("{0}/{1}", Application.persistentDataPath, fileName);
        string streamingAssetsFileName = string.Format("{0}/{1}", Application.streamingAssetsPath, fileName);
        FileStream stream = null;
        if (File.Exists(persistFileName))
        {
            stream = new FileStream(persistFileName, FileMode.Open, FileAccess.Read);
        }
        else
        {
            stream = new FileStream(streamingAssetsFileName, FileMode.Open, FileAccess.Read);
        }

        if (stream == null)
            return null;
        try
        {
            if (stream.Length <= 0)
                return null;
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }
        finally
        {
            stream.Close();
            stream.Dispose();
        }
    }


}
