using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OKr.Win8Book.Client.Controls
{
    public sealed partial class Cover : UserControl
    {
        protected App App { get { return App.Current as App; } }

        private double ScreenHeight
        {
            get
            {
                return Window.Current.Bounds.Height;
            }
        }

        public Cover()
        {
            this.InitializeComponent();
        }

        CoreDispatcher dispatcher;
        Page _Page;
        public void PrepareCover(Page page)
        {
            _Page = page;
            this.DataContext = App.HomeViewModel;
            continueReadingPanel.Visibility = App.HomeViewModel.Progress == null ? Visibility.Collapsed : Visibility.Visible;
            
            SetAnimationValue();
            dispatcher = Window.Current.CoreWindow.Dispatcher;
            dispatcher.AcceleratorKeyActivated += dispatcher_AcceleratorKeyActivated;

            page.BottomAppBar.Visibility = Visibility.Collapsed;
            page.TopAppBar.Visibility = Visibility.Collapsed;
        }

        void dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            VirtualKey key = args.VirtualKey;
            if ((args.EventType == CoreAcceleratorKeyEventType.KeyDown))
            {
                Unlock();
                args.Handled = true;
            }
        }

        private void coverPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Unlock();
        }

        public void Unlock(bool useTransitions = true)
        {
            dispatcher.AcceleratorKeyActivated -= dispatcher_AcceleratorKeyActivated;
            VisualStateManager.GoToState(this, "UnCovered", useTransitions);
            _Page.BottomAppBar.Visibility = Visibility.Visible;
            _Page.TopAppBar.Visibility = Visibility.Visible;
        }

        private void SetAnimationValue()
        {
            // find the "UnCovered" visual state and set a proper value to its storyboard animation

            VisualStateGroup vsGroup = (from stateGroup in VisualStateManager.GetVisualStateGroups(rootGrid)
                                        where stateGroup.Name == "CoverStates"
                                        select stateGroup).FirstOrDefault();

            var visualState = from state in vsGroup.States
                              where state.Name == "UnCovered"
                              select state;

            var animation = visualState.FirstOrDefault().Storyboard.Children.FirstOrDefault() as DoubleAnimation;
            animation.To = 0 - this.ScreenHeight;
        }

        private void UnCovered_Completed(object sender, object e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void continueReading_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            if (ContinueReadingPointerPressed!=null)
            {
                ContinueReadingPointerPressed(this, e);
            }
        }

        public event PointerEventHandler ContinueReadingPointerPressed;
    }
}
