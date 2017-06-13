using System;
using Foundation;
using UIKit;
using KhulumaClient;
using KhulumaClient.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GroupChatPage), typeof(ChatPageRenderer))]
namespace KhulumaClient.iOS
{
    public class ChatPageRenderer : PageRenderer
    {

        private NSObject keyBoardWillShow;
        private NSObject keyBoardWillHide;
        private nfloat scrollAmout;
        private double animDuration;
        private UIViewAnimationCurve animCurve;
        private bool keyboardShowing;

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();

            keyBoardWillShow = UIKeyboard.Notifications.ObserveWillShow(KeyboardWillShow);

            keyBoardWillHide = UIKeyboard.Notifications.ObserveWillHide(KeyboardWillHide);
        }

        void KeyboardWillShow(object sender, UIKeyboardEventArgs args)
        {
            if (!keyboardShowing)
            {
                keyboardShowing = true;
                animDuration = args.AnimationDuration;
                animCurve = args.AnimationCurve;

                var r = UIKeyboard.FrameBeginFromNotification(args.Notification);
                scrollAmout = r.Height;
                ScrollTheView(true);
            }
        }

        void KeyboardWillHide(object sender, UIKeyboardEventArgs args)
        {
            if (keyboardShowing)
            {
                keyboardShowing = false;
                animDuration = args.AnimationDuration;
                animCurve = args.AnimationCurve;

                var r = UIKeyboard.FrameBeginFromNotification(args.Notification);
                scrollAmout = r.Height;
                ScrollTheView(false);
            }
        }

        private void ScrollTheView(bool scale)
        {
            UIView.BeginAnimations(string.Empty, IntPtr.Zero);
            UIView.SetAnimationDuration(animDuration);
            UIView.SetAnimationCurve(animCurve);

            var frame = View.Frame;

            if (scale)
                frame.Y -= scrollAmout;
            else
                frame.Y += scrollAmout;
            View.Frame = frame;
            UIView.CommitAnimations();
        }

    }
}
