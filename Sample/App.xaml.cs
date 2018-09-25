using Avalonia;
using Avalonia.Markup.Xaml;

namespace Sample
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}