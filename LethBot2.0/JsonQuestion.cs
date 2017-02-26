using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace LethBot2._0
{
    public class JsonQuestion
    {
        private List<JsonQuestion> container;
        public int id { get; set; }
        public string answer { get; set; }
        public string question { get; set; }
        public int value { get; set; }

        public JsonQuestion() //default ctor for deserialization calls, avoid call loop
        {
            
        }
        public JsonQuestion(string categoryId, int value)
        {
            string url = "http://jservice.io/api/clues?category=" + categoryId + "&value=" + value;
            var json = new WebClient().DownloadString(url);
            Console.WriteLine(json);
            container = JsonConvert.DeserializeObject<List<JsonQuestion>>(json);
            //Console.WriteLine(container.);
        }
        public DataContainer GetQuestions()
        {
            return new DataContainer
            {
                Questions = container,
            };
        }
    }
}
