using System;
using System.IO;
using Lockstep.Logging;

namespace Lockstep.Util {
    public class Utils {
        //启动服务：定时器、协程检测
        public static void StartServices(){
            LTime.DoStart();                 
            CoroutineHelper.DoStart();
        }

        //更新服务：定时器更新、协程更新
        public static void UpdateServices(){
            LTime.DoUpdate();
            CoroutineHelper.DoUpdate();
        }

        //未找到引用？
        public static void ExecuteCmd(string shellName, string workingDir){
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.ErrorDialog = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = shellName;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.WorkingDirectory = workingDir;
            process.Start();
            Debug.Log($"ExeCmd {process.StartInfo.FileName} {shellName}    ##workingDir={process.StartInfo.WorkingDirectory} ");
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            Debug.Log(output);
        }
    }
}