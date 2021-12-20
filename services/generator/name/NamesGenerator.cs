using System;
using System.Collections.Generic;
using System.IO;

namespace WormsApplication.services.generator.name
{
    public class NamesGenerator
    {
        private const string FileWithNames = "namesOfWorms.txt";
        private const int CountOfWormsInFile = 1000;
        private readonly List<int> _countOfUsing;
        private readonly List<int> _notUsedList;

        public NamesGenerator()
        {
            _countOfUsing = new List<int>();
            for (var i = 0; i < CountOfWormsInFile; i++) _countOfUsing.Add(0);
            _notUsedList = new List<int>();
            for (var i = 0; i < CountOfWormsInFile; i++) _notUsedList.Add(i);
        }

        public string Generate()
        {
            var randomId = new Random().Next(0, _notUsedList.Count - 1);
            var lineId = _notUsedList[randomId];
            _notUsedList.Remove(lineId);
            _countOfUsing[lineId]++;
            return File.ReadAllLines(FileWithNames)[lineId];
        }

        public List<int> GetCountOfUsing()
        {
            return _countOfUsing;
        }
    }
}