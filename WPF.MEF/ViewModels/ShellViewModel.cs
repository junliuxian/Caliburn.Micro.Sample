using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace WPF.MEF.ViewModels
{
    [Export(typeof(ShellViewModel))]
    class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public string Password { get; set;}
        
        public void Login(string username, string password)
        {
            Console.WriteLine("Password: " + this.Password);
            Console.WriteLine(username +" "+ password);
        }
    }
}
