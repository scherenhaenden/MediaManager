// See https://aka.ms/new-console-template for more information

using BootstrapingHelperCli.Tools;

// get current path
var currentPath = Directory.GetCurrentDirectory();
var csvPath = Path.Combine(currentPath, "..", "..", "..", "..");

// get files
var files = Directory.GetFiles(csvPath);

Console.WriteLine("currentPath: " + currentPath);

ICsvToJsonTransformation csvToJsonTransformation = new CsvToJsonTransformation();

var json = csvToJsonTransformation.Transform(files[0]);

Console.WriteLine("JSON: " + json);
Console.WriteLine("Hello, World!");