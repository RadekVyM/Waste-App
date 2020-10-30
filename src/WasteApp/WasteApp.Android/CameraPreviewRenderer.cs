using System;
using Android.Content;
using Android.Hardware;
using WasteApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WasteApp.CameraPreview), typeof(CameraPreviewRenderer))]
namespace WasteApp.Droid
{
    // Based on: https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view

    public class CameraPreviewRenderer : ViewRenderer<WasteApp.CameraPreview, WasteApp.Droid.CameraPreview>
    {
        CameraPreview cameraPreview;

        public CameraPreviewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WasteApp.CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                cameraPreview.Click -= OnCameraPreviewClicked;
            }
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    cameraPreview = new CameraPreview(Context);
                    SetNativeControl(cameraPreview);
                }

                e.NewElement.OpenCamera = () => 
                {
                    try
                    {
                        if (Control.Preview != null)
                        {
                            Control.Preview.Release();
                            Control.Preview = null;
                        }

                        Control.Preview = Camera.Open((int)e.NewElement.Camera);
                    }
                    catch
                    { }

                    //cameraPreview.Click -= OnCameraPreviewClicked;
                    //cameraPreview.Click += OnCameraPreviewClicked;
                };
            }
        }

        void OnCameraPreviewClicked(object sender, EventArgs e)
        {
            if (cameraPreview.IsPreviewing)
            {
                cameraPreview.Preview.StopPreview();
                cameraPreview.IsPreviewing = false;
            }
            else
            {
                cameraPreview.Preview.StartPreview();
                cameraPreview.IsPreviewing = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control?.Preview?.Release();
                cameraPreview = null;
            }
            
            base.Dispose(disposing);
        }
    }
}