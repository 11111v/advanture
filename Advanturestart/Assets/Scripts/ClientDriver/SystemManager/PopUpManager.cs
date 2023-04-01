using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MessageType
{
    public string msg {  set; get; }
    public Action callBack { set; get; }

    public MessageType(string msg, Action cb)
    {
        this.msg = msg;
        callBack = cb;
    }
}
public class PopUpManager:Singleton<PopUpManager>
{
    private Queue<MessageType> messageQuene = new Queue<MessageType>();
    public void ShowMessage(string message)
    {
        messageQuene.Enqueue(new MessageType(message,null));
    }
    public void ShowMessage(MessageType massageType)
    {
        messageQuene.Enqueue(massageType);
    }
    public Queue<MessageType> GetMessageQuene()
    {
        return messageQuene;
    }
}

