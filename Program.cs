using CustomHashTable;


HashTable<string, string> ReadEnglishDictionary(string path)
{
    var englishDictionary = File.ReadAllLines(path);
    var dictionary = new HashTable<string, string>();

    foreach (var line in englishDictionary)
    {
        var item = line.Split(";");
        dictionary.Add(item[0], item[1]);
    }

    return dictionary;
}

const string dictionaryPath = "/Users/stanislavkramar/RiderProjects/EnglishDictionary/dictionary.txt";
var englishDictionary = ReadEnglishDictionary(dictionaryPath);

englishDictionary.Remove("A");
englishDictionary["A"] = "hello";

foreach (var keyvalue in englishDictionary)  
{
    Console.WriteLine($"{keyvalue.Key}: {keyvalue.Value}");
}

Console.WriteLine(englishDictionary["A"]);
