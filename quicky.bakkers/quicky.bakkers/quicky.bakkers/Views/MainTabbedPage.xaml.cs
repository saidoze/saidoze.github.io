using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace quicky.bakkers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage ()
        {
            InitializeComponent();

            Children.Add(new LeaderBoardContentPage());
            Children.Add(new ResultsContentPage());
            Children.Add(new UserContentPage());
        }
    }
}