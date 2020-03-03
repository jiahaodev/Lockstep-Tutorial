/****************************************************
    文件：OneThreadSynchronizationContext.cs
    作者：JiahaoWu
    邮箱: jiahaodev@163.ccom
    日期：2020/03/03 11:27       
    功能：处理发送、接收socket回调,由poll线程统一执行

    拓展：
    SynchronizationContext线程上下文说明：
  功能：SynchronizationContext在通讯中充当传输者的角色，实现功能就是一个线程和另外一个线程的通讯
       （线程间通讯）
　　* Send() 是简单的在“当前线程”上去调用委托来实现（同步调用）。
      也就是在子线程上直接调用UI线程执行，等UI线程执行完成后子线程才继续执行。
　  * Post() 是在“线程池”上去调用委托来实现（异步调用）。
      这是子线程会从线程池中找一个线程去调UI线程，子线程不等待UI线程的完成而直接执行自己下面的代码。
————————————————
版权声明：本文为CSDN博主「codingriver」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/codingriver/article/details/83378003

*****************************************************/
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Lockstep.Network {
    public class OneThreadSynchronizationContext : SynchronizationContext {
        // "线程同步"(支持并发)队列,发送、接收socket回调都放到该队列,由poll线程统一执行
        private readonly ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

        private void Add(Action action){
            this.queue.Enqueue(action);
        }

        public void Update(){
            while (true) {
                Action a;
                if (!this.queue.TryDequeue(out a)) {
                    return;
                }
                a();
            }
        }


        public override void Post(SendOrPostCallback callback, object state){
            this.Add(() => { callback(state); });
        }
    }
}