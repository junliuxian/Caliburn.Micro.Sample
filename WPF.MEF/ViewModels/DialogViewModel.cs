using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace WPF.MEF.ViewModels
{
    [Export(typeof(DialogViewModel))]
    class DialogViewModel : Screen
    {
        public int Val { get; set; }

        public DialogViewModel()
        {
            Val = 1;
        }

        protected override void OnViewAttached(object view, object context)
        {
          //  Val = 2;

        }

        protected override void OnInitialize()
        {
            Val = 3;    
        }

        protected override void OnActivate()
        {
            Val = 4;
        }


    }
}
