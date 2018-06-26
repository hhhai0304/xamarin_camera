using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCamera
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Connection : ContentPage
    {
        public Connection()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            string msg = "Wifi is ON: " + HardwareUtils.IsWifiEnable().ToString() + "\nMobile data is ON: " + HardwareUtils.IsMobileDataEnabled().ToString() +
                "\nRooted: " + HardwareUtils.IsDeviceRooted();
            DisplayAlert("Báo cáo sếp!", msg, "OK");
        }
    }
}