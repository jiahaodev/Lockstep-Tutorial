/****************************************************
    �ļ���ServerLauncher.cs
    ���ߣ�JiahaoWu
    ����: jiahaodev@163.ccom
    ���ڣ�2020/03/03       
    ���ܣ��������
*****************************************************/
using System;
using System.Threading;
using Lockstep.Logging;
using Lockstep.Network;
using Lockstep.Util;

namespace Lockstep.FakeServer{
    public class ServerLauncher {
        private static Server server;

        public static void Main(){
            //let async functions call in this thread  
            OneThreadSynchronizationContext contex = new OneThreadSynchronizationContext();
            //SynchronizationContext �� https://blog.csdn.net/codingriver/article/details/83378003
            SynchronizationContext.SetSynchronizationContext(contex);
            Debug.Log("Main start");
            Utils.StartServices();
            try {
                DoAwake();      //����Server
                while (true) {
                    try {
                        Thread.Sleep(3);
                        contex.Update();  //������ա����͵�socket�ص��б�
                        server.Update();  //����߼�ʱ���Ƿ��ˣ�����game�߼�update
                    }
                    catch (ThreadAbortException e) {
                        return;
                    }
                    catch (Exception e) {
                        Log.Error(e.ToString());
                    }
                }
            }
            catch (ThreadAbortException e) {
                return;
            }
            catch (Exception e) {
                Log.Error(e.ToString());
            }
        }

        static void DoAwake(){
            server = new Server();
            server.Start();
        }
    }
}