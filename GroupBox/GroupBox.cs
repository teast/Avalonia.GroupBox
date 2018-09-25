using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Styling;

namespace Teast.Controls
{
    public class GroupBox: UserControl, IStyleable
    {
        /// <summary>
        /// Defines the <see cref="Header"/> property.
        /// </summary>
        public static readonly DirectProperty<GroupBox, string> HeaderProperty =
            AvaloniaProperty.RegisterDirect<GroupBox, string>(
                nameof(Header),
                o => o.Header,
                (o, v) => o.Header = o.HeaderUpperCase ? v?.ToUpperInvariant() : v );
        
        /// <summary>
        /// Defines the <see cref="BoxContent"/> property.
        /// </summary>
        public static readonly DirectProperty<GroupBox, IControl> BoxContentProperty =
            AvaloniaProperty.RegisterDirect<GroupBox, IControl>(nameof(BoxContent),
                o => o.BoxContent,
                (o, v) => o.BoxContent = v);

        /// <summary>
        /// Defines the <see cref="HeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<GroupBox, IBrush>(nameof(HeaderBackground));

        /// <summary>
        /// Defines the <see cref="HeaderForeground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderForegroundProperty =
            AvaloniaProperty.Register<GroupBox, IBrush>(nameof(HeaderForeground));

        /// <summary>
        /// Defines the <see cref="HeaderMargin"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> HeaderMarginProperty =
            AvaloniaProperty.Register<GroupBox, Thickness>(nameof(HeaderMargin));

        /// <summary>
        /// Defines the <see cref="HeaderUpperCase"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> HeaderUpperCaseProperty =
            AvaloniaProperty.Register<GroupBox, bool>(nameof(HeaderUpperCase));

        private string _header;
        private IControl _boxContent;

        /// <summary>
        /// Gets or sets a brush with which to paint the Header background.
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a brush with which to paint the Header text.
        /// </summary>
        public IBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a margin for the header text.
        /// </summary>
        public Thickness HeaderMargin
        {
            get { return GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the header text should be converted to upper-case
        /// </summary>
        public bool HeaderUpperCase
        {
            get { return GetValue(HeaderUpperCaseProperty); }
            set { SetValue(HeaderUpperCaseProperty, value); }
        }

        /// <summary>
        /// Gets or sets header text
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { SetAndRaise(HeaderProperty, ref _header, value); }
        }

        /// <summary>
        /// Gets or sets the decorated control.
        /// </summary>
        [Content]
        public IControl BoxContent
        {
            get { return _boxContent; }
            set { SetAndRaise(BoxContentProperty, ref _boxContent, value); }
        }

        Type IStyleable.StyleKey => typeof(GroupBox);

        public GroupBox()
        {
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitions("Auto,*"),
                ColumnDefinitions = new ColumnDefinitions("*")
            };

            var dock = new DockPanel
            {
                LastChildFill = true,
                [Grid.RowProperty] = 0,
                [!DockPanel.BackgroundProperty] = this.GetObservable(HeaderBackgroundProperty).ToBinding()
            };

            var header = new TextBlock
            {
                [!TextBlock.TextProperty] = this.GetObservable(HeaderProperty).ToBinding(),
                [!TextBlock.MarginProperty] = this.GetObservable(HeaderMarginProperty).ToBinding(),
                [!TextBlock.ForegroundProperty] = this.GetObservable(HeaderForegroundProperty).ToBinding()
            };

            var ctrl = new UserControl
            {
                [Grid.RowProperty] = 1,
                [!UserControl.ContentProperty] = this.GetObservable(BoxContentProperty).ToBinding(),
                Margin = new Thickness(5)
            };

            dock.Children.Add(header);
            grid.Children.Add(dock);
            grid.Children.Add(ctrl);
            this.Margin = new Thickness(0, 0, 3, 10);

            this.Content = grid;
        }
   }
}
