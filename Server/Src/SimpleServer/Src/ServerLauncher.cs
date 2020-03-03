/****************************************************
    文件：ServerLauncher.cs
    作者：JiahaoWu
    邮箱: jiahaodev@163.ccom
    日期：2020/03/03       
    功能：程序入口
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
            //SynchronizationContext ： https://blog.csdn.net/codingriver/article/details/83378003
            SynchronizationContext.SetSynchronizationContext(contex);
            Debug.Log("Main start");
            Utils.StartServices();
            try {
                DoAwake();      //启动Server
                while (true) {
                    try {
                        Thread.Sleep(3);
                        contex.Update();  //处理接收、发送的socket回调列表
                        server.Update();  //检测逻辑时间是否到了，驱动game逻辑update
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