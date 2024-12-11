var input = await File.ReadAllLinesAsync(Path.Join(Directory.GetCurrentDirectory(), "input.txt"));

var stones = input[0].Split(' ').Select(long.Parse).ToList();

var globalDict = new Dictionary<long, long>();
stones.GroupBy(s => s).ToList().ForEach(g => globalDict.Add(g.Key, g.Count()));
var epoch = 1;

while (epoch <= 75) //Limit for the problem
{
    var tempDict = new Dictionary<long, long>();
    foreach (var (stone, count) in globalDict)
    {
        if (stone == 0)
        {
            AddOrUpdateDictionary(1, count, tempDict);
        }
        else if (HasEvenNumberOfDigits(stone))
        {
            var halves = GetHalves(stone);
            AddOrUpdateDictionary(halves.Item1, count, tempDict);
            AddOrUpdateDictionary(halves.Item2, count, tempDict);
        }
        else
        {
            AddOrUpdateDictionary(stone * 2024, count, tempDict);
        }
    }
    globalDict = tempDict;
    epoch++;
}

long answer = 0;
foreach (var (stone, count) in globalDict)
{
    answer += count;
}

Console.WriteLine(answer);
return;

void AddOrUpdateDictionary(long key, long value, Dictionary<long, long> dict)
{
    if (!dict.TryAdd(key, value))
    {
        dict[key] += value;
    }
}
int GetNumberOfDigits(long number)
{
    return (int)Math.Floor(Math.Log10(number) + 1);
}
bool HasEvenNumberOfDigits(long number)
{
    return GetNumberOfDigits(number) % 2 == 0;
}
(long, long) GetHalves(long number)
{
    var power = GetNumberOfDigits(number) / 2;
    return ((long)(number / Math.Pow(10, power)), (long)(number % Math.Pow(10, power)));
}