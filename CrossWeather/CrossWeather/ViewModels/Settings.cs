namespace CrossWeather
{
    class Settings : CommonINotify
    {
		
        private bool _useGeoLocation;

        public bool useGeoLocation
        {
            get { return _useGeoLocation; }
            set
            {
                _useGeoLocation = value;
                OnPropertyChanged("useGeoLocation");
            }
        }

        public Settings()
        {
            useGeoLocation = true;
        }
    }
}
