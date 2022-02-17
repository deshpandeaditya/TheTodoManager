using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TheTodoManager.BLL.Common;
using TheTodoManager.BLL.Interfaces;
using TheTodoManager.Models;

namespace TheTodoManager.BLL.Services
{
    public class TodoService : ITodoService
    {
        BaseConfigOptions options;
        IConfiguration configuration;
        string BaseUrl;
        ILogger<TodoService> logger;

        public TodoService(IOptions<BaseConfigOptions> _options, IConfiguration _configuration, ILogger<TodoService> _logger)
        {
            this.logger = _logger ?? throw new ArgumentNullException(nameof(_logger));

            this.configuration = _configuration;
            this.options = _options.Value;

            var base_url_from_config = this.configuration["ExternalUrls:BaseUrl"];
            var base_url_from_options = this.options.ExternalUrls.BaseUrl;

            this.BaseUrl = base_url_from_options;
        }

        public Todo Get(int id)
        {
            var _url = BaseUrl + $"todos/{id}";
            logger.LogInformation(">>>> Making API Call to : " + _url);

            HttpResponseMessage response = RestClient.Get(_url).Result;
            Todo todoItem = JsonConvert.DeserializeObject<Todo>(response.Content.ReadAsStringAsync().Result);

            return todoItem;
        }

        public IEnumerable<Todo> GetItems()
        {
            var _url = BaseUrl + "todos";
            logger.LogInformation(">>>> Making API Call to : " + _url);

            HttpResponseMessage response = RestClient.Get(_url).Result;
            IEnumerable<Todo> todos = JsonConvert.DeserializeObject<IEnumerable<Todo>>(response.Content.ReadAsStringAsync().Result);
            return todos;
        }
    }
}
