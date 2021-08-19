using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.IO
{
    public class JsonInterfacer<T> where T : new()
    {
        private const bool AutoAppendExtension = true;
        private const string Extension = ".json";

        private const string STORAGE_DIR = "./Storage/";
        private readonly string FILE_NAME;
        private string FILE_DIR => Path.Combine(STORAGE_DIR, FILE_NAME);

        public T Data;

        public JsonInterfacer(string fileName, Action<string> onDirectoryInitiated = null)
        {
            // Create Storage Directory if it's missing
            if (!Directory.Exists(STORAGE_DIR))
            {
                Directory.CreateDirectory(STORAGE_DIR);
            }

            if (!fileName.ToLower().EndsWith(Extension) && AutoAppendExtension)
                fileName += Extension;

            FILE_NAME = fileName;

            if (!File.Exists(FILE_DIR))
            {
                File.Create(FILE_DIR).Close();
                Data = new T();
                Save();
                onDirectoryInitiated?.Invoke(FILE_DIR);
            }
            else
                Load();
        }

        private string FileContent
        {
            get
            {
                return File.ReadAllText(FILE_DIR);
            }
            set
            {
                File.WriteAllText(FILE_DIR, value);
            }
        }

        private T FileContentAsT
        {
            get
            {
                return JsonConvert.DeserializeObject<T>(FileContent);
            }
            set
            {
                FileContent = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }

        public void Save()
        {
            FileContentAsT = Data;
        }

        public void Load()
        {
            try
            {
                Data = FileContentAsT;
            }
            catch
            {
                Console.WriteLine($"[JsonInterfcer<{typeof(T)}>] Failed to load file at directory:\n\t - {FILE_DIR}\n\t - Initializing as new {typeof(T)}");
            }
        }

        public static implicit operator T(JsonInterfacer<T> interfacer) => interfacer.Data;
    }
}
