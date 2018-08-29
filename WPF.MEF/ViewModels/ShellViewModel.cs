using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace WPF.MEF.ViewModels
{
    [Export(typeof(IShell))]
    class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        public string Password { get; set;}

        [Import]
        private IWindowManager windowManager;

        public void Login(string username, string password)
        {
            Console.WriteLine("Password: " + this.Password);
            Console.WriteLine(username +" "+ password);
        }

        public void Open()
        {
            windowManager.ShowDialog(IoC.Get<DialogViewModel>());
        }
    }
}
