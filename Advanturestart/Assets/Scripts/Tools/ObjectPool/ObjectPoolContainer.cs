using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 供对象池使用的容器（Container）：容器内对象的内容、标记容器内对象为已被（未被）使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPoolContainer<T>
{
    // 容器内对象
    public T Item { get; set; }
    // 容器内对象是否已被使用的标记
    public bool Used { get; private set; }

    // 标记为已被使用
    public void Consume()
    {
        Used = true;
    }

    // 标记为未被使用
    public void Release()
    {
        Used = false;
    }
}