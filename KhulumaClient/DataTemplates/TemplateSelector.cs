using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient.DataTemplates
{
    class TemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        public TemplateSelector()
        {
            incomingDataTemplate = new DataTemplate(typeof(IncomingTemplate));
            outgoingDataTemplate = new DataTemplate(typeof(OutgoingTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageUserId = ((ChatModel)item).UserId;
            var myUserId = Helpers.Settings.id;

            var messageText = ((ChatModel)item).Message;
            var messageID = ((ChatModel)item).ChatMessageAPIModelId;

            Debug.WriteLine("Message User ID: {0}", messageUserId);
            Debug.WriteLine("My User ID: {0}", myUserId);
            Debug.WriteLine("Message Text: {0}", messageText);
            Debug.WriteLine("Message ID: {0}", messageID);

            if (messageUserId == myUserId)
            {
                return outgoingDataTemplate;
            } else
            {
                return incomingDataTemplate;
            }

            
        }
    }
}
