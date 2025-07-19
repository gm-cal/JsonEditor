using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Utils;

namespace Services{
    public interface IJsonConvertService{
        string TabSeparatedToJson(string input, string indentOption, out string error);
        string PrettyPrintJson(string json, string indentOption, out string error);
        string MinifyJson(string json, out string error);
    }

    public class JsonConvertService : IJsonConvertService{
        public string TabSeparatedToJson(string input, string indentOption, out string error){
            error = string.Empty;
            try{
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string[] lines = input.Replace("\r\n", "\n").Split('\n');
                foreach (string line in lines){
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('\t');
                    if (parts.Length < 2){
                        error = "タブ区切りで2列必要です: " + line;
                        return string.Empty;
                    }
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    dict[key] = value;
                }
                string indent = GetIndentString(indentOption);
                JsonSerializerOptions options = new JsonSerializerOptions{
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                string json = JsonSerializer.Serialize(dict, options);
                if (indent != " ".Repeat(2)){
                    json = json.Replace("  ", indent);
                }
                return json;
            } catch (Exception ex) {
                error = ex.Message;
                return string.Empty;
            }
        }

        public string PrettyPrintJson(string json, string indentOption, out string error){
            error = string.Empty;
            try{
                object? obj = JsonSerializer.Deserialize<object>(json);
                string indent = GetIndentString(indentOption);
                JsonSerializerOptions options = new JsonSerializerOptions{
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                string pretty = JsonSerializer.Serialize(obj, options);
                if (indent != " ".Repeat(2)){
                    pretty = pretty.Replace("  ", indent);
                }
                return pretty;
            } catch (Exception ex) {
                error = ex.Message;
                return string.Empty;
            }
        }

        public string MinifyJson(string json, out string error){
            error = string.Empty;
            try{
                object? obj = JsonSerializer.Deserialize<object>(json);
                JsonSerializerOptions options = new JsonSerializerOptions{
                    WriteIndented = false,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return JsonSerializer.Serialize(obj, options);
            } catch (Exception ex) {
                error = ex.Message;
                return string.Empty;
            }
        }

        private static string GetIndentString(string indentOption){
            if (indentOption == "2") return "  ";
            if (indentOption == "4") return "    ";
            if (indentOption == "Tab") return "\t";
            return "  ";
        }
    }
}
