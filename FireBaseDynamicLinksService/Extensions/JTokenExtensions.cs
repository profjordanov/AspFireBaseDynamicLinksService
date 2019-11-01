using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace FireBaseDynamicLinksService.Extensions
{
    public static class JTokenExtensions
    {
        public static JToken RemoveFields(this JToken token, params string[] fields)
        {
            if (!(token is JContainer container))
            {
                return token;
            }

            var removeList = new List<JToken>();

            foreach (var el in container.Children())
            {
                if (el is JProperty p &&
                    ((IList) fields).Contains(p.Name))
                {
                    removeList.Add(el);
                }
                el.RemoveFields(fields);
            }

            foreach (var el in removeList)
            {
                el.Remove();
            }

            return token;
        }
    }
}