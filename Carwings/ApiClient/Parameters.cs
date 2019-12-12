using System.Collections;
using System.Collections.Generic;

namespace Carwings.ApiClient
{
    internal class Parameters : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

        public void Add(string key, string value)
        {
            list.Add(new KeyValuePair<string, string>(key, value));
        }


        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
