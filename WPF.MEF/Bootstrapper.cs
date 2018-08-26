using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Caliburn.Micro;
using WPF.MEF.ViewModels;
using System.Windows.Controls;
using WPF.MEF.Helpers;

namespace WPF.MEF
{
    class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer container;

        public Bootstrapper()
        {
            // 初始化框架
            Initialize();
        }

        /// <summary>
        /// 配置框架与设置 IOC 容器
        /// </summary>
        protected override void Configure()
        {
 
            ConventionManager.AddElementConvention<PasswordBox>(PasswordBoxHelper.PasswordProperty, "Password", "PasswordChanged");

            var catalog = new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
            );

            container = new CompositionContainer(catalog);
            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());

            batch.AddExportedValue(container);
            container.Compose(batch);
        }

        /// <summary>
        /// 获取指定类型或协定名称的导出对象
        /// </summary>
        /// <param name="service">类型</param>
        /// <param name="key">协定名称</param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Any())
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        /// <summary>
        /// 获取指定类型的所有导出对象
        /// </summary>
        /// <param name="service">类型</param>
        /// <returns></returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        /// <summary>
        /// 将现有实例传递给 IOC 容器以启用注入依赖项
        /// </summary>
        /// <param name="instance">需要执行注入的实例</param>
        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        /// <summary>
        /// 在调用 Application 对象的 Run 方法时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
