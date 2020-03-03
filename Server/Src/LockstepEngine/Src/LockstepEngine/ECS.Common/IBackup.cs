/****************************************************
    文件：IBackup.cs
    作者：JiahaoWu
    邮箱: jiahaodev@163.ccom
    日期：2020/03/03      
    功能：回退接口
*****************************************************/
using System.Text;
using Lockstep.Serialization;

namespace Lockstep.Game {
    public interface IBackup : IDumpStr {
        void WriteBackup(Serializer writer);
        void ReadBackup(Deserializer reader);
    }

    public interface IHashCode {
        int GetHash(ref int idx);
    }

    public interface IDumpStr {
        void DumpStr(StringBuilder sb, string prefix);
    }

    public interface IAfterBackup {
        void OnAfterDeserialize(); //反序列化后重构
    }
}