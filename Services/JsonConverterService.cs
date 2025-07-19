using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Services{
    public class JsonConverterService{
        public string ConvertTabToJson(string input, string indent, bool pretty){
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] lines = input.Split('\n');

            foreach (string line in lines){
                string[] parts = line.Split('\t');
                if (parts.Length == 2){
                    result[parts[0].Trim()] = parts[1].Trim();
                }
            }

            JsonSerializerOptions options = new JsonSerializerOptions{
                WriteIndented = pretty
            };

            string json = JsonSerializer.Serialize(result, options);
            return FormatIndent(json, indent);
        }

        public string FormatJson(string json, string indent, bool pretty){
            Dictionary<string, object> parsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
            JsonSerializerOptions options = new JsonSerializerOptions{
                WriteIndented = pretty
            };

            string formatted = JsonSerializer.Serialize(parsed, options);
            return FormatIndent(formatted, indent);
        }

        private string FormatIndent(string json, string indent){
            if (indent == "Tab"){
                return json.Replace("  ", "\t").Replace("    ", "\t");
            } else if (indent == "4") {
                return json.Replace("  ", "    ");
            } else {
                return json; // 2スペースがデフォルト
            }
        }
    }
}
