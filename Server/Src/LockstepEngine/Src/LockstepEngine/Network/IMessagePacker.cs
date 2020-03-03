/****************************************************
    文件：IMessagePacker.cs
    作者：JiahaoWu
    邮箱: jiahaodev@163.ccom
    日期：2020/03/03      
    功能：消息 打包、 解包接口声明
*****************************************************/
namespace Lockstep.Network {
    public interface IMessagePacker {
        //反序列
        object DeserializeFrom(ushort type, byte[] bytes, int index, int count);
        //序列化
        byte[] SerializeToByteArray(IMessage msg);
    }
}