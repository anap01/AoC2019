using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode
{
    [DebuggerTypeProxy(typeof(DictionaryDebugView<,>))]
    public class AutoinsertingDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var value))
                    return value;
                else
                {
                    return this[key] = default(TValue);
                }
            }

            set => base[key] = value;
        }
    }
    
    
    internal sealed class DictionaryDebugView<TKey, TValue> {
        private readonly IDictionary<TKey, TValue> m_dict; 
        
        public DictionaryDebugView(IDictionary<TKey, TValue> dictionary) {
            this.m_dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }
       
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] Items   { 
            get {
                var items = new KeyValuePair<TKey, TValue>[m_dict.Count];
                m_dict.CopyTo(items, 0);
                return items;
            }
        }
    }       
}