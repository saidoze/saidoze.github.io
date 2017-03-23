// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var leagueRankingApp = angular.module('leagueRankingApp', ['ionic', 'firebase', 'ionic-datepicker', 'ionic.wizard'])

  .run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
      if (window.cordova && window.cordova.plugins.Keyboard) {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

        // Don't remove this line unless you know what you are doing. It stops the viewport
        // from snapping when text inputs are focused. Ionic handles this internally for
        // a much nicer keyboard experience.
        cordova.plugins.Keyboard.disableScroll(true);
      }
      if (window.StatusBar) {
        StatusBar.styleDefault();
      }
    });
  })

  .config(function ($stateProvider, $urlRouterProvider, ionicDatePickerProvider) {
    $stateProvider.state('ranking', {
      url: '/',
      controller: 'RankingController',
      templateUrl: 'partials/ranking.html'
    })
      .state('gamedates', {
        url: '/gamedates',
        controller: 'GamedatesController',
        templateUrl: 'partials/gamedates.html'
      })
      .state('results', {
        cache: false,
        url: '/results',
        controller: 'ResultsController',
        templateUrl: 'partials/results.html'
      })
      .state('addGame', {
        cache: false,
        url: '/add-game',
        controller: 'AddGameController',
        templateUrl: 'partials/addgame.html'
      })
      .state('settings', {
        cache: false,
        url: '/settings',
        controller: 'SettingsController',
        templateUrl: 'partials/settings.html'
      })
      .state('closeGamedate', {
        cache: false,
        url: '/settings/closegameDate/:gamedateId',
        controller: 'CloseGamedateController',
        templateUrl: 'partials/closeGameDate.html'
      });
    $urlRouterProvider.otherwise('/');

    //$urlRouterProvider.when('/settings', '/settings/preferences');
    var datePickerObj = {
      inputDate: new Date(),
      titleLabel: 'Select a Date',
      setLabel: 'Set',
      todayLabel: 'Today',
      closeLabel: 'Close',
      mondayFirst: true,
      weeksList: ["Z", "M", "D", "W", "D", "V", "Z"],
      monthsList: ["Jan", "Feb", "Mar", "April", "Mei", "Juni", "Juli", "Aug", "Sept", "Okt", "Nov", "Dec"],
      templateType: 'popup',
      from: new Date(2012, 8, 1),
      to: new Date(2018, 8, 1),
      showTodayButton: true,
      dateFormat: 'dd MMMM yyyy',
      closeOnSelect: false,
      disableWeekdays: []
    };
    ionicDatePickerProvider.configDatePicker(datePickerObj);

  })

  .constant("myConfig", {
        "firebaseUrl": "https://sport-league-creator.firebaseio.com/"
    });
    /*// connect to firebase 
  var ref = new Firebase("https://burning-torch-4263.firebaseio.com/days");  
  var fb = $firebase(ref);*/
