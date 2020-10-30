using System;
using Xamarin.Forms;

namespace WasteApp
{
	// Based on: https://docs.microsoft.com/cs-cz/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view

	public class CameraPreview : View
    {
		public static readonly BindableProperty CameraProperty = BindableProperty.Create(
			propertyName: "Camera",
			returnType: typeof(CameraOptions),
			declaringType: typeof(CameraPreview),
			defaultValue: CameraOptions.Rear);

		public CameraOptions Camera
		{
			get { return (CameraOptions)GetValue(CameraProperty); }
			set { SetValue(CameraProperty, value); }
		}

		public Action OpenCamera;

		public void Open()
        {
			OpenCamera();
		}
	}

	public enum CameraOptions
	{
		Rear,
		Front
	}
}
