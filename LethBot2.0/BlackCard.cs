using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LethBot2._0
{
    class BlackCard
    {
        private BlackCardsContainer blackCardsContainer;
        public string text { get; set; }
        public int pick { get; set; }

        public BlackCard()
        {
            
        }
        public BlackCard(string text, int pick)
        {
            this.text = text;
            this.pick = pick;
        }
        public void LoadJson()
        {
                string json = File.ReadAllText("C:\\Users\\ThomasB\\Documents\\JsonDocs\\cah.json");
                Console.WriteLine(json);
                blackCardsContainer = JsonConvert.DeserializeObject<BlackCardsContainer>(json);
        }

        public BlackCard GetBlackCard()
        {
            Random r = new Random();
            int rand = r.Next(0, blackCardsContainer.blackCards.Count);
            return blackCardsContainer.blackCards[rand];
        }
    }
}
