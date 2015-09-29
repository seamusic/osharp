﻿// -----------------------------------------------------------------------
//  <copyright file="InitializeOptionsBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-29 19:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Context;
using OSharp.Core.Security;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 框架初始化选项基类
    /// </summary>
    public abstract class InitializeOptionsBase
    {
        private IEntityInfoHandler _entityInfoHandler;
        private IFunctionHandler _functionHandler;
        
        /// <summary>
        /// 获取或设置 当前运行平台标识
        /// </summary>
        public PlatformToken PlatformToken { get; set; }

        /// <summary>
        /// 获取或设置 数据配置重置者
        /// </summary>
        public IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 基础日志初始化器
        /// </summary>
        public IBasicLoggingInitializer BasicLoggingInitializer { get; set; }

        /// <summary>
        /// 获取或设置 依赖注入初始化器
        /// </summary>
        public IIocInitializer IocInitializer { get; set; }

        /// <summary>
        /// 获取或设置 数据库初始化器
        /// </summary>
        public IDatabaseInitializer DatabaseInitializer { get; set; }

        /// <summary>
        /// 获取或设置 实体信息数据处理器
        /// </summary>
        public IEntityInfoHandler EntityInfoHandler
        {
            get { return _entityInfoHandler; }
            set
            {
                _entityInfoHandler = value;
                OSharpContext.Current.EntityInfoHandler = value;
            }
        }

        /// <summary>
        /// 获取或设置 功能信息数据处理器
        /// </summary>
        public IFunctionHandler FunctionHandler
        {
            get { return _functionHandler; }
            set
            {
                _functionHandler = value;
                OSharpContext.Current.FunctionHandler = value;
            }
        }
    }
}