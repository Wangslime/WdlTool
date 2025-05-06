using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace DRSoft.Runtime.MVVM.Toolkit
{
    public class ParsedMessage
    {
        public string EventName { get; set; }
        public string MethodName { get; set; }
        public List<string> Parameters { get; set; }
    }

    public static class MessageParser
    {
        static readonly Regex LongFormatRegularExpression = new Regex(@"^[\s]*\[[^\]]*\][\s]*=[\s]*\[[^\]]*\][\s]*$", RegexOptions.Compiled);
        public static List<ParsedMessage> ParseMessage(string message)
        {
            //// 预处理：移除所有空格和分号
            //var trimmed = Regex.Replace(message, @"[\s;]+", "");

            //// 正则表达式（支持复杂方法名和参数）
            //var pattern =
            //    @"$$Event(?<event>[^$$]+)\]" +
            //    @"=" +
            //    @"$$Action(?<method>[^$$]+)($(?<params>[^$]*)\))?$$";

            //var match = Regex.Match(trimmed, pattern);
            //if (!match.Success)
            //    throw new FormatException($"Invalid Message.Attach syntax: {message}");

            // 提取数据
            //var eventName = match.Groups["event"].Value;
            //var methodName = match.Groups["method"].Value;
            //var paramsStr = match.Groups["params"].Value;

            var messageTexts = StringSplitter.Split(message, ';');
            List<ParsedMessage> parsedMessageList = new List<ParsedMessage>();
            foreach (var messageText in messageTexts)
            {
                var triggerPlusMessage = LongFormatRegularExpression.IsMatch(message)
                                            ? StringSplitter.Split(message, '=')
                                            : new[] { null, message };
                var messageEvent = triggerPlusMessage.First()
                 .Replace("[", string.Empty)
                 .Replace("]", string.Empty)
                 .Replace("Event", string.Empty)
                 .Trim();

                var messageAction = triggerPlusMessage.Last()
                  .Replace("[", string.Empty)
                  .Replace("]", string.Empty)
                  .Replace("Action", string.Empty)
                  .Trim();

                string eventName = messageEvent;
                int index = messageAction.IndexOf("(");
                string methodName = messageAction.Substring(0, index);
                string paramsStr = messageAction.Replace(methodName, string.Empty).Replace(")", string.Empty).Replace("(", string.Empty).Trim();
                //解析参数
                List<string> parameters = null;
                if (!string.IsNullOrEmpty(paramsStr))
                {
                    parameters ??= new List<string>();
                    parameters = paramsStr.Split(',').ToList();
                }
                parsedMessageList.Add(new ParsedMessage
                {
                    EventName = eventName,
                    MethodName = methodName,
                    Parameters = parameters
                });
            }
            return parsedMessageList;
        }
    }
}
