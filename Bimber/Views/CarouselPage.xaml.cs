using Bimber.ViewModels;
using MLToolkit.Forms.SwipeCardView.Core;
using System;
using Xamarin.Forms;

namespace Bimber.Views
{
    public partial class CarouselPage : ContentView
    {
        public CarouselPage()
        {
            InitializeComponent();
        }

        private void OnDragging(object sender, DraggingCardEventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;
            var nopeLabel = view.FindByName<Label>("nopeLabel");
            var likeLabel = view.FindByName<Label>("likeLabel");
            var superLabel = view.FindByName<Label>("superLabel");
            var threshold = (BindingContext as CarouselPageViewModel)?.Threshold ?? 0;

            var draggedXPercent = e.DistanceDraggedX / threshold;

            var draggedYPercent = e.DistanceDraggedY / threshold;

            switch (e.Position)
            {
                case DraggingCardPosition.Start:
                    nopeLabel.Opacity = 0;
                    likeLabel.Opacity = 0;
                    superLabel.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superButton.Scale = 1;
                    break;

                case DraggingCardPosition.UnderThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeLabel.Opacity = (-1) * draggedXPercent;
                        nopeButton.Scale = 1 + (draggedXPercent / 3.5);
                        superLabel.Opacity = 0;
                        superButton.Scale = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeLabel.Opacity = draggedXPercent;
                        likeButton.Scale = 1 - (draggedXPercent / 3.5);
                        superLabel.Opacity = 0;
                        superButton.Scale = 1;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        nopeLabel.Opacity = 0;
                        likeLabel.Opacity = 0;
                        nopeButton.Scale = 1;
                        likeButton.Scale = 1;
                        superLabel.Opacity = (-1) * draggedYPercent;
                        superButton.Scale = 1 + (draggedYPercent / 3.5);
                    }
                    break;

                case DraggingCardPosition.OverThreshold:
                    if (e.Direction == SwipeCardDirection.Left)
                    {
                        nopeLabel.Opacity = 1;
                        superLabel.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Right)
                    {
                        likeLabel.Opacity = 1;
                        superLabel.Opacity = 0;
                    }
                    else if (e.Direction == SwipeCardDirection.Up)
                    {
                        nopeLabel.Opacity = 0;
                        likeLabel.Opacity = 0;
                        superLabel.Opacity = 1;
                    }
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    nopeLabel.Opacity = 0;
                    likeLabel.Opacity = 0;
                    superLabel.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superButton.Scale = 1;
                    break;

                case DraggingCardPosition.FinishedOverThreshold:
                    nopeLabel.Opacity = 0;
                    likeLabel.Opacity = 0;
                    superLabel.Opacity = 0;
                    nopeButton.Scale = 1;
                    likeButton.Scale = 1;
                    superButton.Scale = 1;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnButtonTapped(object sender, EventArgs e)
        {
            if (!(sender is Frame element))
            {
                return;
            }

            if (element.Id == nopeButton.Id)
            {
                swipeCardView.InvokeSwipe(SwipeCardDirection.Left);
            }
            else if (element.Id == superButton.Id)
            {
                swipeCardView.InvokeSwipe(SwipeCardDirection.Up);
            }
            else if (element.Id == likeButton.Id)
            {
                swipeCardView.InvokeSwipe(SwipeCardDirection.Right);
            }
        }
    }
}