
namespace LanaSoftCRM
{
    /// <summary>
    /// Describes an entity form left navigation item
    /// </summary>
    public struct IsvLeftNavigationItem
    {
        private string _title;
        private string _iconUrl;
        private string _url;
        private string _id;

        /// <summary>
        /// The title of the navigation item
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// The URL to the Icon that should be rendered next to the title for this item
        /// </summary>
        public string Icon
        {
            get
            {
                return _iconUrl;
            }

            set
            {
                _iconUrl = value;
            }
        }

        /// <summary>
        /// The URL to load when the user navigates to this item
        /// </summary>
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }


        /// <summary>
        /// The unique client-side ID of this item
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
    }
}
