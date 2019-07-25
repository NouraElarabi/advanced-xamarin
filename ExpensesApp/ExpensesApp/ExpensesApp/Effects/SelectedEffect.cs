using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExpensesApp.Effects
{
    public class SelectedEffect : RoutingEffect
    {
        public SelectedEffect() : base("CompanyName.SelectedEffect") 
        // group name, if you are implementing effects from a third party or other projects there wouldnt be a confusion
        {
        }
    }
}
