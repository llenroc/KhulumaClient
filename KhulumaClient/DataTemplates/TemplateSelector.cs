using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient.DataTemplates
{
    class TemplateSelector : DataTemplateSelector
    {
        //private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        public TemplateSelector()
        {
            //incomingDataTemplate = new DataTemplate(typeof(IncomingTemplate));
            outgoingDataTemplate = new DataTemplate(typeof(OutgoingTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return outgoingDataTemplate;
        }
    }
}
