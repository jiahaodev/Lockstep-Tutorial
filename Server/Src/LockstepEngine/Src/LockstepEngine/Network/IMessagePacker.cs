/****************************************************
    �ļ���IMessagePacker.cs
    ���ߣ�JiahaoWu
    ����: jiahaodev@163.ccom
    ���ڣ�2020/03/03      
    ���ܣ���Ϣ ����� ����ӿ�����
*****************************************************/
namespace Lockstep.Network {
    public interface IMessagePacker {
        //������
        object DeserializeFrom(ushort type, byte[] bytes, int index, int count);
        //���л�
        byte[] SerializeToByteArray(IMessage msg);
    }
}