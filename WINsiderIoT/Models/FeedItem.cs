using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Controls;

namespace WINsiderIoT.Models
{
    /// <summary>
    /// Model to the define that data set of an blog feed item.
    /// </summary>
    public class FeedItem
    {
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
            get { return publishDate; }

            set
            {
                if (DateTime.TryParse(value, out DateTime time))
                {
                    publishDate = time.ToLocalTime().ToString("dd.MM.yyyy");
                }
                else
                {
                    publishDate = value;
                }
            }
        }

        /// <summary>
        /// Gets a custom CSS-styled variant of <see cref="Description"/>.
        /// </summary>
        public string StyledDescription
        {
            get
            {
                if(!string.IsNullOrEmpty(styledDescripton))
                {
                    return styledDescripton;
                }

                styledDescripton = htmlStyle + Description;
                return styledDescripton;
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
                if (!string.IsNullOrWhiteSpace(caption))
                {
                    return caption;
                }

				// Remove Html tags
				caption = Regex.Replace(Description, "<[^>]*>", string.Empty);

				return caption;
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
                if (!string.IsNullOrWhiteSpace(firstImage))
                {
                    return firstImage;
                }

                var foundImageString = Regex.Match(Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;

                if (foundImageString.Length > 0)
                {
                    firstImage = foundImageString;
                }
                else
                {
                    firstImage = Placeholder;
                }

				return firstImage;
			}
		}

        #endregion

        #region Public constants

        /// <summary>
        /// Cover image placeholder link./Assets/placeholder.png
        /// </summary>
        public static string Placeholder { get { return "/Assets/placeholder.png"; } }

        #endregion

        /// <summary>
        /// CSS-based styling parameters for feed item html content.
        /// </summary>
        private const string htmlStyle = "<style>body { font-family: sans-serif; font: caption; color: #4A4A4A;} h3 {line-height: 1.5em;} h1 {font-size: 100%; color: #37A3CF;} img, iframe { max-width: 100% !important; height:auto !important;} p {line-height: 1.5em;} a {color: #37A3CF; text-decoration: none;}</style>";

        #region Private backing fields

        private string styledDescripton;
        private string caption;
        private string publishDate;
		private string firstImage;

        #endregion

        #region Constructor

        public FeedItem()
        {
        }

        #endregion
    }
}
