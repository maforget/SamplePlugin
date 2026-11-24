using System.Collections.Generic;
using System.Linq;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;

namespace SamplePlugin
{
    public class AnotherSamplePlugin : DotNetPlugin, ICreateBookList
    {
        public override string Key { get; set; } = "AnotherSamplePlugin";
        public override string Name => "[Code Sample] Another custom smart list";
        public override string Description { get; set; } = "A custom smart list that returns books where the series contains the input";
        int ICreateBookList.PCount { get; set; } = 1;

        IEnumerable<ComicBook> ICreateBookList.Execute(ComicBook[] books, string a, string b)
        {
            return books.Where(book => book.Series.Contains(a));
        }
    }
}
