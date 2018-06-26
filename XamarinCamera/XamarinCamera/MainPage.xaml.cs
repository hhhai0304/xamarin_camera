using System;
using Xamarin.Forms;

namespace XamarinCamera
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        async void OnTakePhotoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPage());
        }
    }
}