using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        //private NewBookAlertMessageConfig _newBookOption = null;
        private readonly IOptionsMonitor<NewBookAlertMessageConfig> _newBookOption = null;
        public MessageRepository(IOptionsMonitor<NewBookAlertMessageConfig> newBookOption) {
            //_newBookOption = newBookOption.CurrentValue;
            //if we want to get the update value from appSettings.json file in Singleton Services we use below code
            //newBookOption.OnChange(config=> {
            //    _newBookOption = config;
            //});

            _newBookOption = newBookOption;


        }
        public string GetName()
        {
            //return _newBookOption.AlertMessage;
            return _newBookOption.CurrentValue.AlertMessage;
        }
    }
}
