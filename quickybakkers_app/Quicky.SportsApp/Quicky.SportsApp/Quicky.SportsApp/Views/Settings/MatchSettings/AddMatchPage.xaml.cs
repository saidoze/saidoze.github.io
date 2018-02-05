﻿using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services.Service;
using Quicky.SportsApp.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quicky.SportsApp.Views.Settings.MatchSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMatchPage : ContentPage
    {
        private PlayerService _playerService;
        private MatchdayService _matchdayService;
        private MatchService _matchService;
        private TeamService _teamService;
        private List<Player> _players;

        public AddMatchPage()
        {
            InitializeComponent();
            _playerService = new PlayerService();
            _matchService = new MatchService();
            _matchdayService = new MatchdayService();
            _teamService = new TeamService();
            LoadData();
        }

        private void LoadData()
        {
            //load players
            _players = _playerService.GetAllItems();
            if (_players != null)
            {
                _players = _players.OrderBy(p => p.Name).ToList();
                team1player1Picker.ItemsSource = _players;
                team1player2Picker.ItemsSource = _players;
                team2player1Picker.ItemsSource = _players;
                team2player2Picker.ItemsSource = _players;
                //load open matchdays
                var matchdays = _matchdayService.GetAllItems();
                matchDayPicker.ItemsSource = matchdays;
                //possible scores
                var scores = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                team1ScorePicker.ItemsSource = scores;
                team2ScorePicker.ItemsSource = scores;
            }
        }

        #region SelectedIndex changes
        public void OnTeam1Player1SelectedIndexChanged(object sender, EventArgs e)
        {
            //var picker = (Picker)sender;
            //int selectedIndex = picker.SelectedIndex;
        }
        public void OnTeam1Player2SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void OnTeam2Player1SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void OnTeam2Player2SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //validate input
                if (!ValidateInput())
                    return;

                var matchdayId = (matchDayPicker.SelectedItem as Matchday).Key;
                var team1player1Id = (team1player1Picker.SelectedItem as Player).Key;
                var team1player2Id = (team1player2Picker.SelectedItem as Player).Key;
                var team2player1Id = (team2player1Picker.SelectedItem as Player).Key;
                var team2player2Id = (team2player2Picker.SelectedItem as Player).Key;

                //create teams
                var team1 = await _teamService.SaveTeam(new Team() { Player1Key = team1player1Id, Player2Key = team1player2Id, Player1AllowPoints = team1player1AllowPoints.IsToggled, Player2AllowPoints = team1player2AllowPoints.IsToggled });
                var team2 = await _teamService.SaveTeam(new Team() { Player1Key = team2player1Id, Player2Key = team2player2Id, Player1AllowPoints = team2player1AllowPoints.IsToggled, Player2AllowPoints = team2player2AllowPoints.IsToggled });

                //TODO check if teams already exist, then don't create

                //create match
                var match = await _matchService.SaveMatch(new Match()
                {
                    MatchdayKey = matchdayId,
                    Team1Key = team1.Key,
                    Team2Key = team2.Key,
                    ScoreTeam1 = Convert.ToInt32(team1ScorePicker.SelectedItem),
                    ScoreTeam2 = Convert.ToInt32(team2ScorePicker.SelectedItem)
                });

                if (!string.IsNullOrEmpty(match.Key))
                {
                    //redirect to previous page
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool ValidateInput()
        {
            if (team1player1Picker.SelectedIndex == -1 || team1player2Picker.SelectedIndex == -1 ||
                team2player1Picker.SelectedIndex == -1 || team2player2Picker.SelectedIndex == -1 ||
                matchDayPicker.SelectedIndex == -1)
                return false;

            return true;
        }

        public async void onTogglePlayerPoints(object sender, EventArgs e)
        {
            if (!(sender as Switch).IsToggled)
            {
                var answer = await DisplayAlert("Uitschakelen", "Spelerspunten niet meetellen?", "Ja", "Neen");
                if (!answer)
                    (sender as Switch).IsToggled = true;
            }
        }
    }
}