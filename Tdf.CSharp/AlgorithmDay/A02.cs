using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.AlgorithmDay
{
    /*
     * 递归的一个典型应用就是遍历目标文件夹，
     * 把该文件夹下的所有文件和文件夹打印或显示出来，
     * 还可以递归计算目标文件夹的总大小。
     */ 
    public class A02
    {
        public static void TestMethod()
        {
            Console.WriteLine("输入目标文件夹");
            var path = Console.ReadLine();
            FindFoldersAndFiles(path);
            Console.WriteLine("\r\n");
            Console.WriteLine($"目标文件夹的总大小为：{GetDirectoryLength(path)}个字节");
            Console.ReadKey();
        }

        /// <summary>
        /// 递归目标文件夹中的所有文件和文件夹
        /// </summary>
        /// <param name="path"></param>
        private static void FindFoldersAndFiles(string path)
        {
            Console.WriteLine("文件夹" + path);
            // 遍历目标文件夹的所有文件
            foreach (var fileName in Directory.GetFiles(path))
            {
                Console.WriteLine("┣" + fileName);
            }

            // 遍历目标文件夹的所有文件夹
            foreach (var directory in Directory.GetDirectories(path))
            {
                FindFoldersAndFiles(directory);
            }

        }

        // 递归计算文件夹大小
        static long GetDirectoryLength(string path)
        {
            if (!Directory.Exists(path))
            {
                return 0;
            }

            long size = 0;

            // 遍历指定路径下的所有文件
            var di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles())
            {
                size += fi.Length;
            }

            // 遍历指定路径下的所有文件夹
            var dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                foreach (var t in dis)
                {
                    size += GetDirectoryLength(t.FullName);
                }
            }
            return size;
        }

    }
}
