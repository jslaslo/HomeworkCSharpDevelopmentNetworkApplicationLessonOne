using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TaskOne
{
    internal class Message
    {
        public Message()
        {
            
        }
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public string GetJsonString()
        {
            return JsonSerializer.Serialize(this);            
        }
        public static Message? FromJson(string message)
        {
            return JsonSerializer.Deserialize<Message>(message);
        }
        public Message(string nickname, string text)
        {
            Name = nickname;
            Text = text;
            Time = DateTime.Now;
        }
        public override string ToString()
        {
            return $"Получено сообщение!\nВ {Time.ToShortTimeString()}, от {Name}: \n{Text}";
        }
    }
}
