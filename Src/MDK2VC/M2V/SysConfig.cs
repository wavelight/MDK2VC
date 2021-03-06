﻿using MDK2VC.M2V.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MDK2VC.M2V
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SysConfig 
    {
        /// <summary>
        /// 转换目标 2017:VC2017 2019:VC2019
        /// </summary>
        [Description("转换目标 2017:VC2017 2019:VC2019")]
        public int TargetType { get; set; } = 2019;
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string UV4_Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string MDK_Project_Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string MDK_Project_File { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string MDK_Target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string IncludePath { get; set; }
        /// <summary>
        /// vcxproj文件路径
        /// </summary>
        [Description("vcxproj文件路径")]
        public string VCProject_Path { get; set; }
        /// <summary>
        /// VC 工程名
        /// </summary>
        [Description("VC 工程名")]
        public string VcxprojName
        {
            get
            {
                return this.ProjectName + ".vcxproj";
            }
        }
        /// <summary>
        /// VC filters文件名
        /// </summary>
        [Description("VC filters 文件名")]
        public string VC_Filters_Name
        {
            get
            {
                return this.VcxprojName + ".filters";
            }
        }
        /// <summary>
        /// VC用户文件
        /// </summary>
        [Description("VC用户文件")]
        public string VC_UserFileName
        {
            get
            {
                return this.VcxprojName + ".user";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string NMakePreprocessorDefinitions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string NMakeBuildCommandLine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string NMakeCleanCommandLine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string LocalDebuggerCommandArguments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string LocalDebuggerWorkingDirectory { get; set; }
        /// <summary>
        /// 工程文件路径
        /// </summary>
        [Description("工程文件路径")]
        public string FromFilePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        public string VCPath
        {
            get
            {
                var outdir = "\\VC2019";
                switch(TargetType)
                {
                    case 2019:
                        outdir = "\\VC2019";
                        break;
                    case 2017:
                        outdir = "\\VC2017";
                        break;
                    default:
                        break;
                }
                return this.DirectoryName + outdir;
            }
        }
        /// <summary>
        /// vcxproj文件路径
        /// </summary>
        [Description("vcxproj文件路径")]
        public string Vcxproj
        {
            get
            {
                return this.VCPath + "\\"+ FileNameWithoutExtension + ".vcxproj";
            }
        }
        /// <summary>
        /// filters文件路径
        /// </summary>
        [Description("filters文件路径")]
        public string Filters
        {
            get
            {
                return this.VCPath + "\\" + FileNameWithoutExtension + ".vcxproj.filters";
            }
        }
        /// <summary>
        /// sln文件路径
        /// </summary>
        [Description("sln文件路径")]
        public string Sln
        {
            get
            {
                return this.VCPath + "\\" + FileNameWithoutExtension + ".sln";
            }
        }
        public string Vcusers
        {
            get
            {
                return this.VCPath + "\\" + FileNameWithoutExtension + ".vcxproj.user";
            }
        }
        /// <summary>
        /// 宏定义
        /// </summary>
        public List<string> MacroDefine { private get; set; }
        /// <summary>
        /// 预定义
        /// </summary>
        public string MacroDefineStr
        {
            get
            {
                var builder = new StringBuilder();
                foreach(var str in MacroDefine)
                {
                    builder.Append(str.Trim()).Append(";");
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// 预定义
        /// </summary>
        public string MacroDefineStrCNMake
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var str in MacroDefine)
                {
                    builder.Append(str.Trim()).Append(",");
                }
                builder = builder.Remove(builder.Length-1,1);
                return builder.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Projguid { get; set; }
        public string Projguidvc
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append(Projguid);
                return builder.ToString();
            }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public List<String> IncludePathOld { private get; set; }
        /// <summary>
        /// 项目包含文件
        /// </summary>
        public BTree<Node> ProjFiles { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string IncludePathStr
        {
            get
            {
                var builder = new StringBuilder();
                for(int i=0;i<IncludePathOld.Count;i++)
                {
                    builder.Append(IncludePathOld[i]);
                    if (i != IncludePathOld.Count - 1)
                        builder.Append(";");
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// 仅文件名，不包含路径 hello
        /// </summary>
        public string FileNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FromFilePath);
            }
        }
        public string DirectoryName
        {
            get
            {
                return Path.GetDirectoryName(FromFilePath);
            }
        }
        /// <summary>
        /// 返回扩展名 ：.txt
        /// </summary>
        public string Extension
        {
            get
            {
                return Path.GetExtension(FromFilePath);
            }
        }
        /// <summary>
        /// 过滤器文件、目录列表
        /// </summary>
        public string ToFilter_FileFolders { get; set; }
        /// <summary>
        /// 所有文件列表
        /// </summary>
        public string ToProj_Files { get; set; }
        /// <summary>
        /// 过滤器目录列表
        /// </summary>
        public string ToFilter_files { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _Config
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string ToolName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string ToolsVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string UV4Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        public string DocName { get; set; }
    }
}
static class Fff
{
    public static void Write(this FileStream fs, byte[] array)
    {
        fs.Write(array, 0, array.Length);
    }
}
