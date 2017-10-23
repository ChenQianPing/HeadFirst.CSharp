using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    public class MonitorDirectory
    {
        public static void TestMethod()
        {

            /*
             * 当需要监控某一文件，
             * FileSystemWatcher类提供了Created, Deleted,Rename等事件。
             * 
             * 就拿FileSystemWatcher的Created事件来说，该事件类型是FileSystemEventHandler。
             * public delgate void FileSystemEventHandler(Object sender, FileSystemEventArgs e)
             *              
             * sender表示事件的发起者；
             * e表示事件参数；
             */


            // 启用FileSystemWatcher
            var watcher = new FileSystemWatcher(@"D:\pic") {EnableRaisingEvents = true};
            
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            Console.ReadKey();
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType}:{e.Name}");
        }
        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType}:{e.Name}");
        }
        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType}:原文件名{e.OldName},新文件名{e.Name}");
        }

    }
}
