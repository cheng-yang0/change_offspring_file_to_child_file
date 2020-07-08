using System;
using System.IO;
using System.Collections.Generic;
namespace get_file_from_dir_recursive
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("请输入要删除的目录");
            string dir = Console.ReadLine();
            Console.WriteLine();
            get_file_from_dir(dir);
            delete_empty_dir(dir);
        }
        static void get_file_from_dir(string dir)
        {
            List<string> file_list= new List<string>();
            get_file_from_dir_recursive(dir,file_list);
            foreach (string file in file_list)
            {
                //Console.WriteLine(str);
                string[]str_arr=file.Split('\\');
                string file_name=str_arr[str_arr.Length-1];
                string file_new = dir + "\\" + file_name;
                if (!File.Exists(file_new))
                    File.Move(file, file_new);
                else
                    if(Path.GetDirectoryName(file)!=dir)
                         Console.WriteLine(file+ "没有成功移动");                                                   
            }   
        }
        static List<string> get_file_from_dir_recursive(string dir,List<string> file_list)
        {
            string[]child_dir = Directory.GetDirectories(dir);
            string[] child_file = Directory.GetFiles(dir);
            file_list.AddRange(child_file);
            foreach(string str in child_dir)
            {
                get_file_from_dir_recursive(str,file_list);
            }
            return file_list;
        }
        static void delete_empty_dir(string dir)
        {
            List<string> file_list=new List<string>();
            get_file_from_dir_recursive(dir,file_list);
            if(file_list.Count==0)
            {
                Directory.Delete(dir,true);
            }
            else
            {
                foreach (string child_dir in Directory.GetDirectories(dir))
                {
                    delete_empty_dir(child_dir);
                }
            }
        }
    }
}
