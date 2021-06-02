using System;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;

namespace MovieApp.WPF.State.NetworkState
{
    public static class NetworkState
    {
        private static bool _isInternetAvailable;

        public static bool IsInternetAvailable
        {
            get => _isInternetAvailable;
            set
            {
                if (_isInternetAvailable != value)
                {
                    _isInternetAvailable = value;
                    StateChanged?.Invoke();
                }
            }
        }

        static NetworkState()
        {
            _isInternetAvailable = CheckForInternetConnection();
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
        }

        private static void AddressChangedCallback(object sender, EventArgs e)
        {
            IsInternetAvailable = CheckForInternetConnection();
        }

        public static event Action StateChanged;

        public static bool CheckForInternetConnection(int timeoutMs = 7000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}