using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WINsiderIoT.Helper;
using WINsiderIoT.Models;

namespace WINsiderIoT.ViewModels
{
    public class FeedViewModel: BaseViewModel
    {
        private ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();

        /// <summary>
        /// Gets or sets the feed items
        /// </summary>
        public ObservableCollection<FeedItem> FeedItems
        {
            get { return feedItems; }
            set { feedItems = value; OnPropertyChanged("FeedItems"); }
        }

        private FeedItem selectedFeedItem;

        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public FeedItem SelectedFeedItem
        {
            get { return selectedFeedItem; }
            set
            {
                selectedFeedItem = value;
                OnPropertyChanged("SelectedFeedItem");
            }
        }

        private RelayCommand loadItemsCommand;

        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public ICommand LoadItemsCommand
        {
            get { return loadItemsCommand ?? (loadItemsCommand = new RelayCommand(async () => await ExecuteLoadItemsCommand())); }
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var httpClient = new HttpClient();
            var feed = "http://windowscommunity.de/feed/";
            var responseString = await httpClient.GetStringAsync(feed);

            FeedItems.Clear();
            var items = await ParseFeed(responseString);
            foreach (var item in items)
            {
                FeedItems.Add(item);
            }

            IsBusy = false;
        }

        /// <summary>
        /// Parse the RSS Feed
        /// </summary>
        /// <param name="rss"></param>
        /// <returns></returns>
        private async Task<List<FeedItem>> ParseFeed(string rss)
        {
            XNamespace nsContent = "http://purl.org/rss/1.0/modules/content/";

            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;

                return (from item in xdoc.Descendants("item")
                        select new FeedItem
                        {
                            Id = id++,
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element(nsContent + "encoded"),
                            Uri = new System.Uri((string)item.Element("link")),
                            PublishDate = (string)item.Element("pubDate"),
                        }).ToList();
            });
        }

        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(int id)
        {
            return FeedItems.FirstOrDefault(i => i.Id == id);
        }
    }
}
