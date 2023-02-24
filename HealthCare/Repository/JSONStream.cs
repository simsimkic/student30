// File:    JSONStream.cs
// Created: Saturday, May 23, 2020 0:57:25
// Purpose: Definition of Class JSONStream

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class JSONStream<T> : IJSONStream<T> where T : class
   {
      private string _path;


        public JSONStream(string path)
        {
            _path = path;
        }

        public IEnumerable<T> GetAll()
        {

            string jsonString = File.Exists(_path) ? File.ReadAllText(_path) : "";
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            if (jsonString != "")
                return JsonConvert.DeserializeObject<List<T>>(jsonString, jset);
            else
                return new List<T>();

        }

        public void SaveAll(IEnumerable<T> entities)
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            using (StreamWriter sw = new StreamWriter(_path))
            using (JsonWriter wr = new JsonTextWriter(sw))
            {
                serializer.Serialize(wr, entities);
            }
        }

    }
}