using System;
using System.Text.RegularExpressions;

namespace WINsiderIoT.Models
{
    /// <summary>
    /// Model to the define that data set of an blog feed item.
    /// </summary>
    public class FeedItem
    {
        /// <summary>
        /// CSS-based styling parameters for feed item html content.
        /// </summary>
        private const string HtmlStyle = "<style>body { font-family: sans-serif; font: caption; color: #4A4A4A;} h3 {line-height: 1.5em;} h1 {font-size: 100%; color: #37A3CF;} img, iframe { max-width: 100% !important; height:auto !important;} p {line-height: 1.5em;} a {color: #37A3CF; text-decoration: none;}</style>";

        #region Public members

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the html description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the uri to the blog post
        /// </summary>
        /// <value>The URI to the blog post</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the feed author identifier.
        /// </summary>
        /// <value>The feed author identifier.</value>
        public string Author { get; set; }

        #endregion

        #region Public computed members

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>The publish date.</value>
        public string PublishDate
        {
            get => _publishDate;
            set => _publishDate = DateTime.TryParse(value, out DateTime time) ? time.ToLocalTime().ToString("dd.MM.yyyy") : value;
        }

        /// <summary>
        /// Gets a custom CSS-styled variant of <see cref="Description"/>.
        /// </summary>
        public string StyledDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(_styledDescripton))
                    return _styledDescripton;

                _styledDescripton = HtmlStyle + Description;
                return _styledDescripton;
            }
        }

        /// <summary>
        /// Gets the caption which is a cleaned up version of <see cref="Description"/>
        /// </summary>
        /// <value>The caption.</value>
		public string Caption
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_caption))
                    return _caption;

                // Remove Html tags
                _caption = Regex.Replace(Description, "<[^>]*>", string.Empty);
                return _caption;
            }
        }

        /// <summary>
        /// Gets the first image of the blog post
        /// </summary>
        /// <value>The first image.</value>
		public string FirstImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_firstImage))
                    return _firstImage;

                var foundImageString = Regex.Match(Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;

                _firstImage = foundImageString.Length > 0 ? foundImageString : Placeholder;
                return _firstImage;
            }
        }

        #endregion

        #region Public constants

        /// <summary>
        /// Cover image placeholder link./Assets/placeholder.png
        /// </summary>
        public static string Placeholder => "/Assets/placeholder.png";

        #endregion        

        #region Private backing fields

        private string _styledDescripton;
        private string _caption;
        private string _publishDate;
        private string _firstImage;

        #endregion
    }
}
