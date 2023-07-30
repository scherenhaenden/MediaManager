using Newtonsoft.Json;

namespace BootstrapingHelperCli.Tools;

public interface ICsvToJsonTransformation
{
    string Transform(string path);
}

public class CsvToJsonTransformation : ICsvToJsonTransformation
{
    public string Transform(string path)
    {
        /*var csvReader = new CsvReader(new StringReader(csv), CultureInfo.InvariantCulture);
        var records = csvReader.GetRecords<dynamic>();
        var json = JsonConvert.SerializeObject(records);
        return json;*/

        var csv = new List<string[]>();
        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
            csv.Add(line.Split(','));

        var properties = lines[0].Split(',');

        var listObjResult = new List<Dictionary<string, string>>();

        for (var i = 1; i < lines.Length; i++)
        {
            var objResult = new Dictionary<string, string>();
            for (var j = 0; j < properties.Length; j++)
                objResult.Add(properties[j], csv[i][j]);

            listObjResult.Add(objResult);
        }

        return JsonConvert.SerializeObject(listObjResult);
    }
}