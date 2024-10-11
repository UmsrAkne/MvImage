using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using MvImage.Models;

namespace MvImage.Behaviors
{
    public class ListBoxKeyDownBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not ListBox lb || lb.Items.Count == 0)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.J:
                    if (lb.SelectedIndex < lb.Items.Count - 1)
                    {
                        lb.SelectedIndex++;
                    }

                    break;
                case Key.K:
                    if (lb.SelectedIndex > 0)
                    {
                        lb.SelectedIndex--;
                    }

                    break;
            }

            lb.ScrollIntoView(lb.SelectedItem);

            if (e.Key is Key.J or Key.K)
            {
                // key が j, k の場合はカーソル移動なので、ここで処理を中断する
                return;
            }

            if (e.Key is < Key.A or > Key.Z)
            {
                return;
            }

            if (lb.SelectedItem is IKeyed item)
            {
                item.KeyCharacter = e.Key.ToString().ToLower().First();
            }
        }
    }
}