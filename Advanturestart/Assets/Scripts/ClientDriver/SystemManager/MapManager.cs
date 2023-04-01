using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    //百度地图Api
    public const string BaiduAk = "5ocMj3G0xahH3xoIiSezojHpKb7ukj7O";
    //经纬度转地址
    private const string LngAndLatToAddressUrl = "http://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1},{2}";
    // 地址转经纬度
    private const string AddressToLngAndLatUrl = "http://api.map.baidu.com/geocoding/v3/?address={0}&output=json&ak={1}";

    /// <summary>
    /// 根据经纬度  获取 地址信息
    /// </summary>
    /// <param name="lat">经度</param>
    /// <param name="lng">纬度</param>
    /// <returns></returns>
    public  BaiDuGeoCoding GeoCoder(string lat, string lng)
    {
        string url = string.Format(LngAndLatToAddressUrl, BaiduAk, lat, lng);
        var model = HttpClientHelper.GetResponse<BaiDuGeoCoding>(url);
        return model;
    }

    /// <summary>
    /// 根据地址信息  获取 经纬度
    /// </summary>
    /// <param name="lat">经度</param>
    /// <param name="lng">纬度</param>
    /// <returns></returns>
    public BaiDuGeoCoding GetCoordinateByAddress(string address)
    {
        string url = string.Format(AddressToLngAndLatUrl,address, BaiduAk);
        var model = HttpClientHelper.GetResponse<BaiDuGeoCoding>(url);
        return model;
    }
}



public class BaiDuGeoCoding
{
    public int Status { get; set; }
    public Result Result { get; set; }
}

public class Result
{
    public Location Location { get; set; }

    public string Formatted_Address { get; set; }

    public string Business { get; set; }

    public AddressComponent AddressComponent { get; set; }

    public string CityCode { get; set; }
}

public class AddressComponent
{
    /// <summary>
    /// 省份
    /// </summary>
    public string Province { get; set; }
    /// <summary>
    /// 城市名
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// 区县名
    /// </summary>
    public string District { get; set; }

    /// <summary>
    /// 街道名
    /// </summary>
    public string Street { get; set; }

    public string Street_number { get; set; }

}

public class Location
{
    public string Lng { get; set; }
    public string Lat { get; set; }
}

public class HttpClientHelper
{
    /// <summary>
    /// GET请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    public static T GetResponse<T>(string url) where T : class, new()
    {
        string returnValue = string.Empty;
        HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
        webReq.Method = "GET";
        webReq.ContentType = "application/json";
        using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
        {
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                returnValue = streamReader.ReadToEnd();
                T result = default(T);
                result = JsonConvert.DeserializeObject<T>(returnValue);
                return result;
            }
        }
    }
}
