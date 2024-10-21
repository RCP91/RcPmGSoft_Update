using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static AutoUpdate.Classes.F_Update;

namespace AutoUpdate.Classes
{
    internal class JsonWork
    {
        private static readonly string jsonLocal = Path.Combine(jsonFolderPath, "config.json");

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new F_Update());
        }

        public static void LoadJson()
        {
            try
            {
                JObject jsonContent = JObject.Parse(File.ReadAllText(jsonLocal));
                JObject configObject = (JObject)jsonContent["config"];

                // Atualiza as variáveis com os valores do arquivo JSON
                version = (string)configObject["version"];
                repo = (string)configObject["repo"];
                owner = (string)configObject["owner"];
            }
            catch (Exception ex)
            {
                WriteJson("", "");
                alertError($"Erro ao carregar configurações do arquivo JSON: {ex.Message}", "Erro ao Carregar.");
            }
        }
        public static void WriteJson(string key, object value)
        {
            JObject configObject;

            if (File.Exists(jsonLocal))
            {
                // Carrega o arquivo JSON existente
                configObject = JObject.Parse(File.ReadAllText(jsonLocal));
                configObject["config"][key] = JToken.FromObject(value);
            }
            else
            {
                // Cria um novo objeto JSON se o arquivo não existir
                JObject newConfigObject = new JObject();
                newConfigObject["version"] = version ?? "1.0.0.1";
                newConfigObject["repo"] = repo ?? "RcPmGSoft_Update";
                newConfigObject["owner"] = owner ?? "RCP91";

                // Adiciona um objeto de configuração ao novo objeto JSON
                configObject = new JObject(new JProperty("config", newConfigObject));
            }

            try
            {
                // Escreve o objeto JSON resultante de volta para o arquivo JSON
                File.WriteAllText(jsonLocal, configObject.ToString(Formatting.Indented));
            }
            catch (Exception ex)
            {
                alertError($"Erro ao gravar configurações no arquivo JSON: {ex.Message}", "Erro ao Gravar.");
            }
        }

    }
}