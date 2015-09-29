﻿// -----------------------------------------------------------------------
//  <copyright file="FrameworkInitializerBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-09-23 14:59</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Configs;
using OSharp.Core.Context;
using OSharp.Core.Properties;
using OSharp.Core.Security;


namespace OSharp.Core.Initialize
{
    /// <summary>
    /// 框架初始化基类
    /// </summary>
    public abstract class FrameworkInitializerBase : IFrameworkInitializer
    {
        //基础模块，只初始化一次
        private static bool _basicLoggingInitialized;
        private static bool _databaseInitialized;
        private static bool _entityInfoInitialized;

        private readonly InitializeOptionsBase _options;


        /// <summary>
        /// 初始化一个<see cref="FrameworkInitializerBase"/>类型的新实例
        /// </summary>
        protected FrameworkInitializerBase(InitializeOptionsBase options)
        {
            _options = options;
        }
        
        /// <summary>
        /// 开始初始化
        /// </summary>
        public void Initialize()
        {
            OSharpConfig config = ResetConfig(OSharpConfig.Instance);

            if (!_basicLoggingInitialized && _options.BasicLoggingInitializer != null)
            {
                _options.BasicLoggingInitializer.Initialize(config.LoggingConfig);
                _basicLoggingInitialized = true;
            }

            if (_options.IocInitializer == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_IocInitializeIsNull);
            }
            _options.IocInitializer.Initialize(config);

            if (!_databaseInitialized)
            {
                if (_options.DatabaseInitializer == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_DatabaseInitializeIsNull);
                }
                _options.DatabaseInitializer.Initialize(config.DataConfig);
                _databaseInitialized = true;
            }

            if (!_entityInfoInitialized)
            {
                if (_options.EntityInfoHandler == null)
                {
                    throw new InvalidOperationException(Resources.FrameworkInitializerBase_EntityInfoHandlerIsNull);
                }
                _options.EntityInfoHandler.Initialize();
                _entityInfoInitialized = true;
            }

            if (_options.FunctionHandler == null)
            {
                throw new InvalidOperationException(Resources.FrameworkInitializerBase_FunctionHandlerIsNull);
            }
            _options.FunctionHandler.Initialize();
        }

        /// <summary>
        /// 重写以实现重置OSharp配置信息
        /// </summary>
        /// <param name="config">原始配置信息</param>
        /// <returns>重置后的配置信息</returns>
        protected OSharpConfig ResetConfig(OSharpConfig config)
        {
            if (_options.DataConfigReseter != null)
            {
                config.DataConfig = _options.DataConfigReseter.Reset(config.DataConfig);
            }
            return config;
        }
    }
}