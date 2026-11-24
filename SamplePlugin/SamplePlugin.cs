using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using HtmlAgilityPack;

[assembly: PluginApiVersion(1)]

namespace SamplePlugin
{
    public class SamplePlugin : DotNetPlugin, ILibrary, IEditor, IBooks, IConfig
    {
        public override string Key { get; set; } = "SamplePlugin";
        public override string Name => "[Code Sample] .NET Sample Plugin";
        public override string Description => "Returns the number of books";

        // Image that will be associated with this plugin
        // You can embed the plugin image as an embedded ressource and the plugin engine will read it from there, if no file exists in the plugin folder location
        public override string Image => "SamplePlugin.png";

        // Implementation for Library (or generic) hooks
        public void Execute(ComicBook[] books)
        {
            Log("Running from the generic entry point");
            string localizedText = string.Format(ComicRack.Localize(Key, "Library", "You have {0} books in your library."), books.Count());
            MessageBox.Show(localizedText);
        }

        // Implementation for Books hook
        void IBooks.Execute(ComicBook[] books)
        {
            Log("Running from IBooks entry point");
            string localizedText = string.Format(ComicRack.Localize(Key, "Books", "You have selected {0} books."), books.Count());
            MessageBox.Show(localizedText);
        }

        // Implementation for Editor hook
        void IEditor.Execute(ComicBook[] books)
        {
            Log("Running from IEditor entry point");
            string localizedText = string.Format(ComicRack.Localize(Key, "Editor", "You are currently editing the Book:\n{0}"), books.FirstOrDefault()?.Caption);
            MessageBox.Show(localizedText);
        }

        // Implementation for Config
        void IConfig.Execute()
        {
            Log("Simulating HTML scraping");
            string html = """
                <div id="test-container" class="sample">
                  <h1>Main Heading</h1>
                  <p>This is a <strong>bold</strong>, <em>italic</em>, and <a href="https://example.com" target="_blank">linked</a> text.</p>
                  <ul>
                    <li>Item 1</li>
                    <li>Item 2 &amp; Special Chars: &lt; &gt; &quot; &#39;</li>
                  </ul>
                </div>
                """;

            string title = ComicRack.Localize(Key, "Title", "Given the following simulated html:");
            string headerValue = ComicRack.Localize(Key, "Header", "h1 value is {0}");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(title);
            sb.AppendLine();
            sb.AppendLine(html);

            Log("Using HtmlAgilityPack");
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var h1 = doc?.DocumentNode.SelectSingleNode("//h1");
            string localizedtext = string.Format(headerValue, h1.InnerText);

            sb.AppendLine();
            sb.AppendLine(localizedtext);

            MessageBox.Show(sb.ToString());

        }
    }
}
