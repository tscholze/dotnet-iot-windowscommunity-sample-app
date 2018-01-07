using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WINsiderIoT.Helper;
using WINsiderIoT.Models;

namespace WINsiderIoT.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        private readonly XNamespace _contentNamespace = "http://purl.org/rss/1.0/modules/content/";

        private ObservableCollection<FeedItem> _feedItems = new ObservableCollection<FeedItem>();
        /// <summary>
        /// Gets or sets the feed items
        /// </summary>
        public ObservableCollection<FeedItem> FeedItems
        {
            get => _feedItems;
            set { _feedItems = value; OnPropertyChanged(); }
        }

        private FeedItem _selectedFeedItem;
        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public FeedItem SelectedFeedItem
        {
            get => _selectedFeedItem;
            set
            {
                _selectedFeedItem = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _loadItemsCommand;
        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public ICommand LoadItemsCommand
        {
            get { return _loadItemsCommand ?? (_loadItemsCommand = new RelayCommand(async () => await ExecuteLoadItemsCommand())); }
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                using (var httpClient = new HttpClient())
                {
                    var responseString = await httpClient.GetStringAsync(Utils.FeedUrl);
                    FeedItems.Clear();

                    var items = await ParseFeedAsync(responseString);
                    foreach (var feedItem in items)
                        FeedItems.Add(feedItem);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Parse the RSS Feed
        /// </summary>
        /// <param name="rss"></param>
        /// <returns></returns>
        private async Task<List<FeedItem>> ParseFeedAsync(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;

                return (from item in xdoc.Descendants("item")
                        select new FeedItem
                        {
                            Id = id++,
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element(_contentNamespace + "encoded"),
                            Uri = new Uri((string)item.Element("link")),
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