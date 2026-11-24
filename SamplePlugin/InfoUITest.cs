using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using System.Windows.Forms;

namespace SamplePlugin
{
    public class InfoUITest : DotNetPlugin, IComicInfoUI
    {
        public override string Key { get; set; } = "InfoUITest";
        public override string Name => "[Code Sample] Info UI Sample";

        public InfoUITest()
        {
        }

        public UIControl Execute()
        {
            // Important to return a new instance of your control each time, otherwise it will be removed when added to the folder view is created since it's the same instance as the previous one.
            return new MyControl();
        }
    }

    // It is important to inherit from UIControl, because it forces to implement ShowInfo, which is required.
    public class MyControl : UIControl
    {
        ListBox lb = new ListBox();

        public MyControl()
        {
            lb.Dock = DockStyle.Fill;
            lb.IntegralHeight = false;
            this.Controls.Add(lb);
        }

        public override void ShowInfo(ComicBook[] books)
        {
            lb.Items.Clear();
            foreach (var b in books)
            {
                lb.Items.Add(b.Caption);
            }
        }
    }
}
